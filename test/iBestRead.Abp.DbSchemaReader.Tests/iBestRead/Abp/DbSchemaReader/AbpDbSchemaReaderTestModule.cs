using Volo.Abp.Modularity;

namespace iBestRead.Abp.DbSchemaReader
{
    [DependsOn(typeof(AbpDbSchemaReaderModule))]
    public class AbpDbSchemaReaderTestModule: AbpModule
    {
        
    }
}