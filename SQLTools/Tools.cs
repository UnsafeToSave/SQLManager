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
        static DataTable currentTable;


        internal static List<string> GetListDB(string DataSource)
        {
            var dbNames = new List<string>();

            _connectionStr = new SqlConnectionStringBuilder
            {
                DataSource = DataSource,
                IntegratedSecurity = true
            };

            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand("Select name from sys.databases");
                command.Connection = connection;
                connection.Open();


                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dbNames.Add(reader.GetString(0));
                }
            }
            return dbNames;
        }

        internal static List<string> GetListTables(string dbName)
        {
            var tableNames = new List<string>();

            var DBconnection = new SqlConnectionStringBuilder
            {
                DataSource = _connectionStr.DataSource,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            using (SqlConnection connection = new SqlConnection(DBconnection.ToString()))
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
            var tableConnection = new SqlConnectionStringBuilder();
            tableConnection.DataSource = _connectionStr.DataSource;
            tableConnection.InitialCatalog = dbName;
            tableConnection.IntegratedSecurity = true;

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

        internal static void ConfigAdapter(out SqlDataAdapter adapter, string query)
        {
            adapter = new SqlDataAdapter(query, _connectionStr.ToString());
            var builder = new SqlCommandBuilder(adapter);
        }

        internal static DataTable GetTable(string InitialCatalog, string tableName)
        {
            currentTable = new DataTable(tableName);
            string query = $"Select * from {tableName}";

            CreateConnect(InitialCatalog, query);

            try
            {
                _adapter.Fill(currentTable);
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
            foreach (var db in GetListDB(_connectionStr.DataSource))
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
                            query += currentTable.Rows[i][j].ToString();
                            if (currentTable.Rows[i][j].ToString() == "true")
                                query += "Null";
                            else
                                query += "Not null";
                        }
                    }
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
                "Имя столбца", 
                "Тип данных", 
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

        internal static void CloseConnections()
        {
            SqlConnection.ClearAllPools();
        }

        internal static bool IsExist(string fullPath)
        {
            //TODO написать метод проверки на существование объекта(базы, таблицы)
            using (SqlConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                //IDbCommand command = new SqlCommand($"Select Count form sys.databases where = '{objectName}'");
                //command.Connection = connection;
                //connection.Open();

                //IDataReader reader = command.ExecuteReader();
                //if (reader.GetInt32(0) >= 1)
                //    return true;
                //else
                //    return false;
                return true;
            }
        }

        internal static bool IsLock(string fullPath)
        {
            return false;
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
