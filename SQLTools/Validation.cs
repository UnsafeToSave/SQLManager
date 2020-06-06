using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal static class Validation
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();

        internal static bool IsExist(string fullPath)
        {

            var path = fullPath.Split('\\');
            switch (path.Length)
            {
                case 1:
                    CloseConnections();
                    _connectionStr.InitialCatalog = "";
                    using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
                    {
                        IDbCommand command = new SqlCommand($"select Count(name) from sys.databases where name = '{path[0]}'");
                        command.Connection = connection;
                        connection.Open();

                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        if (reader.GetInt32(0) == 1)
                        {
                            CloseConnection(connection);
                            return true;
                        }
                        CloseConnection(connection);
                        return false;
                    }
                case 2:
                    if (IsExist(path[0]))
                    {
                        CloseConnections();
                        _connectionStr.InitialCatalog = path[0];
                        using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
                        {
                            IDbCommand command = new SqlCommand($"Select Count(name) from sys.tables where name = '{path[1]}'");
                            command.Connection = connection;
                            connection.Open();

                            IDataReader reader = command.ExecuteReader();
                            reader.Read();
                            if (reader.GetInt32(0) == 1)
                            {
                                CloseConnection(connection);
                                return true;
                            }
                            CloseConnection(connection);
                            return false;
                        }
                    }
                    return false;
            }
            return false;
        }

        internal static bool IsLockDB(string dbName)
        {
            CloseConnections();
            _connectionStr.InitialCatalog = "";
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand($"select Count(Distinct dbid) from sys.sysprocesses where db_name(dbid) = '{dbName}'");
                command.Connection = connection;
                connection.Open();

                IDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.GetInt32(0) == 1)
                {
                    CloseConnection(connection);
                    return true;
                }
                CloseConnection(connection);
                return false;
            }
        }

        internal static bool CheckType(string value, Type type)
        {
            switch (type.Name)
            {
                case "String":
                    return value as string != null;
                case "Int32":
                    return int.TryParse(value, out _);
                case "Boolean":
                    return bool.TryParse(value, out _);
                case "TimeSpan":
                    return TimeSpan.TryParse(value, out TimeSpan _);
                case "DateTime":
                    return DateTime.TryParse(value, out DateTime _);
                case "Decimal":
                    return decimal.TryParse(value, out decimal _);
                default:
                    return false;
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
