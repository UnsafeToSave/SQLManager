using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTools
{
    public interface ITools
    {
        void Connection(string dataSource, SqlAuthenticationMethod method, string login = "", string password = "");
        Task<TreeNode[]> GetDBNames();
        bool TryGetTable(string dbName, string tableName);
        Task<DataTable> GetNewTable(string InitialCatalog, string tableName, int startRow = 0, int maxRows = 1000);
        Task<DataTable> FillingTable(string IntitialCatalog);
        void DeleteRow(int index);
        void ChangeRow();
        bool CreateDB(string dbName);
        void Disconnected();
        DataTable GetCreatorTable();
        bool CreateTable(string dbName, string tableName);
        void DeleteDB(string dbName);
        void DeleteTable(string dbName, string tableName);
        void RenameDB(string selectDB, string newName);
        void RenameTable(string dbName, string tableName, string newName);
        bool SearchRow(string columnName, string value, int selectRowId, out int index);
        void DataFilter(string filter);
        bool IsLockDB(string dbName);
        bool IsExist(string fullPath);
        Task<bool> IsFullTable(string InitialCatalog, string tableName);
        Task<int> GetRowsCount(string InitialCatalog, string tableName);
        void CloseApp();
    }

    public class SqlTools : ITools
    {

        public void  CloseApp()
        {
            ConfigConnection.CloseConnections();
        }


        public void Connection(string dataSource, SqlAuthenticationMethod method, string login = "", string password = "")
        {
            ConfigConnection.CreateConnectionString(dataSource, method, login, password);
        }

        public async Task<TreeNode[]> GetDBNames()
        {
            var db = await Task.Run(()=> GetListNames.GetDBNames());
            var tree = new TreeNode[db.Count];

            
            for (int i = 0; i < tree.Length; i++)
            {
                tree[i] = new TreeNode(db[i], 0, 1, await Task.Run(()=>GetTableNames(db[i])))
                {
                    Tag = "DB"
                };
                if (tree[i].GetNodeCount(false) == 0)
                {
                    tree[i].ForeColor = System.Drawing.Color.Gray;
                    tree[i].ImageIndex = 4;
                }
            }
            return tree;
        }

        private TreeNode[] GetTableNames(string dbName)
        {
            var tables = GetListNames.GetTableNames(dbName);

            var treeChild = new TreeNode[tables.Count];

            for (int i = 0; i < treeChild.Length; i++)
            {
                treeChild[i] = new TreeNode(tables[i], 2, 3)
                {
                    Tag = "Table"
                };
            }
            return treeChild;
        }

        public bool TryGetTable(string dbName, string tableName)
        {
            var fullPath = dbName + "\\" + tableName;
            if (Validation.IsExist(fullPath))
            {
                return true;
            }
            return false;
        }

        public async Task<DataTable> GetNewTable(string InitialCatalog, string tableName, int startRow = 0, int maxRows = 1000)
        {
           return await Task.Run(() => TableTools.GetNewTable(InitialCatalog, tableName, startRow, maxRows));
        }

        public async Task<DataTable> FillingTable(string InitialCatalog)
        {
            return await Task.Run(() => TableTools.FillingTable(InitialCatalog));
        }

        public void DeleteRow(int index)
        {
            TableTools.DeleteRow(index);
        }
         
        public void ChangeRow()
        {
            TableTools.ChangeRow();
        }

        public bool CreateDB(string dbName)
        {
            return CreateObject.CreateDB(dbName);
        }

        public void Disconnected()
        {
            ConfigConnection.Disconnect();
        }

        public bool CreateTable(string dbName, string tableName)
        {
            return CreateObject.CreateTable(dbName, tableName);
        }

        public DataTable GetCreatorTable()
        {
            return CreateObject.GetCreatorTable();
        }

        public void DeleteDB(string dbName)
        {
            DeleteObject.DeleteDB(dbName);
        }

        public void DeleteTable(string dbName, string tableName)
        {
            DeleteObject.DeleteTable(dbName, tableName);
        }

        public void RenameDB(string selectDB, string newName)
        {
            NameChanger.RenameDB(selectDB, newName);
        }

        public void RenameTable(string dbName, string tableName, string newName)
        {
            NameChanger.RenameTable(dbName, tableName, newName);
        }

        public bool SearchRow(string columnName, string value, int selectRowId, out int index)
        {
            return TableTools.SearchRow(columnName, value, selectRowId, out  index);
        }

        public bool IsLockDB(string dbName)
        {
            return Validation.IsLockDB(dbName);
        }

        public bool IsExist(string fullPath)
        {
            return Validation.IsExist(fullPath);
        }

        public async Task<bool> IsFullTable(string InitialCatalog, string tableName)
        {
            return await Validation.IsFullTable(InitialCatalog, tableName);
        }

        public void DataFilter (string filter)
        {
           TableTools.DataFilter(filter);
        }

        public async Task<int> GetRowsCount (string InitialCatalog, string tableName)
        {
            return await Task.Run(() => TableTools.GetRowsCount(InitialCatalog, tableName));
        }
    }
}
