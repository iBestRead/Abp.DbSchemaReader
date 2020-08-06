using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace iBestRead.Abp.DbSchemaReader
{
    [DependsOn(
        typeof(AbpVirtualFileSystemModule),
        typeof(AbpAutoMapperModule)
    )]
    public class AbpDbSchemaReaderModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpDbSchemaReaderModule>();
            });
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpDbSchemaReaderModule>();
            });
        }
    }
}