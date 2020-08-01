using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace iBestRead.Abp.DbSchemaReader
{
    [DependsOn(
        typeof(AbpVirtualFileSystemModule)
    )]
    public class AbpDbSchemaReaderModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpDbSchemaReaderModule>();
            });
        }
    }
}