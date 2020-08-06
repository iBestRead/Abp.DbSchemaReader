using System.Collections.Generic;

namespace iBestRead.Abp.DbSchemaReader.Dtos
{
    public class TableDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 架构名
        /// </summary>
        public string DbSchema { get; set; }
        /// <summary>
        /// 表或视图
        /// 表=T 视图=V
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 主键对应的列
        /// </summary>
        public ColumnDto PrimaryKeyColumn { get; set; }
        /// <summary>
        /// 是否为自增
        /// </summary>
        public bool AutoIncrement { get; set; }
        /// <summary>
        /// 表描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 表的所有列
        /// </summary>
        public IEnumerable<ColumnDto> Columns { get; set; }
    }
}