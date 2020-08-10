using System;
using System.Collections.Concurrent;
using System.Linq;
using iBestRead.Database.Consts;
using Microsoft.Extensions.FileProviders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.VirtualFileSystem;

namespace iBestRead.Abp.DbSchemaReader
{
    public class SqlLoader : ISqlLoader, ISingletonDependency
    {
        private readonly ConcurrentDictionary<DbProviderType, QuerySql> _dictionarySql;
        private readonly IVirtualFileProvider _virtualFileProvider;

        public SqlLoader(IVirtualFileProvider virtualFileProvider)
        {
            _virtualFileProvider = virtualFileProvider;
            _dictionarySql = new ConcurrentDictionary<DbProviderType, QuerySql>();
            Initial();
        }
        
        private void Initial()
        {
            var providers = Enum.GetValues(typeof(DbProviderType)).Cast<DbProviderType>();
            
            foreach (var provider in providers)
            {
                var sqlTable = _virtualFileProvider
                    .GetFileInfo($"/iBestRead/Abp/DbSchemaReader/Sql/{provider.ToString()}.Table.sql");
                if(!sqlTable.Exists)
                    continue;
                
                var sqlTableContent = sqlTable.ReadAsString();
                
                var sqlColumn = _virtualFileProvider
                    .GetFileInfo($"/iBestRead/Abp/DbSchemaReader/Sql/{provider.ToString()}.Column.sql");
                var sqlColumnContent = sqlColumn.ReadAsString();

                _dictionarySql.TryAdd(provider, new QuerySql(sqlTableContent, sqlColumnContent));
            }
        }

        public QuerySql Get(DbProviderType dbProviderType)
        {
            _dictionarySql.TryGetValue(dbProviderType, out var querySql);

            return querySql;
        }
        
    }
}