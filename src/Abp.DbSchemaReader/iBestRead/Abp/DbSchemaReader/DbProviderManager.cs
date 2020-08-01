using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Volo.Abp.DependencyInjection;

namespace iBestRead.Abp.DbSchemaReader
{
    public class DbProviderManager : IDbProviderManager, ISingletonDependency
    {
        private readonly IDictionary<string, DbProvider> _dbProviders =
            new Dictionary<string, DbProvider>(StringComparer.CurrentCultureIgnoreCase);

        private static readonly DbProvider DbProviderSqlServer =
            new DbProvider(
                DbProviderType.SqlServer,
                "@",
                SqlClientFactory.Instance);
        
        private static readonly DbProvider DbProviderMySql =
            new DbProvider(
                DbProviderType.MySql,
                "?",
                MySqlClientFactory.Instance);
            
        
        private static readonly DbProvider DbProviderOracle  =
            new DbProvider(
                DbProviderType.Oracle,
                ":",
                OracleClientFactory.Instance);
        
        private static readonly DbProvider DbProviderPostgreSql =
            new DbProvider(
                DbProviderType.PostgreSql,
                ":",
                NpgsqlFactory.Instance);
        
        public DbProviderManager()
        {
            Add(DbProviderSqlServer);
            Add(DbProviderMySql);
            Add(DbProviderOracle);
            Add(DbProviderPostgreSql);
        }

        public bool TryGet(DbProviderType dbProviderType, out DbProvider dbProvider)
        {
            return _dbProviders.TryGetValue(dbProviderType.ToString(), out dbProvider);
        }
        
        private void Add(DbProvider dbProvider)
        {
            _dbProviders.Add(dbProvider.Name, dbProvider);
        }
    }
}