namespace iBestRead.Abp.DbSchemaReader
{
    public interface IDbProviderManager
    {
        bool TryGet(DbProviderType dbProviderType, out DbProvider dbProvider);
    }
}