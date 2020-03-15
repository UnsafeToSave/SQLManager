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
        bool TryGetTable(string selectTable, out DataTable table);
        void DeleteRow(int index);
        void ChangeRow();
        bool CreateDB(string dbName);
        void Disconnected();
        DataTable GetCreateTable();
        bool CreateTable(string dbName, string tableName);
        void DeleteDB(string dbName);
        void DeleteTable(string selectTable);
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

        public bool TryGetTable(string selectTable, out DataTable table)
        {
            //TODO написать проверку на существование каталога и таблицы;
            string[] selectInfo = selectTable.Split('\\');
            table = GetTable(selectInfo[0], selectInfo[1]);
            return true;
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

        public void DeleteTable(string selectTable)
        {
            string[] selectInfo = selectTable.Split('\\');
            Tools.DeleteTable(selectInfo[0], selectInfo[1]);
        }
    }
}
