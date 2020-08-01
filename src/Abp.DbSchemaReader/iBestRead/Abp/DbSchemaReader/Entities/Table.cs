using System;
using System.Collections.Generic;
using System.Linq;

namespace iBestRead.Abp.DbSchemaReader.Entities
{
    public class Table
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
        
        public TableType Type
        {
            get
            {
                switch (TypeName.Trim())
                {
                    case "T": return TableType.Table;
                    case "V": return TableType.View;
                    default: throw new ArgumentException("参数错误！", "Table.TypeName");
                }
            }
        }
        
        public Column PrimaryKeyColumn { get { return Columns.FirstOrDefault(m => m.IsPrimaryKey); } }
        public bool AutoIncrement { get { return Columns.Any(m => m.AutoIncrement); } }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public IEnumerable<Column> Columns { get; set; }
        
    }
}
