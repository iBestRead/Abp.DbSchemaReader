using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Testing;
using Xunit;

namespace iBestRead.Abp.DbSchemaReader
{
    public class DefaultDbSchemaReader_Tests : AbpIntegratedTest<AbpDbSchemaReaderTestModule>
    {
        private readonly IDbSchemaReader _dbSchemaReader;

        public DefaultDbSchemaReader_Tests()
        {
            _dbSchemaReader = GetRequiredService<IDbSchemaReader>();
        }
        
        [Fact]
        public async Task Can_Get_SqlServer_Schema()
        {
            var result = await _dbSchemaReader.GetSchemaAsync(
                DbProviderType.SqlServer,
                "Data Source=127.0.0.1;Database=DestinationDB;uid=sa;pwd=1q2w3E4r5t6*",
                "DestinationDB"
            );
        
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);
            result.First().Columns.Count().ShouldBeGreaterThan(0);
        }
        
        [Fact]
        public async Task Can_Get_MySql_Schema()
        {
            var result = await _dbSchemaReader.GetSchemaAsync(
                DbProviderType.MySql,
                "Data Source=127.0.0.1;Database=DestinationDB;uid=root;pwd=1q2w3E4r5t6*",
                "DestinationDB"
            );
            
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);
            result.First().Columns.Count().ShouldBeGreaterThan(0);
        }
    }
}