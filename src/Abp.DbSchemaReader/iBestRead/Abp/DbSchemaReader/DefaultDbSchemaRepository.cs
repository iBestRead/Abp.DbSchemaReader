using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using iBestRead.Abp.DbSchemaReader.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace iBestRead.Abp.DbSchemaReader
{
    public class DefaultDbSchemaRepository : IDbSchemaRepository, ITransientDependency
    {
        public ILogger<DefaultDbSchemaRepository> Logger { get; set; }
        private readonly IDbProviderManager _dbProviderManager;
        private readonly ISqlLoader _sqlLoader;
        
        public DefaultDbSchemaRepository(
            ILogger<DefaultDbSchemaRepository> logger, 
            IDbProviderManager dbProviderManager, 
            ISqlLoader sqlLoader)
        {
            _dbProviderManager = dbProviderManager;
            _sqlLoader = sqlLoader;
            Logger = NullLogger<DefaultDbSchemaRepository>.Instance;
        }


        public async Task<List<Table>> GetSchemaAsync(
            DbProviderType dbProviderType, 
            string connectionString, 
            string dbName,
            string dbSchema = null)
        {
            Logger.LogInformation($"----数据库类型:{dbProviderType.ToString()}, 开始查询. ----");
            var tables = new List<Table>();
            try
            {
                _dbProviderManager.TryGet(dbProviderType, out var dbProvider);
                if (null == dbProvider)
                {
                    Logger.LogInformation($"----数据库类型:{dbProviderType.ToString()} 对应的Provider未找到! ----");
                    return null;
                }

                return await GetSchemaByDbProviderAsync(dbProvider, connectionString, dbName, dbSchema);
            }
            finally
            {
                
            }

            Logger.LogInformation($"----Provider:{dbProviderType.ToString()},Tables:{tables.Count()} QueryTable End! ----");
            return tables;
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
                            TableId = table.Id, 
                            TableName = table.Name
                        }))
                    .ToList();
                table.Columns = columns;
            }

            return tables;
        }

    }
}