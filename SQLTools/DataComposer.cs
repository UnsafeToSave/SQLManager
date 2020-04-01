using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTools
{
    public interface IDataComposer
    {
        TreeNode[] GetDataBases(string ServerName);
        bool TryGetTable(string dbName, string tableName, out DataTable table);
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
        bool SearchRow(string columnName, string value, out int index);
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


        public TreeNode[] GetDataBases(string ServerName)
        {
            var db = Tools.GetListDB(ServerName);
            var tree = new TreeNode[db.Count];

            for (int i = 0; i < tree.Length; i++)
            {
                tree[i] = new TreeNode(db[i], 0, 1, GetTables(db[i]));
                tree[i].Tag = "DB";
                if(tree[i].GetNodeCount(false) == 0)
                {
                    tree[i].ForeColor = System.Drawing.Color.Gray;
                    tree[i].ImageIndex = 4;
                }
            }
            return tree;
        }

        private TreeNode[] GetTables(string dbName)
        {
            var tables = Tools.GetListTables(dbName);

            var treeChild = new TreeNode[tables.Count];

            for (int i = 0; i < treeChild.Length; i++)
            {
                treeChild[i] = new TreeNode(tables[i], 2, 3);
                treeChild[i].Tag = "Table";
            }
            return treeChild;
        }

        public bool TryGetTable(string dbName, string tableName, out DataTable table)
        {
            var fullPath = dbName + "\\" + tableName;
            if (Tools.IsExist(fullPath))
            {
                table = GetTable(dbName, tableName);
                return true;
            }
            table = default;
            return false;
        }

        private DataTable GetTable(string InitialCatalog, string tableName)
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

        public bool SearchRow(string columnName, string value, out int index)
        {
            return Tools.SearchRow(columnName, value, out  index);
        }

        public bool IsLockDB(string dbName)
        {
            return Tools.IsLockDB(dbName);
        }

        public bool IsExist(string fullPath)
        {
            return Tools.IsExist(fullPath);
        }
    }
}
