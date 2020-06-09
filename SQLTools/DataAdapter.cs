using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTools
{
    internal static class DataAdapter
    {
        static SqlConnectionStringBuilder _connectionStr = ConfigConnection.GetConnectionBuilder();
        static SqlDataAdapter _adapter = null;

        internal static SqlDataAdapter GetAdapter(string InitialCatalog, string query)
        {
            CreateAdapter(InitialCatalog, query);
            return _adapter;
        }

        private static void CreateAdapter(string InitialCatalog, string query)
        {
            _connectionStr.InitialCatalog = InitialCatalog;
            ConfigAdapter(out _adapter, query);
        }

        private static void ConfigAdapter(out SqlDataAdapter adapter, string query)
        {
            adapter = new SqlDataAdapter(query, _connectionStr.ToString());
            var builder = new SqlCommandBuilder(adapter);
        }
        
    }
}
