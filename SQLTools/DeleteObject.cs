using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal static class DeleteObject
    {

        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();

        internal static void DeleteDB(string dbName)
        {
            CloseConnections();
            _connectionStr.InitialCatalog = "";
            string query = $"Drop DataBase [{dbName}]";
            ExecuteQuery(_connectionStr.ToString(), query);
        }

        internal static void DeleteTable(string dbName, string tableName)
        {
            CloseConnections();
            _connectionStr.InitialCatalog = dbName;
            string query = $"Drop Table [{tableName}]";
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
