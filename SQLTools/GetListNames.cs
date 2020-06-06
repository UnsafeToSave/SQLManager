using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal class GetListNames
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();
        static List<string> result;

        internal static List<string> GetDBNames()
        {
            string query = "Select name from sys.databases";
            ExecuteQuery(_connectionStr.ToString(), query);
            return result;
        }

        internal static List<string> GetTableNames(string dbName)
        {
            _connectionStr.InitialCatalog = dbName;
            string query = "Select name from sys.tables where type_desc = 'USER_TABLE'";
            ExecuteQuery(_connectionStr.ToString(), query);
            return result;
        }

        private static void ExecuteQuery(string connectionStr, string query)
        {
            result = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                IDbCommand command = new SqlCommand(query);
                command.Connection = connection;
                connection.Open();

                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }
        }
    }
}
