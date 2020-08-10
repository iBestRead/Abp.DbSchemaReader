using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using iBestRead.Abp.DbSchemaReader.Dtos;
using iBestRead.Abp.DbSchemaReader.Entities;
using iBestRead.Database.Consts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace iBestRead.Abp.DbSchemaReader
{
    public class DefaultDbSchemaReader : IDbSchemaReader, ITransientDependency
    {
        public ILogger<DefaultDbSchemaReader> Logger { get; set; }
        
        private readonly IDbProviderManager _dbProviderManager;
        private readonly ISqlLoader _sqlLoader;
        private readonly IObjectMapper _objectMapper;
        
        public DefaultDbSchemaReader(
            IDbProviderManager dbProviderManager, 
            ISqlLoader sqlLoader, 
            IObjectMapper objectMapper)
        {
            _dbProviderManager = dbProviderManager;
            _sqlLoader = sqlLoader;
            _objectMapper = objectMapper;
            Logger = NullLogger<DefaultDbSchemaReader>.Instance;
        }

        public async Task<List<TableDto>> GetSchemaAsync(
            DbProviderType dbProviderType, 
            string connectionString, 
            string dbName,
            string dbSchema = null)
        {
            Logger.LogInformation($"---- 数据库类型:{dbProviderType.ToString()}, 开始读取. ----");
            var dtoTables = new List<TableDto>();
            try
            {
                _dbProviderManager.TryGet(dbProviderType, out var dbProvider);
                if (null == dbProvider)
                {
                    Logger.LogInformation($"----数据库类型:{dbProviderType.ToString()} 对应的Provider未找到! ----");
                    return null;
                }
                
                var tables = await GetSchemaByDbProviderAsync(dbProvider, connectionString, dbName, dbSchema);
                Logger.LogInformation($"---- 数据库类型:{dbProviderType.ToString()}, 结束读取. ----");

                dtoTables = _objectMapper.Map<List<Table>, List<TableDto>>(tables);
                return dtoTables;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "读取数据库Schema发生异常.");
            }

            return dtoTables;
        }

        private async Task<List<Table>> GetSchemaByDbProviderAsync(
            DbProvider dbProvider, 
            string connectionString,
            string dbName,
            string dbSchema)
        {
            using var connection = dbProvider.Factory.CreateConnection();
            if (null == connection)
                return null;
            
            connection.ConnectionString = connectionString;
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            var querySql = _sqlLoader.Get(dbProvider.DbProviderType);

            if (dbProvider.DbProviderType == DbProviderType.PostgreSql 
                && dbSchema.IsNullOrWhiteSpace())
            {
                dbSchema = "public";
            }
            
            var tables = (await connection.QueryAsync<Table>(querySql.Table, new
                {
                    DbSchema = dbSchema,
                    DbName = dbName
                }))
                .ToList();

            foreach (var table in tables)
            {
                var columns = (await connection.QueryAsync<Column>(querySql.Column, 
                        new
                        {
                            DbSchema= dbSchema, 
                            DbName = dbName,
                            TableName = table.Name
                        }))
                    .ToList();
                table.Columns = columns;
            }

            return tables;
        }

    }
}