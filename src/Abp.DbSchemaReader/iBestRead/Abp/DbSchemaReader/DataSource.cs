// using System.Data.Common;
//
// namespace iBestRead.Abp.DbSchemaReader
// {
//     public class DataSource
//     {
//         /// <summary>
//         /// 数据源名称
//         /// </summary>
//         public string Name { get; set; }
//         
//         /// <summary>
//         /// 数据源链接字符串
//         /// </summary>
//         public string ConnectionString { get; set; }
//
//         public DbProvider DbProvider { get; set; }
//
//         public virtual DbConnection CreateConnection()
//         {
//             var dbConnection = DbProvider.Factory.CreateConnection();
//             if (null == dbConnection)
//                 return null;
//             
//             dbConnection.ConnectionString = ConnectionString;
//             return dbConnection;
//         }
//     }
// }