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
    internal static class TableTools
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();
        static SqlDataAdapter _adapter = null;
        static DataTable currentTable;
        static string _value;
        private static int fillingStep = 20000;

        internal static int CurrentRowsCount {
            get
            {
                return currentTable.Rows.Count;
            }
        }

        internal static DataTable GetNewTable(string InitialCatalog, string tableName, int startRows, int maxRows)
        {
            currentTable = new DataTable(tableName);

            string query = $"Select * from {tableName}";

            _adapter = DataAdapter.GetAdapter(InitialCatalog, query);

            try
            { 
                _adapter.Fill(startRows, maxRows, currentTable);
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

        internal static DataTable FillingTable(string InitialCatalog)
        {
            int rowsCount = CurrentRowsCount;
            var newTable = new DataTable(currentTable.TableName);
            string query = $"Select * from {currentTable.TableName}";

            _adapter = DataAdapter.GetAdapter(InitialCatalog, query);
            try
            {
                _adapter.Fill(0, (rowsCount + fillingStep), newTable);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Неизвестная ошибка", e);
            }
            currentTable = newTable;
            return newTable;
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

        private static void Update()
        {
            _adapter.Update(currentTable);
        }

        internal static bool SearchRow(string columnName, string value, int selectRowId, out int rowIndex)
        {
            string query = $"if ((Select Count(*) from {currentTable.TableName} where {columnName} = '{value}') = 1) Select Count(*) from {currentTable.TableName} where {currentTable.Columns[0].ColumnName} <= (select {currentTable.Columns[0].ColumnName} from {currentTable.TableName} where {columnName} = '{value}'); else Select Count(*) from {currentTable.TableName} where {currentTable.Columns[0].ColumnName} <= (select {currentTable.Columns[0].ColumnName} from (select top 1 * from (select * from {currentTable.TableName} where {columnName} = '{value}' and {currentTable.Columns[0].ColumnName} > {currentTable.Rows[selectRowId][0]}) as cutTable) as SelectTopElement)";
            if (Validation.CheckType(value, currentTable.Columns[columnName].DataType))
            {
                rowIndex = IntExectue(_connectionStr.ToString(), query) - 1;
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

        internal static int GetRowsCount(string InitialCatalog, string tableName)
        {
            _connectionStr.InitialCatalog = InitialCatalog;
            string query = $"Select Count(*) from {tableName}";
            return IntExectue(_connectionStr.ToString(), query);
        }

        private static int IntExectue(string connectionStr, string query)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                IDbCommand command = new SqlCommand(query);
                command.Connection = connection;
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                reader.Read();
                result = reader.GetInt32(0);
                CloseConnection(connection);
            }
            return result;
        }

        private static void CloseConnections()
        {
            SqlConnection.ClearAllPools();
        }

        private static void CloseConnection(SqlConnection connection)
        {
            SqlConnection.ClearPool(connection);
        }

        internal static void ClearCurrentTable()
        {
            currentTable = null;
        }


    }
}
