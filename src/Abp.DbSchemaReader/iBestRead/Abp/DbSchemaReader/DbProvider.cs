using System.Data.Common;
using iBestRead.Database.Consts;

namespace iBestRead.Abp.DbSchemaReader
{
    public class DbProvider
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DbProviderType
        /// </summary>
        public DbProviderType DbProviderType { get; set; }

        /// <summary>
        /// 不同的Db参数化查询时,参数前缀不一样.
        /// SqlServer PostgreSql 通常@作为前缀
        /// Mysql 通常为?
        /// Oracle 通常为:
        /// </summary>
        public string ParameterPrefix { get; set; }
        /// <summary>
        /// DbProvider工厂
        /// </summary>
        public DbProviderFactory Factory { get; set; }

        public DbProvider(DbProviderType dbProviderType, string parameterPrefix, DbProviderFactory factory)
        {
            DbProviderType = dbProviderType;
            Name = dbProviderType.ToString();
            ParameterPrefix = parameterPrefix;
            Factory = factory;
        }
    }
}