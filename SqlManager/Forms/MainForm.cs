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
using System.IO;

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
        internal  ImageList Images = LoadResources();

        public Message m {
            set
            {
                WndProc(ref value);
            }
        }
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
                if (!value)
                {
                    TableHandler.scrollOnOff = true;
                }

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
                    if (Table.Name == value.TableName && !IsSearch)
                    {
                        Table.FirstDisplayedScrollingRowIndex = ScrollPointer;
                        TableHandler.scrollOnOff = true;
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
        public MainForm()
        {
            FormContainer.mainForm = this;
            InitializeComponent();
            ShowConnectionForm();


            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;
            
        }

        public static ImageList LoadResources()
        {
            var interfaceImages = new ImageList();
            interfaceImages.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("DBIcon"));
            interfaceImages.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("SelectDBIcon"));
            interfaceImages.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("TableIcon"));
            interfaceImages.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("SelectTableIcon"));
            interfaceImages.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("EmptyDBIcon"));
            return interfaceImages;
        }

        public void ShowConnectionForm()
        {
            ShowForm.ShowConnectionForm(this, EventArgs.Empty);
            
        }


        public void ShowDBForm(object sender, EventArgs e)
        {
            ShowForm.ShowDBForm(sender, e);
        }
        public void ShowTableForm(object sender, EventArgs e)
        {
            ShowForm.ShowTableForm(sender, e);
        }
        public void ShowSearchForm(object sender, EventArgs e)
        {
            ShowForm.ShowSearchForm(sender, e);
        }
        public void ShowFilterForm(object sender, EventArgs e)
        {
            ShowForm.ShowFilterForm(sender, e);
        }
        public void ShowQueryForm(object sender, EventArgs e)
        {
            ShowForm.ShowQueryForm(sender, e);
        }
        public void MainForm_Shown(object sender, EventArgs e)
        {
            ShowForm.ShowMainForm();
            CreateEventDictionary();
        }
        public void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplicationClose?.Invoke(this, EventArgs.Empty);
        }

        public void Connection(object sender, EventArgs e)
        {
            if(FormContainer.connectionForm.fldConnectionString.Text != "")
            {
                FormContainer.connectionForm.Visible = false;
                this.Visible = true;
            }
            Connected?.Invoke(this, EventArgs.Empty);
        }
        public void Connection(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                Connection(this, EventArgs.Empty);
            }
        }
        public void AuthenticationChange(object sender, EventArgs e)
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


        public void TreeViewExplorer_AfterSelect(object sender, TreeViewEventArgs e)
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
            TableHandler.ContentMode = Mode.Viewer;
        }
        public void TreeViewExplorer_MouseClick(object sender, MouseEventArgs e)
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
        public void TreeViewExplorer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
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
        public void Refresh(object sender, EventArgs e)
        {
            Refreshed?.Invoke(this, EventArgs.Empty);
            TableHandler.ClearTable();
            
        }

        public void Table_HotKeyDown(object sender, KeyEventArgs e)
        {
            TableHandler.HotKeyDown(sender, e);
        }
        public void Table_SelectionChanged(object sender, EventArgs e)
        {
            TableHandler.SaveChangedRow(this, e);
        }
        public void Table_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TableHandler.CellChanged(sender, e);
        }
        public void TableScroll(object sender, ScrollEventArgs e)
        {
            TableHandler.TableScroll(sender, e);
        }
        public void Table_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message +
                            $"\r\n Ошибка в строке: {e.RowIndex}, " +
                            $"\r\n Ошибка в столбце: {e.ColumnIndex}, " +
                            $"\r\n Значение ячейки: {Table.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
        }
        public void ShowTableContextMenu(object sender, MouseEventArgs e)
        {
            TableHandler.ShowContextMenu(sender, e);
        }
        public void SaveNewTable(object sender, EventArgs e)
        {
            TableHandler.SaveNewTable(sender,e);
        }
        public void CreateNewTable(object sender, EventArgs e)
        {
            TableHandler.CreateNewTable(sender,e);
        }
        public void DeleteTable(object sender, EventArgs e)
        {
            TableHandler.DeleteTable(sender, e);
        }

        public void CloseDBForm(object sender, EventArgs e)
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
        public void DeleteDB(object sender, EventArgs e)
        {
            CurrentDB = (TreeViewExplorer.SelectedNode!= null)? TreeViewExplorer.SelectedNode.FullPath : "";
            DBDeleted?.Invoke(this, EventArgs.Empty);
        }

        public void Rename(object sender, EventArgs e)
        {
            TreeViewExplorer.LabelEdit = true;
            if (!TreeViewExplorer.SelectedNode.IsEditing)
            {
                TreeViewExplorer.SelectedNode.BeginEdit();
            }
            TreeViewExplorer.AfterLabelEdit += TreeViewExplorer_AfterLabelEdit;
        }
        public void RenameDB(object sender, EventArgs e)
        {
            DBRenamed?.Invoke(this, EventArgs.Empty);
        }
        public void RenameTable(object sender, EventArgs e)
        {
            TableRenamed?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteRow(object sender, EventArgs e)
        {
            RowDeleted?.Invoke(this, EventArgs.Empty);
        }
        public void SearchRow(object sender, KeyEventArgs e)
        {
            if ((sender as Control).Name == "fldSearch" && e.KeyData == Keys.Enter && !isSearch)
            {
                SearchRow(this, EventArgs.Empty);
            }
        }
        public void SearchRow(object sender, EventArgs e)
        {
            TableHandler.scrollOnOff = false;
            RowSearched?.Invoke(this, EventArgs.Empty);
        }
        public void DataFilter(object sender, EventArgs e)
        {

            DataFiltered?.Invoke(this, EventArgs.Empty);
            Table.Columns["_FilterRow"].Visible = false;
        }

        public void ExecuteQuery(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                QueryExecute(this, EventArgs.Empty);
            }
        }

        public void CreateEventDictionary()
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

        public void Disconnection(object sender, EventArgs e)
        {
            this.Visible = false;
            TableHandler.ClearTable();
            Table.Name = "";
            TreeViewExplorer.Nodes.Clear();
            Disconnected?.Invoke(this, EventArgs.Empty);
            FormContainer.connectionForm.ShowDialog(this);
        }

    }
}
