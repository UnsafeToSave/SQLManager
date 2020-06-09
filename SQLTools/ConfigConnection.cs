using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal static class ConfigConnection
    {
        static SqlConnectionStringBuilder _connectionStr;
        internal static void CreateConnectionString(string dataSource, SqlAuthenticationMethod method, string login, string password)
        {
            _connectionStr = new SqlConnectionStringBuilder();
            _connectionStr.Authentication = method;
            _connectionStr.UserID = login;
            _connectionStr.Password = password;
            _connectionStr.DataSource = dataSource;
        }

        internal static SqlConnectionStringBuilder GetConnectionBuilder()
        {
            return _connectionStr;
        }

        internal static string GetConnectionString()
        {
            return _connectionStr.ToString();
        }

        internal static void Disconnect()
        {
            _connectionStr = default;
            TableTools.ClearCurrentTable();
        }

        internal static void CloseConnections()
        {
            SqlConnection.ClearAllPools();
        }


    }
}
