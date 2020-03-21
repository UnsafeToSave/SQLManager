using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SqlManager
{
    enum Mode
    {
        Normal,
        Create,
    }

    public interface IMainForm
    {
        string ServerName { get; }
        string DBName { set; get; }
        string TableName { set; get; }
        string CurrentDB { get; set; }
        string CurrentTable { get; }
        TreeNode[] Explorer { set; }
        DataTable Content { set; }
        DataTable CurrentRow { get; }
        int IndexRow { get; }

        
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

    }
    
    public partial class MainForm : Form, IMainForm
    {
        ConnectionForm connectionForm;
        DBForm dbForm; 
        TableForm tableForm;
        ImageList images;
        string _currentDB;
        bool dataChanged = false;
        Mode ContentMode { get; set; }


        public MainForm()
        {
            InitializeComponent();
            LoadResources();

            connectionForm = new ConnectionForm();
            connectionForm.btnClose.Click += CloseForm;
            connectionForm.MenuPanel.MouseDown += MoveForm;
            connectionForm.FormClosed += ConnectionForm_FormClosed;
            connectionForm.btnConnection.Click += BtnConnection_Click;
            connectionForm.ShowDialog(this);

            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;

        }

        private void LoadResources()
        {
            images = new ImageList();
            images.Images.Add(Image.FromFile("Assets\\Images\\DBIcon.png"));
            images.Images.Add(Image.FromFile("Assets\\Images\\SelectDBIcon.png"));
            images.Images.Add(Image.FromFile("Assets\\Images\\TableIcon.png"));
            images.Images.Add(Image.FromFile("Assets\\Images\\SelectTableIcon.png"));
            images.Images.Add(Image.FromFile("Assets\\Images\\EmptyDBIcon.png"));
        }


        #region реализация интерфейса IMainForm
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
                if(dbForm == null)
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
                if(tableForm == null)
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
                return _currentDB;
            }
            set
            {
                _currentDB = value;
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
        public int IndexRow
        {
            get
            {
                return GridContent.CurrentRow.Index;
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
                GridContent.DataSource = value;
                if(value is DataTable)
                    GridContent.Name = value.TableName;
                GridContent.Update();
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
                for(int i = 0; i < GridContent.Columns.Count; i++)
                {
                    row[i] = GridContent.Rows[index].Cells[i].Value;
                }
                table.Rows.Add(row);
                return table;
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
        #endregion


        private void ConnectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void BtnConnection_Click(object sender, EventArgs e)
        {
            connectionForm.FormClosed -= ConnectionForm_FormClosed;
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            Connected?.Invoke(this, EventArgs.Empty);
            ContentMode = Mode.Normal;
            TreeViewExplorer.ImageList = images;
            
            TreeViewExplorer.AfterSelect += TreeViewExplorer_AfterSelect;
            GridContent.KeyDown += GridContent_KeyDown;
            btnAddDB.Click += ShowDBForm;
            btnRefresh.Click += Refresh;
            btnDeleteDB.Click += DeleteDB;
            GridContent.SelectionChanged += GridContent_SelectionChanged;
            GridContent.DataError += GridContent_DataError;
            GridContent.CellValueChanged += GridContent_CellValueChanged;
            TreeViewExplorer.MouseClick += TreeViewExplorer_MouseClick;
            CreateTableTSMItem.Click += CreateTable;
            DeleteDBTSMItem.Click += DeleteDB;
            RenameDBTSMItem.Click += Rename;
            DeleteTableTSMItem.Click += DeleteTable;
            RenameTableTSMItem.Click += Rename;
            DeleteRowTSMItem.Click += DeleteRow;
            GridContent.MouseClick += GridContent_MouseClick;


            MenuPanel.MouseDown += MoveForm;
            MenuPanel.DoubleClick += MaximizeWindow;
            btnClose.Click += CloseForm;
            btnMinimize.Click += MinimizeWindow;
        }
        private void GridContent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RowContextMenu.Show(GridContent, e.Location);
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplicationClose?.Invoke(this, EventArgs.Empty);
        }


        private void TreeViewExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeViewExplorer.SelectedNode.Level > 0)
            {
                _currentDB = TreeViewExplorer.SelectedNode.FullPath.Split('\\')[0];
                TableSelected?.Invoke(this, EventArgs.Empty);
            }
            else
                _currentDB = TreeViewExplorer.SelectedNode.Text;
        }
        private void GridContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == (char)Keys.F)
            {
                //TODO Допистаь окно поиска в таблице
                
            }
            if(e.Control && e.KeyValue == (char)Keys.S)
            {
                ShowTableForm(sender, EventArgs.Empty);
            }
        }
        private void ShowTableForm(object sender, EventArgs e)
        {
            if(tableForm == null)
            {
                tableForm = new TableForm();
            }
            tableForm.btnClose.Click += CloseForm;
            tableForm.MenuPanel.MouseDown += MoveForm;
            tableForm.btnActionTable.Click += CreateNewTable;
            tableForm.fldTableName.Text = "";
            tableForm.ShowDialog(this);
        }
        private void CreateNewTable(object sender, EventArgs e)
        {
            ContentMode = Mode.Normal;
            TableCreated?.Invoke(this, EventArgs.Empty);
        }
        private void DeleteRow(object sender, EventArgs e)
        {
            RowDeleted?.Invoke(this, EventArgs.Empty);
        }
        private void ChangeRow(object sender, EventArgs e)
        {
            RowChanged?.Invoke(this, EventArgs.Empty);
        }
        private void GridContent_SelectionChanged(object sender, EventArgs e)
        {
            if(GridContent.Capture && GridContent.Rows.Count > 1 && dataChanged)
            {
                dataChanged = false;
                RowChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void GridContent_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(ContentMode == Mode.Normal)
                dataChanged = true;
        }
        private void GridContent_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //TODO Написать обработчик
            throw new NotImplementedException();
        }
        private void TreeViewExplorer_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                TreeViewExplorer.SelectedNode = TreeViewExplorer.GetNodeAt(e.Location);
                if(TreeViewExplorer.SelectedNode.Tag.ToString() == "DB")
                    DBContextMenu.Show(TreeViewExplorer, e.Location);
                if (TreeViewExplorer.SelectedNode.Tag.ToString() == "Table")
                    TableContextMenu.Show(TreeViewExplorer, e.Location);
            }
            if(e.Button == MouseButtons.Left)
            {
                TreeViewExplorer.GetNodeAt(e.Location).Toggle();
            }

        }
        private void ShowDBForm(object sender, EventArgs e)
        {
            if(dbForm == null)
            {
                dbForm = new DBForm();
            }
            dbForm.MenuPanel.MouseDown += MoveForm;
            dbForm.btnClose.Click += DBFormClose;
            dbForm.btnActionDB.Click += CreateDB;
            dbForm.fldDBName.Text = "";
            dbForm.ShowDialog(this);
            
        }
        private void DBFormClose(object sender, EventArgs e)
        {
            dbForm.Close();
        }
        private void CreateDB(object sender, EventArgs e)
        {
            DBCreated?.Invoke(this, EventArgs.Empty);
        }
        private void Refresh(object sender, EventArgs e)
        {
            Refreshed?.Invoke(this, EventArgs.Empty);
        }
        private void Disconnect(object sender, EventArgs e)
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
            connectionForm.ShowDialog(this);
        }
        private void CreateTable(object sender, EventArgs e)
        {
            TableCreate?.Invoke(this, EventArgs.Empty);
            ContentMode = Mode.Create;
            CurrentDB = TreeViewExplorer.SelectedNode.FullPath;
        }
        private void DeleteDB(object sender, EventArgs e)
        {
            CurrentDB = TreeViewExplorer.SelectedNode.FullPath;
            DBDeleted?.Invoke(this, EventArgs.Empty);
        }
        private void DeleteTable(object sender, EventArgs e)
        {
            TableDeleted?.Invoke(this, EventArgs.Empty);
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
        private void TreeViewExplorer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
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
        private void RenameDB(object sender, EventArgs e)
        {
            DBRenamed?.Invoke(this, EventArgs.Empty);
        }
        private void RenameTable(object sender, EventArgs e)
        {
            TableRenamed?.Invoke(this, EventArgs.Empty);
        }

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
                    break;
                case "DBForm":
                    dbForm.Close();
                    break;
                case "TableForm":
                    tableForm.Close();
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


    }
}
