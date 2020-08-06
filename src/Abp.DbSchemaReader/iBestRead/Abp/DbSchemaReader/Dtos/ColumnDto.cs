namespace iBestRead.Abp.DbSchemaReader.Dtos
{
    public class ColumnDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// 当为字符串类型时,数据库设置的长度
        /// </summary>
        public long? DataLength { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool AutoIncrement { get; set; }
    }
}