namespace iBestRead.Abp.DbSchemaReader
{
    public interface ISqlLoader
    {
        QuerySql Get(DbProviderType dbProviderType);
    }
}