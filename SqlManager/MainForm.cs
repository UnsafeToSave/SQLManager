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

    public interface IMainForm
    {
        string ServerName { get; }
       
        TreeNode[] Explorer { set; }
        string PathToTable { get; }
        DataTable Content { set; }
        DataTable CurrentRow { get; }
        int IndexRow { get; }

        event EventHandler Connected;
        event EventHandler RowDeleted;
        event EventHandler RowChanged;
        event EventHandler TableSelected;

    }
    
    public partial class MainForm : Form, IMainForm
    {
        
        ConnectionForm connectionForm;
        bool dataChanged = false;


        public MainForm()
        {
            InitializeComponent();

            connectionForm = new ConnectionForm();
            connectionForm.FormClosed += ConnectionForm_FormClosed;
            connectionForm.btnConnection.Click += BtnConnection_Click;
            connectionForm.ShowDialog(this);

            this.Shown += MainForm_Shown;
        }  


        #region реализация интерфейса IMainForm
        public string ServerName
        {
            get
            {
                return connectionForm.fldConnectionString.Text;
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
                TreeViewExplorer.Nodes.AddRange(value);
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

        //TODO переделать
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

            TreeViewExplorer.AfterSelect += TreeViewExplorer_AfterSelect;
            GridContent.KeyDown += GridContent_KeyDown;
            btnDelete.Click += BtnDelete_Click;
            btnChange.Click += BtnChange_Click;
            GridContent.SelectionChanged += GridContent_SelectionChanged;
            GridContent.DataError += GridContent_DataError;
            GridContent.CellValueChanged += GridContent_CellValueChanged;
        }

        
        private void TreeViewExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TableSelected?.Invoke(this, EventArgs.Empty);
        }
        private void GridContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == (char)Keys.F)
            {
                //TODO Допистаь окно поиска в таблице
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            RowDeleted?.Invoke(this, EventArgs.Empty);
        }
        private void BtnChange_Click(object sender, EventArgs e)
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
            dataChanged = true;
        }
        private void GridContent_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //TODO Написать обработчик
            throw new NotImplementedException();
        }

    }
}
