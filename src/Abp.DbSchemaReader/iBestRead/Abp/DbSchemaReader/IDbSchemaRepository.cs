using System.Collections.Generic;
using System.Threading.Tasks;
using iBestRead.Abp.DbSchemaReader.Entities;

namespace iBestRead.Abp.DbSchemaReader
{
    public interface IDbSchemaRepository
    {
        Task<List<Table>> GetSchemaAsync(
            DbProviderType dbProviderType, 
            string connectionString, 
            string dbName,
            string dbSchema = null);
    }
}