namespace iBestRead.Abp.DbSchemaReader
{
    public class QuerySql
    {
        public string Table { get;  }
        
        public string Column { get;  }

        public QuerySql(string table, string column)
        {
            Table = table;
            Column = column;
        }
    }
}