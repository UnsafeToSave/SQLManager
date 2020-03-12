using System;
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

        internal static List<DataSet> GetListDB(string DataSource)
        {
            var ListDB = new List<DataSet>();

            _connectionStr = new SqlConnectionStringBuilder
            {
                DataSource = DataSource,
                IntegratedSecurity = true
            };

            using (IDbConnection connection = new SqlConnection(_connectionStr.ToString()))
            {
                IDbCommand command = new SqlCommand("Select name from sys.databases");
                command.Connection = connection;
                connection.Open();


                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListDB.Add(new DataSet(reader.GetString(0)));
                }
            }

            return ListDB;
        }

        internal static List<DataTable> GetListTables(string dbName)
        {
            var ListTables = new List<DataTable>();

            var DBconnection = new SqlConnectionStringBuilder
            {
                DataSource = _connectionStr.DataSource,
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };

            using (IDbConnection connection = new SqlConnection(DBconnection.ToString()))
            {
                IDbCommand command = new SqlCommand("Select name from sys.tables where type_desc = 'USER_TABLE'");
                command.Connection = connection;
                connection.Open();

                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListTables.Add(new DataTable(reader.GetString(0)));
                }
            }

            return ListTables;
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

        internal static void CreateConnect(string DataSource, string InitialCatalog, string query)
        {
            _connectionStr = new SqlConnectionStringBuilder
            {
                DataSource = DataSource,
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

        internal static DataTable GetTable(string DataSource, string InitialCatalog, string tableName)
        {
            currentTable = new DataTable(tableName);
            string query = $"Select * from {tableName}";

            CreateConnect(DataSource, InitialCatalog, query);

            try
            {
                _adapter.Fill(currentTable);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
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

        internal static void ChangeRow(DataTable table, int index)
        {
            Update();
        }

        private static void Update()
        {
            _adapter.Update(currentTable);
        }
    }
}
