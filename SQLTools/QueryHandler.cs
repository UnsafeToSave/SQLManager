using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal static class QueryHandler
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();
        static DataTable resultTable;
        internal static DataTable ExecuteQuery (string query)
        {
            resultTable = new DataTable();
            return resultTable;

        }

        internal static void Execute(string IntitialCatalog, string query)
        {

        }
    }
}
