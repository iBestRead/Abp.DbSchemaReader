using Shouldly;
using Volo.Abp.Testing;
using Xunit;

namespace iBestRead.Abp.DbSchemaReader
{
    public class DbProviderManager_Test : AbpIntegratedTest<AbpDbSchemaReaderTestModule>
    {
        private readonly IDbProviderManager _dbProviderManager;

        public DbProviderManager_Test()
        {
            _dbProviderManager = GetRequiredService<IDbProviderManager>();
        }
        
        [Theory]
        [InlineData(DbProviderType.SqlServer)]
        [InlineData(DbProviderType.MySql)]
        [InlineData(DbProviderType.Oracle)]
        [InlineData(DbProviderType.PostgreSql)]
        public void Can_Get_Provider(DbProviderType dbProviderType)
        {
            var result = _dbProviderManager.TryGet(dbProviderType, out var dbProvider);
        
            result.ShouldBeTrue();
            dbProvider.ShouldNotBeNull();
            dbProvider.Name.ShouldBe(dbProviderType.ToString());
            dbProvider.Factory.ShouldNotBeNull();
        }
    }
}