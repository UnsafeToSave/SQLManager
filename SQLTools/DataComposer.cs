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
    public interface IDataComposer
    {
        void Connection(string dataSource, SqlAuthenticationMethod method, string login = "", string password = "");
        TreeNode[] GetDBNames();
        bool TryGetTable(string dbName, string tableName);
        DataTable GetTable(string InitialCatalog, string tableName);
        void DeleteRow(int index);
        void ChangeRow();
        bool CreateDB(string dbName);
        void Disconnected();
        DataTable GetCreateTable();
        bool CreateTable(string dbName, string tableName);
        void DeleteDB(string dbName);
        void DeleteTable(string dbName, string tableName);
        void RenameDB(string selectDB, string newName);
        void RenameTable(string dbName, string tableName, string newName);
        bool SearchRow(string columnName, string value, int selectRowId, out int index);
        void DataFilter(string filter);
        bool IsLockDB(string dbName);
        bool IsExist(string fullPath);
        void CloseApp();
    }

    public class DataComposer : IDataComposer
    {

        public void  CloseApp()
        {
            Tools.CloseConnections();
        }


        public void Connection(string dataSource, SqlAuthenticationMethod method, string login = "", string password = "")
        {
            Tools.ConfigConnection(dataSource, method, login, password);
        }

        public TreeNode[] GetDBNames()
        {
            var db = Tools.GetDBNames();
            var tree = new TreeNode[db.Count];

            
            for (int i = 0; i < tree.Length; i++)
            {
                tree[i] = new TreeNode(db[i], 0, 1, GetTableNames(db[i]))
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
            var tables = Tools.GetTableNames(dbName);

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
            if (Tools.IsExist(fullPath))
            {
                return true;
            }
            return false;
        }

        public  DataTable GetTable(string InitialCatalog, string tableName)
        {
            return Tools.GetTable(InitialCatalog, tableName);
        }

        public void DeleteRow(int index)
        {
            Tools.DeleteRow(index);
        }
         
        public void ChangeRow()
        {
            Tools.ChangeRow();
        }

        public bool CreateDB(string dbName)
        {
            return Tools.CreateDB(dbName);
        }

        public void Disconnected()
        {
            Tools.Disconnect();
        }

        public bool CreateTable(string dbName, string tableName)
        {
            return Tools.CreateTable(dbName, tableName);
        }

        public DataTable GetCreateTable()
        {
            return Tools.GetCreateTable();
        }

        public void DeleteDB(string dbName)
        {
            Tools.DeleteDB(dbName);
        }

        public void DeleteTable(string dbName, string tableName)
        {
            Tools.DeleteTable(dbName, tableName);
        }

        public void RenameDB(string selectDB, string newName)
        {
            Tools.RenameDB(selectDB, newName);
        }

        public void RenameTable(string dbName, string tableName, string newName)
        {
            Tools.RenameTable(dbName, tableName, newName);
        }

        public bool SearchRow(string columnName, string value, int selectRowId, out int index)
        {
            return Tools.SearchRow(columnName, value, selectRowId, out  index);
        }

        public bool IsLockDB(string dbName)
        {
            return Tools.IsLockDB(dbName);
        }

        public bool IsExist(string fullPath)
        {
            return Tools.IsExist(fullPath);
        }

        public void DataFilter (string filter)
        {
           Tools.DataFilter(filter);
        }
    }
}
