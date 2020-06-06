using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal class NameChanger
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();

        internal static void RenameDB(string name, string newName)
        {
            CloseConnections();
            string query = $"Alter database {name} Modify Name = {newName}";
            ExecuteQuery(_connectionStr.ToString(), query);
        }

        internal static void RenameTable(string dbName, string name, string newName)
        {
            _connectionStr.InitialCatalog = dbName;
            string query = $"exec sp_rename '{name}', '{newName}'";
            ExecuteQuery(_connectionStr.ToString(), query);
        }


        private static void ExecuteQuery(string connectionStr, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                IDbCommand command = new SqlCommand(query);
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                CloseConnection(connection);
            }
        }

        private static void CloseConnections()
        {
            SqlConnection.ClearAllPools();
        }

        private static void CloseConnection(SqlConnection connection)
        {
            SqlConnection.ClearPool(connection);
        }
    }
}
