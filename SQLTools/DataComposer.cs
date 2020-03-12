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

        DataTable GetTable(string ServerName, string selectTable);

        void DeleteRow(int index);

        void ChangeRow(DataTable table, int index);
    }

    public class DataComposer : IDataComposer
    {


        public TreeNode[] GetDataBases(string ServerName)
        {
            var db = Tools.GetListDB(ServerName);
            var tree = new TreeNode[db.Count];

            for (int i = 0; i < tree.Length; i++)
            {
                tree[i] = new TreeNode(db[i].DataSetName, 2, 1, GetTables(db[i].DataSetName));
            }
            return tree;
        }

        private TreeNode[] GetTables(string dbName)
        {
            var tables = Tools.GetListTables(dbName);

            var treeChild = new TreeNode[tables.Count];

            for (int i = 0; i < treeChild.Length; i++)
            {
                treeChild[i] = new TreeNode(tables[i].TableName);
            }
            return treeChild;
        }

        public DataTable GetTable(string ServerName, string selectTable)
        {
            if (selectTable != default)
            {
                string[] names = selectTable.Split('\\');

                return _GetTable(ServerName, names[0], names[1]);
            }

            return default;
        }

        private DataTable _GetTable(string ServerName, string InitialCatalog, string tableName)
        {
            return Tools.GetTable(ServerName, InitialCatalog, tableName);
        }

        public void DeleteRow(int index)
        {
            Tools.DeleteRow(index);
        }

        public void ChangeRow(DataTable table, int index)
        {
            Tools.ChangeRow(table, index);
        }
    }
}
