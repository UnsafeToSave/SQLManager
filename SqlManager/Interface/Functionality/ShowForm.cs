using SqlManager.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlManager.InterfaceHandler
{
    public static class ShowForm
    {

        public static void ShowMainForm()
        {
            var main = FormContainer.mainForm;

            TableHandler.ContentMode = Mode.Viewer;
            main.TreeViewExplorer.ImageList = main.Images;

            main.MenuPanel.MouseDown += Menu.MoveForm;
            main.MenuPanel.DoubleClick += Menu.MaximizeWindow;
            main.btnClose.Click += Menu.CloseForm;
            main.btnMinimize.Click += Menu.MinimizeWindow;


            main.TreeViewExplorer.AfterSelect += main.TreeViewExplorer_AfterSelect;
            main.Table.KeyDown += main.Table_HotKeyDown;
            main.btnAddDB.Click += main.ShowDBForm;
            main.btnRefresh.Click += main.Refresh;
            main.btnDeleteDB.Click += main.DeleteDB;
            main.btnDisconnect.Click += main.Disconnection;
            main.Table.SelectionChanged += main.Table_SelectionChanged;
            main.Table.DataError += main.Table_DataError;
            main.Table.CellValueChanged += main.Table_CellValueChanged;
            main.TreeViewExplorer.MouseClick += main.TreeViewExplorer_MouseClick;
            main.CreateTableTSMItem.Click += main.CreateNewTable;
            main.DeleteDBTSMItem.Click += main.DeleteDB;
            main.RenameDBTSMItem.Click += main.Rename;
            main.DeleteTableTSMItem.Click += main.DeleteTable;
            main.RenameTableTSMItem.Click += main.Rename;
            main.DeleteRowTSMItem.Click += main.DeleteRow;
            main.FindValueTSMItem.Click += main.ShowSearchForm;
            main.FilterTSMItem.Click += main.ShowFilterForm;
            main.Table.MouseClick += main.ShowTableContextMenu;
            main.Table.Scroll += main.TableScroll;

        }

        public static void ShowDBForm(object sender, EventArgs e)
        {
            if (FormContainer.dbForm == null)
            {
                FormContainer.dbForm = new DBForm();
                FormContainer.dbForm.MenuPanel.MouseDown += Menu.MoveForm;
                FormContainer.dbForm.btnClose.Click += Menu.CloseForm;
                FormContainer.dbForm.btnActionDB.Click += FormContainer.mainForm.CreateDB;
                FormContainer.dbForm.fldDBName.KeyDown += FormContainer.mainForm.CreateDB;
            }
            FormContainer.dbForm.fldDBName.Text = "";
            FormContainer.dbForm.ShowDialog(FormContainer.mainForm);
        }

        public static void ShowTableForm(object sender, EventArgs e)
        {
            if (FormContainer.tableForm == null)
            {
                FormContainer.tableForm = new TableForm();
            }
            FormContainer.tableForm.btnClose.Click += Menu.CloseForm;
            FormContainer.tableForm.MenuPanel.MouseDown += Menu.MoveForm;
            FormContainer.tableForm.btnActionTable.Click += TableHandler.SaveNewTable;
            FormContainer.tableForm.fldTableName.Text = "";
            FormContainer.tableForm.ShowDialog(FormContainer.mainForm);
        }

        public static void ShowFilterForm(object sender, EventArgs e)
        {
            if (FormContainer.filterForm == null)
            {
                FormContainer.filterForm = new FilterForm();
                FormContainer.filterForm.btnClose.Click += Menu.CloseForm;
                FormContainer.filterForm.MenuPanel.MouseDown += Menu.MoveForm;
                FormContainer.filterForm.fldFilter.TextChanged += FormContainer.mainForm.DataFilter;
            }
            FormContainer.filterForm.ShowDialog(FormContainer.mainForm);
        }

        public static void ShowConnectionForm(object sender, EventArgs e)
        {
            if (FormContainer.connectionForm == null)
            {
                FormContainer.connectionForm = new ConnectionForm();
                FormContainer.connectionForm.btnClose.Click += Menu.CloseForm;
                FormContainer.connectionForm.MenuPanel.MouseDown += Menu.MoveForm;
                FormContainer.connectionForm.btnConnection.Click += FormContainer.mainForm.Connection;
                FormContainer.connectionForm.fldConnectionString.KeyDown += FormContainer.mainForm.Connection;
                FormContainer.connectionForm.Authentication.SelectedIndexChanged += FormContainer.mainForm.AuthenticationChange;
            }
            FormContainer.connectionForm.Authentication.SelectedIndex = 0;

            FormContainer.connectionForm.Show(FormContainer.mainForm);
        }

        public static void ShowSearchForm(object sender, EventArgs e)
        {
            if (FormContainer.searchForm == null)
            {
                FormContainer.searchForm = new SearchForm();
                FormContainer.searchForm.btnClose.Click += Menu.CloseForm;
                FormContainer.searchForm.MenuPanel.MouseDown += Menu.MoveForm;
                FormContainer.searchForm.btnSearch.Click += FormContainer.mainForm.SearchRow;
                FormContainer.searchForm.fldSearch.KeyDown += FormContainer.mainForm.SearchRow;
            }
            FormContainer.searchForm.cmbSearch.Items.Clear();
            FormContainer.searchForm.fldSearch.Text = "";
            for (int i = 0; i < FormContainer.mainForm.Table.Columns.Count; i++)
            {
                if (FormContainer.mainForm.Table.Columns[i].HeaderText == "_FilterRow") continue;
                FormContainer.searchForm.cmbSearch.Items.Add(FormContainer.mainForm.Table.Columns[i].HeaderText);
            }
            FormContainer.searchForm.cmbSearch.SelectedIndex = 0;
            FormContainer.searchForm.ShowDialog(FormContainer.mainForm);
        }

        public static void ShowQueryForm(object sender, EventArgs e)
        {
            if (FormContainer.queryForm == null)
            {
                FormContainer.queryForm = new QueryForm();
                FormContainer.queryForm.QueryField.KeyDown += FormContainer.mainForm.ExecuteQuery;
                FormContainer.queryForm.btnClose.Click += Menu.CloseForm;
                FormContainer.queryForm.MenuPanel.MouseDown += Menu.MoveForm;
            }
            FormContainer.queryForm.ShowDialog(FormContainer.mainForm);
        }
    }
}
