using System.Collections.Generic;
using System.Threading.Tasks;
using iBestRead.Abp.DbSchemaReader.Dtos;
using iBestRead.Abp.DbSchemaReader.Entities;
using iBestRead.Database.Consts;

namespace iBestRead.Abp.DbSchemaReader
{
    public interface IDbSchemaReader
    {
        Task<List<TableDto>> GetSchemaAsync(
            DbProviderType dbProviderType, 
            string connectionString, 
            string dbName,
            string dbSchema = null);
    }
}