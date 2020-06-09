using SqlManager.Forms;
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
    enum Mode
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
        int SelectedRowIndex { get; set; }
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

    }
    
    public partial class MainForm : Form, IMainForm
    {
        ConnectionForm connectionForm;
        DBForm dbForm; 
        TableForm tableForm;
        SearchForm searchForm;
        FilterForm filterForm;

        ImageList interfaceImages;

        ListBox TypeBox;
        int editCell;


        int scrollPointer;
        bool dataChanged = false;
        bool rowsIsAdd = false;
        string currentDB { get; set; }
        Mode ContentMode { get; set; }

        #region Реализация интерфейса IMainForm
        public string ServerName
        {
            get
            {
                return connectionForm.fldConnectionString.Text;
            }
        }
        public string DBName
        {
            set
            {
                if (dbForm == null)
                {
                    dbForm = new DBForm();
                }
                dbForm.fldDBName.Text = value;
            }
            get
            {
                return dbForm.fldDBName.Text;
            }
        }
        public string TableName
        {
            set
            {
                if (tableForm == null)
                {
                    tableForm = new TableForm();
                }
                tableForm.fldTableName.Text = value;
            }
            get
            {
                return tableForm.fldTableName.Text;
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
                return searchForm.cmbSearch.SelectedItem.ToString();
            }
        }
        public string SearchValue
        {
            get
            {
                return searchForm.fldSearch.Text;
            }
        }
        public string Filter
        {
            get
            {
                return filterForm.fldFilter.Text;
            }
        }
        public string Login 
        { 
            get
            {
                if (connectionForm != null)
                {
                    return connectionForm.fldLogin.Text;
                }
                else
                    throw new Exception("Ошибка создания формы соединения");
            } 
        }
        public string Password
        {
            get
            {
                if (connectionForm != null)
                {
                    return connectionForm.fldPass.Text;
                }
                else
                    throw new Exception("Ошибка создания формы соединения");
            }
        }
        public int SelectedRowIndex
        {
            set
            {
                GridContent.Rows[value].Selected = true;
                GridContent.FirstDisplayedScrollingRowIndex = GridContent.SelectedRows[0].Index;
            }
            get
            {
                return GridContent.SelectedRows[0].Index;
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
                    GridContent.DataSource = value;
                    GridContent.Update();
                    if (GridContent.Name == value.TableName)
                    {
                        GridContent.FirstDisplayedScrollingRowIndex = scrollPointer;
                        rowsIsAdd = false;
                    }
                    GridContent.Name = value.TableName;
                }
                
            }
        }
        public DataTable CurrentRow
        {
            get
            {
                var table = new DataTable();
                for (int i = 0; i < GridContent.Columns.Count; i++)
                {
                    table.Columns.Add(new DataColumn(GridContent.Columns[i].HeaderText));
                }
                DataRow row = table.NewRow();
                int index = GridContent.CurrentRow.Index;
                for (int i = 0; i < GridContent.Columns.Count; i++)
                {
                    row[i] = GridContent.Rows[index].Cells[i].Value;
                }
                table.Rows.Add(row);
                return table;
            }
        }
        public SqlAuthenticationMethod Authentication
        {
            get
            {
                if (connectionForm != null)
                {
                    if (connectionForm.Authentication.Items[connectionForm.Authentication.SelectedIndex].ToString() == "Проверка подлинности SQL Server")
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
        #endregion

        #region Меню
        private void MoveForm(object sender, MouseEventArgs e)
        {
            Message m = default;
            switch ((sender as Panel).Parent.Name)
            {
                case "MainForm":
                    MenuPanel.Capture = false;
                    m = Message.Create(this.Handle, 161, new IntPtr(2), IntPtr.Zero);
                     
                    break;
                case "ConnectionForm":
                    connectionForm.MenuPanel.Capture = false;
                    m = Message.Create(connectionForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "DBForm":
                    dbForm.MenuPanel.Capture = false;
                    m = Message.Create(dbForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "TableForm":
                    tableForm.MenuPanel.Capture = false;
                    m = Message.Create(tableForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "SearchForm":
                    searchForm.MenuPanel.Capture = false;
                    m = Message.Create(searchForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "FilterForm":
                    filterForm.MenuPanel.Capture = false;
                    m = Message.Create(filterForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
            }
            this.WndProc(ref m);
        }
        private void CloseForm(object sender, EventArgs e)
        {
            switch ((sender as Button).Parent.Parent.Name)
            {
                case "MainForm":
                    this.Close();
                    break;
                case "ConnectionForm":
                    connectionForm.Close();
                    Application.Exit();
                    break;
                case "DBForm":
                    dbForm.Close();
                    break;
                case "TableForm":
                    tableForm.Close();
                    break;
                case "SearchForm":
                    searchForm.Close();
                    break;
                case "FilterForm":
                    filterForm.Close();
                    break;
            }
        }
        private void MinimizeWindow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void MaximizeWindow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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
            if(connectionForm == null)
            {
                connectionForm = new ConnectionForm();
                connectionForm.btnClose.Click += CloseForm;
                connectionForm.MenuPanel.MouseDown += MoveForm;
                connectionForm.btnConnection.Click += Connection;
                connectionForm.fldConnectionString.KeyDown += Connection;
                connectionForm.Authentication.SelectedIndexChanged += AuthenticationChange;
            }
            connectionForm.Authentication.SelectedIndex = 0;

            connectionForm.Show(this);
            
        }


        private void ShowDBForm(object sender, EventArgs e)
        {
            if (dbForm == null)
            {
                dbForm = new DBForm();
                dbForm.MenuPanel.MouseDown += MoveForm;
                dbForm.btnClose.Click += CloseDBForm;
                dbForm.btnActionDB.Click += CreateDB;
                dbForm.fldDBName.KeyDown += CreateDB;
            }
            dbForm.fldDBName.Text = "";
            dbForm.ShowDialog(this);

        }
        private void ShowTableForm(object sender, EventArgs e)
        {
            if (tableForm == null)
            {
                tableForm = new TableForm();
            }
            tableForm.btnClose.Click += CloseForm;
            tableForm.MenuPanel.MouseDown += MoveForm;
            tableForm.btnActionTable.Click += CreateNewTable;
            tableForm.fldTableName.Text = "";
            tableForm.ShowDialog(this);
        }
        private void ShowSearchForm(object sender, EventArgs e)
        {
            if (searchForm == null)
            {
                searchForm = new SearchForm();
                searchForm.btnClose.Click += CloseForm;
                searchForm.MenuPanel.MouseDown += MoveForm;
                searchForm.btnSearch.Click += SearchRow;
                searchForm.fldSearch.KeyDown += SearchRow;
            }
            searchForm.cmbSearch.Items.Clear();
            searchForm.fldSearch.Text = "";
            for (int i = 0; i < GridContent.Columns.Count; i++)
            {
                if (GridContent.Columns[i].HeaderText == "_FilterRow") continue;
                searchForm.cmbSearch.Items.Add(GridContent.Columns[i].HeaderText);
            }
            searchForm.cmbSearch.SelectedIndex = 0;
            searchForm.ShowDialog(this);
        }
        private void ShowFilterForm(object sender, EventArgs e)
        {
            if(filterForm == null)
            {
                filterForm = new FilterForm();
                filterForm.btnClose.Click += CloseForm;
                filterForm.MenuPanel.MouseDown += MoveForm;
                filterForm.fldFilter.TextChanged += DataFilter;
            }
            filterForm.ShowDialog(this);
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            ContentMode = Mode.Viewer;
            TreeViewExplorer.ImageList = interfaceImages;

            MenuPanel.MouseDown += MoveForm;
            MenuPanel.DoubleClick += MaximizeWindow;
            btnClose.Click += CloseForm;
            btnMinimize.Click += MinimizeWindow;


            TreeViewExplorer.AfterSelect += TreeViewExplorer_AfterSelect;
            GridContent.KeyDown += Table_HotKeyDown;
            btnAddDB.Click += ShowDBForm;
            btnRefresh.Click += Refresh;
            btnDeleteDB.Click += DeleteDB;
            btnDisconnect.Click += Disconnection;
            GridContent.SelectionChanged += Table_SelectionChanged;
            GridContent.DataError += Table_DataError;
            GridContent.CellValueChanged += Table_CellValueChanged;
            TreeViewExplorer.MouseClick += TreeViewExplorer_MouseClick;
            CreateTableTSMItem.Click += CreateTable;
            DeleteDBTSMItem.Click += DeleteDB;
            RenameDBTSMItem.Click += Rename;
            DeleteTableTSMItem.Click += DeleteTable;
            RenameTableTSMItem.Click += Rename;
            DeleteRowTSMItem.Click += DeleteRow;
            FindValueTSMItem.Click += ShowSearchForm;
            FilterTSMItem.Click += ShowFilterForm;
            GridContent.MouseClick += ShowTableContextMenu;
            GridContent.Scroll += TableScroll;
        }

        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplicationClose?.Invoke(this, EventArgs.Empty);
        }

        private void Connection(object sender, EventArgs e)
        {
            if(connectionForm.fldConnectionString.Text != "")
            {
                connectionForm.Visible = false;
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
            if (connectionForm.Authentication.Items[connectionForm.Authentication.SelectedIndex].ToString() == "Проверка подлинности Windows")
            {
                connectionForm.LoginLable.Visible = false;
                connectionForm.PasswordLable.Visible = false;
                connectionForm.fldLogin.Visible = false;
                connectionForm.fldPass.Visible = false;
                connectionForm.Height = 108;
            }
            if (connectionForm.Authentication.Items[connectionForm.Authentication.SelectedIndex].ToString() == "Проверка подлинности SQL Server")
            {
                connectionForm.LoginLable.Visible = true;
                connectionForm.PasswordLable.Visible = true;
                connectionForm.fldLogin.Visible = true;
                connectionForm.fldPass.Visible = true;
                connectionForm.Height = 164;
            }
        }


        private void TreeViewExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeViewExplorer.SelectedNode.Level > 0)
            {
                currentDB = TreeViewExplorer.SelectedNode.FullPath.Split('\\')[0];
                if(GridContent.DataSource != null)
                {
                    ClearTable();
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
            if (e.Control && e.KeyValue == (char)Keys.F)
            {
                ShowSearchForm(sender, EventArgs.Empty);
            }
            if(e.Control && e.KeyValue == (char)Keys.S)
            {
                ShowTableForm(sender, EventArgs.Empty);
            }
            if(e.KeyValue == (char)Keys.F5)
            {
                if(filterForm != null)
                {
                    filterForm.fldFilter.Text = "";
                }
            }
        }
        private void Table_SelectionChanged(object sender, EventArgs e)
        {
            //Сохраняет измененную строку
            if (GridContent.Rows.Count > 1 && dataChanged && ContentMode == Mode.Viewer)
            {
                dataChanged = false;
                RowChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void Table_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ContentMode == Mode.Viewer)
                dataChanged = true;
        }
        private void TableScroll(object sender, ScrollEventArgs e)
        {
            int countRows = GridContent.Rows.Count;
            int allCellHeight = GridContent.Rows.GetRowsHeight(DataGridViewElementStates.None);
            int oneCellHeight = allCellHeight / countRows;
            int currentRows = GridContent.VerticalScrollingOffset / oneCellHeight;
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll && countRows - currentRows <= 100 && !rowsIsAdd)
            {
                UploadRows?.Invoke(this, EventArgs.Empty);
                scrollPointer = currentRows;
                rowsIsAdd = true;
            }
        }
        private void Table_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message +
                            $"\r\n Ошибка в строке: {e.RowIndex}, " +
                            $"\r\n Ошибка в столбце: {e.ColumnIndex}, " +
                            $"\r\n Значение ячейки: {GridContent.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
        }
        private void ShowTableContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridRowContext.Show(GridContent, e.Location);
            }
        }
        private void ClearTable()
        {
            GridContent.DataSource = null;
        }


        private void CreateNewTable(object sender, EventArgs e)
        {
            TableCreated?.Invoke(this, EventArgs.Empty);
            tableForm.btnActionTable.Click -= CreateNewTable;
            ContentMode = Mode.Viewer;
            GridContent.CellBeginEdit -= GridContent_CellBeginEdit;
            GridContent.CellEndEdit -= GridContent_CellEndEdit;
            ClearTable();
        }
        private void CreateTable(object sender, EventArgs e)
        {
            TableCreate?.Invoke(this, EventArgs.Empty);
            ContentMode = Mode.Creator;
            GridContent.CellBeginEdit += GridContent_CellBeginEdit;
            GridContent.CellEndEdit += GridContent_CellEndEdit;
            CurrentDB = TreeViewExplorer.SelectedNode.FullPath;
        }

        private void GridContent_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(GridContent.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                this.Controls.Remove(TypeBox);
            }
            
        }

        private void GridContent_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(GridContent.Name == "Creator")
            {
                int cellHeigh = GridContent.RowTemplate.Height;
                int cellWidth = GridContent.Columns[0].Width;
                int WidthLB = GridContent.Columns[1].Width;
                editCell = e.RowIndex;
                CreateTypeBox();
                if (e.ColumnIndex == GridContent.Columns["Type"].Index)
                {
                    Point p = GridContent.Location;
                    p.X += cellWidth + GridContent.RowHeadersWidth;
                    p.Y += GridContent.ColumnHeadersHeight + cellHeigh + (cellHeigh * editCell);
                    TypeBox.Width = WidthLB;
                    TypeBox.Location = p;
                    this.Controls.Add(TypeBox);
                    TypeBox.BringToFront();
                }
                if (e.ColumnIndex == GridContent.Columns["Name"].Index || e.ColumnIndex == GridContent.Columns["Nullable"].Index)
                {
                    this.Controls.Remove(TypeBox);
                }
            }
        }

        private void DeleteTable(object sender, EventArgs e)
        {
            TableDeleted?.Invoke(this, EventArgs.Empty);
        }

        private void CloseDBForm(object sender, EventArgs e)
        {
            dbForm.Close();
        }
        private void CreateDB(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                DBCreated?.Invoke(this, EventArgs.Empty);
                dbForm.Close();
            }
        }
        private void CreateDB(object sender, EventArgs e)
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
            if ((sender as Control).Name == "fldSearch" && e.KeyData == Keys.Enter)
                SearchRow(this, EventArgs.Empty);
        }
        private void SearchRow(object sender, EventArgs e)
        {
            RowSearched?.Invoke(this, EventArgs.Empty);
        }
        private void DataFilter(object sender, EventArgs e)
        {

            DataFiltered?.Invoke(this, EventArgs.Empty);
            GridContent.Columns["_FilterRow"].Visible = false;
        }

        private List<string> GetListTypes()
        {
            var Types = new List<string>();
            Types.Add("BIT");
            Types.Add("TINYINT");
            Types.Add("SMALLINT");
            Types.Add("INT");
            Types.Add("BIGINT");
            Types.Add("DECIMAL(18, 0)");
            Types.Add("NUMERIC(18, 0)");
            Types.Add("SMALLMONEY");
            Types.Add("MONEY");
            Types.Add("FLOAT");
            Types.Add("REAL");
            Types.Add("DATE");
            Types.Add("TIME(7)");
            Types.Add("DATETIME");
            Types.Add("DATETIME2(7)");
            Types.Add("SMALLDATETIME(7)");
            Types.Add("DATETIMEOFFSET");
            Types.Add("CHAR(10)");
            Types.Add("VARCHAR");
            Types.Add("NVARCHAR(24)");
            Types.Add("BINARY(50)");
            Types.Add("VARBINARY");
            Types.Add("UNIQUEIDENTIFIER");
            Types.Add("TIMESTAMP");
            Types.Add("CURSOR");
            Types.Add("HIERARCHYID");
            Types.Add("SQL_VARIANT");
            Types.Add("XML");
            Types.Add("TABLE");
            Types.Add("GEOGRAPHY");
            Types.Add("GEOMETRY");
            return Types;
        } 
        private void CreateTypeBox()
        {
            if(TypeBox == null)
            {
                TypeBox = new ListBox();
                TypeBox.MouseClick += TypeBox_MouseClick;
                TypeBox.Name = "TypeBox";
                TypeBox.BackColor = Color.FromArgb(63, 64, 74);
                TypeBox.ForeColor = Color.White;
                TypeBox.BorderStyle = BorderStyle.None;
                foreach (var type in GetListTypes())
                {
                    TypeBox.Items.Add(type);
                }
            }
        }

        private void TypeBox_MouseClick(object sender, MouseEventArgs e)
        {
            GridContent.CurrentCell = GridContent.Rows[editCell].Cells[1];
            GridContent.BeginEdit(true);
            GridContent.Rows[editCell].Cells[1].Value = TypeBox.SelectedItem;
            this.Controls.Remove(TypeBox);
            GridContent.EndEdit();
        }

        private void Disconnection(object sender, EventArgs e)
        {
            this.Visible = false;
            ClearTable();
            TreeViewExplorer.Nodes.Clear();
            Disconnected?.Invoke(this, EventArgs.Empty);
            connectionForm.ShowDialog(this);
        }





    }
}
