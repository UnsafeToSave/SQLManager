﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTools
{
    internal static class Tools
    {
        static SqlConnectionStringBuilder _connectionStr;
        static SqlDataAdapter _adapter = null;
        static Dictionary<string, DataTable> cacheTables;
        static DataTable currentTable;

        internal static void ConfigConnection(string dataSource, SqlAuthenticationMethod method, string login, string password)
        {
            _connectionStr = new SqlConnectionStringBuilder();
            _connectionStr.Authentication = method;
            _connectionStr.UserID = login;
            _connectionStr.Password = password;
            _connectionStr.DataSource = dataSource;
        }

        internal static void ConfigAdapter(out SqlDataAdapter adapter, string query)
        {
            adapter = new SqlDataAdapter(query, _connectionStr.ToString());
            var builder = new SqlCommandBuilder(adapter);
        }

        internal static List<string> GetDBNames()
        {
            var dbNames = new List<string>();
            

            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand("Select name from sys.databases");
                command.Connection = connection;
                connection.Open();


                IDataReader reader = command.ExecuteReader(); ;

                while (reader.Read())
                {
                    dbNames.Add(reader.GetString(0));
                }
            }
            return dbNames;
        }

        internal static List<string> GetTableNames(string dbName)
        {
            var tableNames = new List<string>();

            _connectionStr.InitialCatalog = dbName;

            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand("Select name from sys.tables where type_desc = 'USER_TABLE'");
                command.Connection = connection;
                connection.Open();

                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tableNames.Add(reader.GetString(0));
                }
            }
            return tableNames;
        }

        internal static string GetPKColumn(string dbName, string tableName)
        {
            string columnName = default;
            var tableConnection = new SqlConnectionStringBuilder
            {
                DataSource = _connectionStr.DataSource,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            using (IDbConnection connection = new SqlConnection(tableConnection.ToString()))
            {
                IDbCommand command = new SqlCommand($"Select column_name from information_schema.key_column_usage where table_name = '{tableName}' and ordinal_position = 1");
                command.Connection = connection;
                connection.Open();

                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    columnName = reader.GetString(0);
                }
            }
            return columnName;
        }

        internal static void CreateConnect(string InitialCatalog, string query)
        {
            _connectionStr = new SqlConnectionStringBuilder
            {
                DataSource = _connectionStr.DataSource,
                InitialCatalog = InitialCatalog,
                IntegratedSecurity = true
            };

            ConfigAdapter(out _adapter, query);
        }

        internal static DataTable GetTable(string InitialCatalog, string tableName)
        {
            currentTable = new DataTable(tableName);
            
            string query = $"Select * from {tableName}";

            CreateConnect(InitialCatalog, query);

            if (cacheTables == null)
            {
                cacheTables = new Dictionary<string, DataTable>();
            }

            try
            {
                if (!cacheTables.ContainsKey(tableName)) _adapter.Fill(currentTable);
                else cacheTables.TryGetValue(tableName, out currentTable);
                if (currentTable.Rows.Count > 50000 && !cacheTables.ContainsKey(tableName)) cacheTables.Add(tableName, currentTable);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentTable = default;
            }
            catch (Exception e)
            {
                throw new Exception("Неизвестная ошибка", e);
            }

            return currentTable;
        } 

        internal static void DeleteRow(int index)
        {
            currentTable.AcceptChanges();
            currentTable.Rows[index].Delete();
            Update();
        }

        internal static void ChangeRow()
        {
            Update();
        }

        internal static bool CreateDB(string dbName)
        {
            foreach (var db in GetDBNames())
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

        internal static void Disconnect()
        {
            _connectionStr = default;
            CloseConnections();
            ClearStaticData();
        }

        internal static bool CreateTable(string dbName, string tableName)
        {
            if (currentTable.Rows.Count == 0) return false;
            CloseConnections();
            _connectionStr.InitialCatalog = dbName;
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                string query = $"Create table {tableName}(";
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    for(int j = 0; j < currentTable.Columns.Count; j++)
                    {
                        query += " ";
                        if (j != 2)
                        {
                            query += currentTable.Rows[i][j].ToString();
                        }
                        else
                        {
                            if (currentTable.Rows[i][j].ToString() == "true")
                                query += "Null";
                            else
                                query += "Not null";
                        }
                    }
                    if (i == 0)
                        query += " IDENTITY(1,1) Primary Key";
                    if(currentTable.Rows.Count > 1)
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

        internal static DataTable GetCreateTable()
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
            for(int i = 0; i < columnNames.Count; i++)
            {
                columns.Add(new DataColumn(columnNames[i], columnTypes[i]));
            }
            
            foreach(var column in columns)
            {
                table.Columns.Add(column);
            }

            currentTable = table;

            return table;
        }

        internal static void DeleteDB(string dbName)
        {
            CloseConnections();
            _connectionStr.InitialCatalog = "";
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand($"Drop DataBase {dbName}");
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                CloseConnection(connection);
            }
        }

        internal static void DeleteTable(string dbName, string tableName)
        {
            CloseConnections();
            _connectionStr.InitialCatalog = dbName;
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand($"Drop Table {tableName}");
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                CloseConnection(connection);
            }
        }

        internal static void RenameDB(string name, string newName)
        {
            CloseConnections();
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand($"Alter database {name} Modify Name = {newName}");
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                CloseConnection(connection);
            }
        }

        internal static void RenameTable(string dbName, string name, string newName)
        {
            _connectionStr.InitialCatalog = dbName;
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand($"exec sp_rename '{name}', '{newName}'");
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                CloseConnection(connection);
            }
        }

        internal static bool SearchRow(string columnName, string value, int selectRowId, out int rowIndex)
        {
            if (CheckType(value, currentTable.Columns[columnName].DataType))
            {
                using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
                {
                    IDbCommand command = new SqlCommand($"if ((Select Count(*) from {currentTable.TableName} where {columnName} = '{value}') = 1) Select Count(*) from {currentTable.TableName} where {currentTable.Columns[0].ColumnName} <= (select {currentTable.Columns[0].ColumnName} from {currentTable.TableName} where {columnName} = '{value}'); else Select Count(*) from {currentTable.TableName} where {currentTable.Columns[0].ColumnName} <= (select {currentTable.Columns[0].ColumnName} from (select top 1 * from (select * from {currentTable.TableName} where {columnName} = '{value}' and {currentTable.Columns[0].ColumnName} > {currentTable.Rows[selectRowId][0]}) as cutTable) as SelectTopElement)");
                    command.Connection = connection;
                    connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    rowIndex = reader.GetInt32(0) - 1;
                    CloseConnection(connection);
                }
            }
            else
                rowIndex = -1;

            if (rowIndex >= 0)
                return true;
            else
                return false;
        }

        internal static void DataFilter(string filter)
        {
            if (!currentTable.Columns.Contains("_FilterRow"))
            {
                DataColumn ctRowString = currentTable.Columns.Add("_FilterRow", typeof(string));
                foreach (DataRow dataRow in currentTable.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < currentTable.Columns.Count - 1; i++)
                    {
                        sb.Append(dataRow[i].ToString());
                        sb.Append("\t");
                    }
                    dataRow[ctRowString] = sb.ToString();
                }
            }
            currentTable.DefaultView.RowFilter = string.Format("[_FilterRow] LIKE '%{0}%'", filter);
        }

        internal static void CloseConnections()
        {
            SqlConnection.ClearAllPools();
        }

        internal static bool IsExist(string fullPath)
        {
            
            var path = fullPath.Split('\\');
            switch (path.Length)
            {
                case 1:
                    CloseConnections();
                    _connectionStr.InitialCatalog = "";
                    using(SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
                    {
                        IDbCommand command = new SqlCommand($"select Count(name) from sys.databases where name = '{path[0]}'");
                        command.Connection = connection;
                        connection.Open();

                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        if(reader.GetInt32(0) == 1)
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
                            if(reader.GetInt32(0) == 1)
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

        private static bool CheckType(string value, Type type)
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

        private static void Update()
        {
            _adapter.Update(currentTable);
        }

        private static void CloseConnection(SqlConnection connection)
        {
            SqlConnection.ClearPool(connection);
        }

        private static void ClearStaticData()
        {
            _adapter = null;
            currentTable = default;
        }

    }
}
