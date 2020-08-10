using iBestRead.Database.Consts;
using Shouldly;
using Volo.Abp.Testing;
using Xunit;

namespace iBestRead.Abp.DbSchemaReader
{
    public class SqlLoader_Tests : AbpIntegratedTest<AbpDbSchemaReaderTestModule>
    {
        private readonly ISqlLoader _sqlLoader;

        public SqlLoader_Tests()
        {
            _sqlLoader = GetRequiredService<ISqlLoader>();
        }
        
        [Theory]
        [InlineData(DbProviderType.SqlServer)]
        [InlineData(DbProviderType.MySql)]
        [InlineData(DbProviderType.Oracle)]
        [InlineData(DbProviderType.PostgreSql)]
        public void Can_Get_Sql(DbProviderType dbProviderType)
        {
            var result = _sqlLoader.Get(dbProviderType);
        
            result.ShouldNotBeNull();
            result.Table.ShouldNotBeNullOrEmpty();
            result.Column.ShouldNotBeNullOrEmpty();
        }
    }
}