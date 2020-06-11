using SqlManager.Forms;
using SqlManager.InterfaceHandler;
using menu = SqlManager.InterfaceHandler.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SqlManager
{
    public enum Mode
    {
        Viewer,
        Creator,
    }

    public interface IMainForm
    {
        string ServerName { get; }
        string DBName { set; get; }
        string TableName { set; get; }
        string CurrentDB { get; set; }
        string CurrentTable { get; }
        string FullPath { get; }
        string SearchColumn { get; }
        string SearchValue { get; }
        string Filter { get; }
        string Login { get; }
        string Password { get; }
        string Query { get; }
        int SelectedRowIndex { get; set; }
        bool IsFull { get; set; }
        bool IsSearch { get; set; }
        TreeNode[] Explorer { set; }
        DataTable Content { set; }
        DataTable CurrentRow { get; }
        SqlAuthenticationMethod Authentication { get; }



        event EventHandler Connected;
        event EventHandler RowDeleted;
        event EventHandler RowChanged;
        event EventHandler TableSelected;
        event EventHandler DBCreated;
        event EventHandler Refreshed;
        event EventHandler Disconnected;
        event EventHandler ApplicationClose;
        event EventHandler TableCreated;
        event EventHandler TableCreate;
        event EventHandler DBDeleted;
        event EventHandler TableDeleted;
        event EventHandler DBRenamed;
        event EventHandler TableRenamed;
        event EventHandler RowSearched;
        event EventHandler DataFiltered;
        event EventHandler UploadRows;
        event EventHandler QueryExecute;

    }
    
    public partial class MainForm : Form, IMainForm
    {
        ImageList interfaceImages;

        public int ScrollPointer { get; set; }
        
        bool isFull = false;
        bool isSearch = false;
        string currentDB { get; set; }

        #region Реализация интерфейса IMainForm
        public string ServerName
        {
            get
            {
                return FormContainer.connectionForm.fldConnectionString.Text;
            }
        }
        public string DBName
        {
            set
            {
                if (FormContainer.dbForm == null)
                {
                    FormContainer.dbForm = new DBForm();
                }
                FormContainer.dbForm.fldDBName.Text = value;
            }
            get
            {
                return FormContainer.dbForm.fldDBName.Text;
            }
        }
        public string TableName
        {
            set
            {
                if (FormContainer.tableForm == null)
                {
                    FormContainer.tableForm = new TableForm();
                }
                FormContainer.tableForm.fldTableName.Text = value;
            }
            get
            {
                return FormContainer.tableForm.fldTableName.Text;
            }
        }
        public string CurrentDB
        {
            get
            {
                return currentDB;
            }
            set
            {
                currentDB = value;
            }
        }
        public string CurrentTable
        {
            get
            {
                if (TreeViewExplorer.SelectedNode.Level != 0)
                    return TreeViewExplorer.SelectedNode.FullPath.Split('\\')[1];
                return default;
            }
        }
        public string FullPath
        {
            get
            {
                return TreeViewExplorer.SelectedNode.FullPath;
            }
        }
        public string SearchColumn
        {
            get
            {
                return FormContainer.searchForm.cmbSearch.SelectedItem.ToString();
            }
        }
        public string SearchValue
        {
            get
            {
                return FormContainer.searchForm.fldSearch.Text;
            }
        }
        public string Filter
        {
            get
            {
                return FormContainer.filterForm.fldFilter.Text;
            }
        }
        public string Login 
        { 
            get
            {
                if (FormContainer.connectionForm != null)
                {
                    return FormContainer.connectionForm.fldLogin.Text;
                }
                else
                    throw new Exception("Ошибка создания формы соединения");
            } 
        }
        public string Password
        {
            get
            {
                if (FormContainer.connectionForm != null)
                {
                    return FormContainer.connectionForm.fldPass.Text;
                }
                else
                    throw new Exception("Ошибка создания формы соединения");
            }
        }
        public string Query
        {
            get
            {
                return FormContainer.queryForm.QueryField.Text;
            }
        }
        public int SelectedRowIndex
        {
            set
            {
                Table.Rows[value].Selected = true;
                Table.FirstDisplayedScrollingRowIndex = Table.SelectedRows[0].Index;
            }
            get
            {
                if(Table.Rows.Count - 1 != Table.SelectedRows[0].Index)
                {
                    return Table.SelectedRows[0].Index;
                }
                return Table.SelectedRows[0].Index - 1;
            }
        }
        public bool IsFull
        {
            get
            {
                return isFull;
            }

            set
            {
                isFull = value;
            }
        }
        public bool IsSearch
        {
            get
            {
                return isSearch;
            }
            set
            {
                isSearch = value;
            }
        }
        
        public TreeNode[] Explorer
        {
            set
            {
                if (TreeViewExplorer.Nodes.Count == 0)
                    TreeViewExplorer.Nodes.AddRange(value);
                else
                {
                    TreeViewExplorer.Nodes.Clear();
                    TreeViewExplorer.Nodes.AddRange(value);
                }

            }
        }
        public DataTable Content
        {
            set
            {
                
                if (value is DataTable)
                {
                    Table.DataSource = value;
                    Table.Update();
                    if (Table.Name == value.TableName)
                    {
                        Table.FirstDisplayedScrollingRowIndex = ScrollPointer;
                        TableHandler.rowsIsAdd = false;
                    }
                    Table.Name = value.TableName;
                }
                
            }
        }
        public DataTable CurrentRow
        {
            get
            {
                var table = new DataTable();
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    table.Columns.Add(new DataColumn(Table.Columns[i].HeaderText));
                }
                DataRow row = table.NewRow();
                int index = Table.CurrentRow.Index;
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    row[i] = Table.Rows[index].Cells[i].Value;
                }
                table.Rows.Add(row);
                return table;
            }
        }
        public SqlAuthenticationMethod Authentication
        {
            get
            {
                if (FormContainer.connectionForm != null)
                {
                    if (FormContainer.connectionForm.Authentication.Items[FormContainer.connectionForm.Authentication.SelectedIndex].ToString() == "Проверка подлинности SQL Server")
                    {
                        return SqlAuthenticationMethod.SqlPassword;
                    }
                    else
                    {
                        return SqlAuthenticationMethod.NotSpecified;
                    }
                }
                else
                    throw new Exception("Ошибка создания формы соединения");
            }
        }

        public event EventHandler Connected;
        public event EventHandler RowDeleted;
        public event EventHandler RowChanged;
        public event EventHandler TableSelected;
        public event EventHandler DBCreated;
        public event EventHandler Refreshed;
        public event EventHandler Disconnected;
        public event EventHandler ApplicationClose;
        public event EventHandler TableCreated;
        public event EventHandler TableCreate;
        public event EventHandler DBDeleted;
        public event EventHandler TableDeleted;
        public event EventHandler DBRenamed;
        public event EventHandler TableRenamed;
        public event EventHandler RowSearched;
        public event EventHandler DataFiltered;
        public event EventHandler UploadRows;
        public event EventHandler QueryExecute;
        #endregion

        #region Меню
        public void MoveForm(object sender, MouseEventArgs e)
        {
            Message m = menu.MoveForm(sender,e);
            this.WndProc(ref m);
        }
        private void CloseForm(object sender, EventArgs e)
        {
            menu.CloseForm(sender, e);
        }
        private void MinimizeWindow(object sender, EventArgs e)
        {
            menu.MinimizeWindow(sender, e);
        }
        private void MaximizeWindow(object sender, EventArgs e)
        {
            menu.MaximizeWindow(sender, e);
        }
        #endregion

        public MainForm()
        {
            LoadResources();
            InitializeComponent();
            ShowConnectionForm();



            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;
            
        }

        private void LoadResources()
        {
            interfaceImages = new ImageList();
            interfaceImages.Images.Add(Image.FromFile("Assets\\Images\\DBIcon.png"));
            interfaceImages.Images.Add(Image.FromFile("Assets\\Images\\SelectDBIcon.png"));
            interfaceImages.Images.Add(Image.FromFile("Assets\\Images\\TableIcon.png"));
            interfaceImages.Images.Add(Image.FromFile("Assets\\Images\\SelectTableIcon.png"));
            interfaceImages.Images.Add(Image.FromFile("Assets\\Images\\EmptyDBIcon.png"));
        }

        private void ShowConnectionForm()
        {
            if (FormContainer.connectionForm == null)
            {
                FormContainer.connectionForm = new ConnectionForm();
                FormContainer.connectionForm.btnClose.Click += CloseForm;
                FormContainer.connectionForm.MenuPanel.MouseDown += MoveForm;
                FormContainer.connectionForm.btnConnection.Click += Connection;
                FormContainer.connectionForm.fldConnectionString.KeyDown += Connection;
                FormContainer.connectionForm.Authentication.SelectedIndexChanged += AuthenticationChange;
            }
            FormContainer.connectionForm.Authentication.SelectedIndex = 0;

            FormContainer.connectionForm.Show(this);

        }


        private void ShowDBForm(object sender, EventArgs e)
        {
            ShowForm.ShowDBForm(sender, e);
        }
        public void ShowTableForm(object sender, EventArgs e)
        {
            if (FormContainer.tableForm == null)
            {
                FormContainer.tableForm = new TableForm();
            }
            FormContainer.tableForm.btnClose.Click += CloseForm;
            FormContainer.tableForm.MenuPanel.MouseDown += MoveForm;
            FormContainer.tableForm.btnActionTable.Click += SaveNewTable;
            FormContainer.tableForm.fldTableName.Text = "";
            FormContainer.tableForm.ShowDialog(this);
        }
        public void ShowSearchForm(object sender, EventArgs e)
        {
            if (FormContainer.searchForm == null)
            {
                FormContainer.searchForm = new SearchForm();
                FormContainer.searchForm.btnClose.Click += CloseForm;
                FormContainer.searchForm.MenuPanel.MouseDown += MoveForm;
                FormContainer.searchForm.btnSearch.Click += SearchRow;
                FormContainer.searchForm.fldSearch.KeyDown += SearchRow;
            }
            FormContainer.searchForm.cmbSearch.Items.Clear();
            FormContainer.searchForm.fldSearch.Text = "";
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                if (Table.Columns[i].HeaderText == "_FilterRow") continue;
                FormContainer.searchForm.cmbSearch.Items.Add(Table.Columns[i].HeaderText);
            }
            FormContainer.searchForm.cmbSearch.SelectedIndex = 0;
            FormContainer.searchForm.ShowDialog(this);
        }
        private void ShowFilterForm(object sender, EventArgs e)
        {
            if(FormContainer.filterForm == null)
            {
                FormContainer.filterForm = new FilterForm();
                FormContainer.filterForm.btnClose.Click += CloseForm;
                FormContainer.filterForm.MenuPanel.MouseDown += MoveForm;
                FormContainer.filterForm.fldFilter.TextChanged += DataFilter;
            }
            FormContainer.filterForm.ShowDialog(this);
        }
        private void ShowQueryForm(object sender, EventArgs e)
        {
            if(FormContainer.queryForm == null)
            {
                FormContainer.queryForm = new QueryForm();
                FormContainer.queryForm.QueryField.KeyDown += ExecuteQuery;
                FormContainer.queryForm.btnClose.Click += CloseForm;
                FormContainer.queryForm.MenuPanel.MouseDown += MoveForm;
            }
            FormContainer.queryForm.ShowDialog(this);
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            TableHandler.ContentMode = Mode.Viewer;
            TreeViewExplorer.ImageList = interfaceImages;

            MenuPanel.MouseDown += MoveForm;
            MenuPanel.DoubleClick += MaximizeWindow;
            btnClose.Click += CloseForm;
            btnMinimize.Click += MinimizeWindow;


            TreeViewExplorer.AfterSelect += TreeViewExplorer_AfterSelect;
            Table.KeyDown += Table_HotKeyDown;
            btnAddDB.Click += ShowDBForm;
            btnRefresh.Click += Refresh;
            btnDeleteDB.Click += DeleteDB;
            btnDisconnect.Click += Disconnection;
            Table.SelectionChanged += Table_SelectionChanged;
            Table.DataError += Table_DataError;
            Table.CellValueChanged += Table_CellValueChanged;
            TreeViewExplorer.MouseClick += TreeViewExplorer_MouseClick;
            CreateTableTSMItem.Click += CreateNewTable;
            DeleteDBTSMItem.Click += DeleteDB;
            RenameDBTSMItem.Click += Rename;
            DeleteTableTSMItem.Click += DeleteTable;
            RenameTableTSMItem.Click += Rename;
            DeleteRowTSMItem.Click += DeleteRow;
            FindValueTSMItem.Click += ShowSearchForm;
            FilterTSMItem.Click += ShowFilterForm;
            Table.MouseClick += ShowTableContextMenu;
            Table.Scroll += TableScroll;
            //-----------------------
            FormContainer.mainForm = this;
            CreateEventDictionary();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplicationClose?.Invoke(this, EventArgs.Empty);
        }

        private void Connection(object sender, EventArgs e)
        {
            if(FormContainer.connectionForm.fldConnectionString.Text != "")
            {
                FormContainer.connectionForm.Visible = false;
                this.Visible = true;
            }
            Connected?.Invoke(this, EventArgs.Empty);
        }
        private void Connection(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                Connection(this, EventArgs.Empty);
            }
        }
        private void AuthenticationChange(object sender, EventArgs e)
        {
            if (FormContainer.connectionForm.Authentication.Items[FormContainer.connectionForm.Authentication.SelectedIndex].ToString() == "Проверка подлинности Windows")
            {
                FormContainer.connectionForm.LoginLable.Visible = false;
                FormContainer.connectionForm.PasswordLable.Visible = false;
                FormContainer.connectionForm.fldLogin.Visible = false;
                FormContainer.connectionForm.fldPass.Visible = false;
                FormContainer.connectionForm.Height = 108;
            }
            if (FormContainer.connectionForm.Authentication.Items[FormContainer.connectionForm.Authentication.SelectedIndex].ToString() == "Проверка подлинности SQL Server")
            {
                FormContainer.connectionForm.LoginLable.Visible = true;
                FormContainer.connectionForm.PasswordLable.Visible = true;
                FormContainer.connectionForm.fldLogin.Visible = true;
                FormContainer.connectionForm.fldPass.Visible = true;
                FormContainer.connectionForm.Height = 164;
            }
        }


        private void TreeViewExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeViewExplorer.SelectedNode.Level > 0)
            {
                currentDB = TreeViewExplorer.SelectedNode.FullPath.Split('\\')[0];
                if(Table.DataSource != null)
                {
                    TableHandler.ClearTable();
                }
                TableSelected?.Invoke(this, EventArgs.Empty);
            }
            else
                currentDB = TreeViewExplorer.SelectedNode.Text;
        }
        private void TreeViewExplorer_MouseClick(object sender, MouseEventArgs e)
        {
            //Показ контекстного меню
            if (e.Button == MouseButtons.Right)
            {
                TreeViewExplorer.SelectedNode = TreeViewExplorer.GetNodeAt(e.Location);
                if (TreeViewExplorer.SelectedNode.Tag.ToString() == "DB")
                    ExplorerDBContext.Show(TreeViewExplorer, e.Location);
                if (TreeViewExplorer.SelectedNode.Tag.ToString() == "Table")
                    ExplorerTableContext.Show(TreeViewExplorer, e.Location);
            }
            //Сворачивание нода
            if (e.Button == MouseButtons.Left)
            {
                TreeViewExplorer.GetNodeAt(e.Location).Toggle();
            }

        }
        private void TreeViewExplorer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (TreeViewExplorer.LabelEdit == true)
            {
                e.Node.EndEdit(false);
                TreeViewExplorer.LabelEdit = false;
                if (e.Node.Tag.ToString() == "DB")
                {
                    DBName = e.Label;
                    RenameDB(this, EventArgs.Empty);
                }
                if (e.Node.Tag.ToString() == "Table")
                {
                    TableName = e.Label;
                    RenameTable(this, EventArgs.Empty);
                }
            }
        }
        //Обновлние списка элементов TreeViewExplorer
        private void Refresh(object sender, EventArgs e)
        {
            Refreshed?.Invoke(this, EventArgs.Empty);
        }

        private void Table_HotKeyDown(object sender, KeyEventArgs e)
        {
            TableHandler.HotKeyDown(sender, e);
        }
        private void Table_SelectionChanged(object sender, EventArgs e)
        {
            TableHandler.SaveChangedRow(this, e);
        }
        private void Table_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TableHandler.CellChanged(sender, e);
        }
        private void TableScroll(object sender, ScrollEventArgs e)
        {
            TableHandler.TableScroll(sender, e);
        }
        private void Table_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message +
                            $"\r\n Ошибка в строке: {e.RowIndex}, " +
                            $"\r\n Ошибка в столбце: {e.ColumnIndex}, " +
                            $"\r\n Значение ячейки: {Table.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
        }
        private void ShowTableContextMenu(object sender, MouseEventArgs e)
        {
            TableHandler.ShowContextMenu(sender, e);
        }
        private void SaveNewTable(object sender, EventArgs e)
        {
            TableHandler.SaveNewTable(sender,e);
        }
        private void CreateNewTable(object sender, EventArgs e)
        {
            TableHandler.CreateNewTable(sender,e);
        }
        private void DeleteTable(object sender, EventArgs e)
        {
            TableHandler.DeleteTable(sender, e);
        }

        private void CloseDBForm(object sender, EventArgs e)
        {
            FormContainer.dbForm.Close();
        }
        public void CreateDB(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                DBCreated?.Invoke(this, EventArgs.Empty);
                FormContainer.dbForm.Close();
            }
        }
        public void CreateDB(object sender, EventArgs e)
        {
            var s = sender;
            DBCreated?.Invoke(this, EventArgs.Empty);
        }
        private void DeleteDB(object sender, EventArgs e)
        {
            CurrentDB = (TreeViewExplorer.SelectedNode!= null)? TreeViewExplorer.SelectedNode.FullPath : "";
            DBDeleted?.Invoke(this, EventArgs.Empty);
        }

        private void Rename(object sender, EventArgs e)
        {
            TreeViewExplorer.LabelEdit = true;
            if (!TreeViewExplorer.SelectedNode.IsEditing)
            {
                TreeViewExplorer.SelectedNode.BeginEdit();
            }
            TreeViewExplorer.AfterLabelEdit += TreeViewExplorer_AfterLabelEdit;
        }
        private void RenameDB(object sender, EventArgs e)
        {
            DBRenamed?.Invoke(this, EventArgs.Empty);
        }
        private void RenameTable(object sender, EventArgs e)
        {
            TableRenamed?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteRow(object sender, EventArgs e)
        {
            RowDeleted?.Invoke(this, EventArgs.Empty);
        }
        private void SearchRow(object sender, KeyEventArgs e)
        {
            if ((sender as Control).Name == "fldSearch" && e.KeyData == Keys.Enter && !isSearch)
            {
                SearchRow(this, EventArgs.Empty);
            }
        }
        private void SearchRow(object sender, EventArgs e)
        {
            RowSearched?.Invoke(this, EventArgs.Empty);
        }
        private void DataFilter(object sender, EventArgs e)
        {

            DataFiltered?.Invoke(this, EventArgs.Empty);
            Table.Columns["_FilterRow"].Visible = false;
        }

        private void ExecuteQuery(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                QueryExecute(this, EventArgs.Empty);
            }
        }

        private void CreateEventDictionary()
        {
            EventContainer.Add("Connected", Connected);
            EventContainer.Add("RowDeleted", RowDeleted);
            EventContainer.Add("RowChanged", RowChanged);
            EventContainer.Add("TableSelected", TableSelected);
            EventContainer.Add("DBCreated", DBCreated);
            EventContainer.Add("Refreshed", Refreshed);
            EventContainer.Add("Disconnected", Disconnected);
            EventContainer.Add("ApplicationClose", ApplicationClose);
            EventContainer.Add("TableCreated", TableCreated);
            EventContainer.Add("TableCreate", TableCreate);
            EventContainer.Add("DBDeleted", DBDeleted);
            EventContainer.Add("TableDeleted", TableDeleted);
            EventContainer.Add("DBRenamed", DBRenamed);
            EventContainer.Add("TableRenamed", TableRenamed);
            EventContainer.Add("RowSearched", RowSearched);
            EventContainer.Add("DataFiltered", DataFiltered);
            EventContainer.Add("UploadRows", UploadRows);
            EventContainer.Add("QueryExecute", QueryExecute);

        }

        private void Disconnection(object sender, EventArgs e)
        {
            this.Visible = false;
            TableHandler.ClearTable();
            TreeViewExplorer.Nodes.Clear();
            Disconnected?.Invoke(this, EventArgs.Empty);
            FormContainer.connectionForm.ShowDialog(this);
        }

    }
}
