using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal static class CreateObject
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();
        static DataTable creatorTable;

        internal static bool CreateDB(string dbName)
        {
            foreach (var db in GetListNames.GetDBNames())
            {
                if (db == dbName)
                {
                    return false;
                }
            }

            CloseConnections();

            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand($"Create DataBase {dbName}");
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                CloseConnection(connection);
            }

            return true;
        }

        internal static bool CreateTable(string dbName, string tableName)
        {
            if (creatorTable.Rows.Count == 0) return false;
            CloseConnections();
            _connectionStr.InitialCatalog = dbName;
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                string query = $"Create table {tableName}(";
                for (int i = 0; i < creatorTable.Rows.Count; i++)
                {
                    for (int j = 0; j < creatorTable.Columns.Count; j++)
                    {
                        query += " ";
                        if (j != 2)
                        {
                            query += creatorTable.Rows[i][j].ToString();
                        }
                        else
                        {
                            if (creatorTable.Rows[i][j].ToString() == "true")
                                query += "Null";
                            else
                                query += "Not null";
                        }
                    }
                    if (i == 0)
                        query += " IDENTITY(1,1) Primary Key";
                    if (creatorTable.Rows.Count > 1)
                        query += ",";
                }
                query += ");";
                IDbCommand command = new SqlCommand($"Select count(TABLE_NAME) from information_schema.TABLES where TABLE_NAME = '{tableName}'");
                command.Connection = connection;
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                reader.Read();
                bool IsExistTable = reader.GetInt32(0) == 0;
                reader.Close();
                if (IsExistTable)
                {
                    command = new SqlCommand(query);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
        }

        internal static DataTable GetCreatorTable()
        {
            ClearStaticData();
            var table = new DataTable("Creator");
            List<DataColumn> columns = new List<DataColumn>();
            List<string> columnNames = new List<string>() {
                "Name",
                "Type",
                "Nullable" };
            List<Type> columnTypes = new List<Type>() {
                typeof(string),
                typeof(string),
                typeof(bool) };
            for (int i = 0; i < columnNames.Count; i++)
            {
                columns.Add(new DataColumn(columnNames[i], columnTypes[i]));
            }

            foreach (var column in columns)
            {
                table.Columns.Add(column);
            }

            creatorTable = table;

            return table;
        }

        private static void CloseConnections()
        {
            SqlConnection.ClearAllPools();
        }

        private static void CloseConnection(SqlConnection connection)
        {
            SqlConnection.ClearPool(connection);
        }

        private static void ClearStaticData()
        {
            DataAdapter.DeleteAdapter();
            TableTools.ClearCurrentTable();
        }
    }
}
