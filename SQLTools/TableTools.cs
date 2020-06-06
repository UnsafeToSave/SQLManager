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

        internal static DataTable GetTable(string InitialCatalog, string tableName)
        {
            currentTable = new DataTable(tableName);

            string query = $"Select * from {tableName}";

            _adapter = DataAdapter.GetAdapter(InitialCatalog, query);
            

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
            if (Validation.CheckType(value, currentTable.Columns[columnName].DataType))
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
