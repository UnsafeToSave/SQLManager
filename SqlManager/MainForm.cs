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
        string CreateDBName { get; }
        string CreateTableName { get; }
        string CurrentDB { get; set; }
        string PathToTable { get; }
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

    }
    
    public partial class MainForm : Form, IMainForm
    {
        ConnectionForm connectionForm;
        CreateDBForm createDBForm;
        CreateTableForm createTableForm;
        ImageList images;
        string _currentDB;
        bool dataChanged = false;
        Mode ContentMode { get; set; }


        public MainForm()
        {
            InitializeComponent();
            LoadResources();

            connectionForm = new ConnectionForm();
            connectionForm.btnClose.Click += BtnClose_Click1;
            connectionForm.MenuPanel.MouseDown += MenuPanel_MouseDown1;
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
        public string CreateDBName
        {
            get
            {
                return createDBForm.fldDBName.Text;
            }
        }
        public string CreateTableName
        {
            get
            {
                return createTableForm.fldTableName.Text;
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
        public string PathToTable
        {
            get
            {
                if (TreeViewExplorer.SelectedNode.Level != 0)
                    return TreeViewExplorer.SelectedNode.FullPath;
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
        #endregion


        private void ConnectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void BtnConnection_Click(object sender, EventArgs e)
        {
            connectionForm.FormClosed -= ConnectionForm_FormClosed;
        }
        private void BtnClose_Click1(object sender, EventArgs e)
        {
            connectionForm.Close();
        }
        private void MenuPanel_MouseDown1(object sender, MouseEventArgs e)
        {
            connectionForm.MenuPanel.Capture = false;
            Message m = Message.Create(connectionForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            Connected?.Invoke(this, EventArgs.Empty);
            ContentMode = Mode.Normal;
            TreeViewExplorer.ImageList = images;
            
            TreeViewExplorer.AfterSelect += TreeViewExplorer_AfterSelect;
            GridContent.KeyDown += GridContent_KeyDown;
            btnAddDB.Click += CreateDB;
            btnRefresh.Click += Refresh;
            btnDeleteDB.Click += DeleteDB;
            GridContent.SelectionChanged += GridContent_SelectionChanged;
            GridContent.DataError += GridContent_DataError;
            GridContent.CellValueChanged += GridContent_CellValueChanged;
            TreeViewExplorer.MouseClick += TreeViewExplorer_MouseClick;
            DBContextMenu.Items[0].Click += CreateTable;
            DBContextMenu.Items[1].Click += DeleteDB;
            TableContextMenu.Items[0].Click += DeleteTable;
            RowContextMenu.Items[0].Click += DeleteRow;
            GridContent.MouseClick += GridContent_MouseClick;


            MenuPanel.MouseDown += MenuPanel_MouseDown;
            btnClose.Click += BtnClose_Click;
            btnMinimize.Click += BtnMinimize_Click;
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
                TableSelected?.Invoke(this, EventArgs.Empty);
        }
        private void GridContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == (char)Keys.F)
            {
                //TODO Допистаь окно поиска в таблице
            }
            if(e.Control && e.KeyValue == (char)Keys.S)
            {
                createTableForm = new CreateTableForm();
                createTableForm.btnCreateTable.Click += BtnCreateTable_Click;
                createTableForm.btnClose.Click += BtnClose_Click2;
                createTableForm.MenuPanel.MouseDown += MenuPanel_MouseDown3;
                createTableForm.ShowDialog(this);
            }
        }

        private void MenuPanel_MouseDown3(object sender, MouseEventArgs e)
        {
            createTableForm.MenuPanel.Capture = false;
            Message m = Message.Create(createTableForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        private void BtnClose_Click2(object sender, EventArgs e)
        {
            createTableForm.Close();
        }

        private void BtnCreateTable_Click(object sender, EventArgs e)
        {
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
        private void CreateDB(object sender, EventArgs e)
        {
            createDBForm = new CreateDBForm();
            createDBForm.MenuPanel.MouseDown += MenuPanel_MouseDown2;
            createDBForm.btnClose.Click += CreateDBFormClose;
            createDBForm.btnCreateDB.Click += BtnCreateDB_Click;
            createDBForm.ShowDialog(this);
            
        }

        private void MenuPanel_MouseDown2(object sender, MouseEventArgs e)
        {
            createDBForm.MenuPanel.Capture = false;
            Message m = Message.Create(createDBForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        private void CreateDBFormClose(object sender, EventArgs e)
        {
            createDBForm.Close();
        }

        private void BtnCreateDB_Click(object sender, EventArgs e)
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


        #region Меню
        private void MenuPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MenuPanel.Capture = false;
            Message m = Message.Create(this.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion


    }
}
