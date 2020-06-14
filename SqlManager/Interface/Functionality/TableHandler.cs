using SqlManager.InterfaceHandler;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SqlManager
{
    public static class TableHandler
    {

        public static int editCell;
        public static bool scrollOnOff = true;
        public static bool dataChanged = false;
        public static Mode ContentMode;



        public static void HotKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == (char)Keys.F && FormContainer.mainForm.Table.DataSource != null)
            {
                FormContainer.mainForm.ShowSearchForm(sender, EventArgs.Empty);
            }
            if (e.Control && e.KeyValue == (char)Keys.S)
            {
                FormContainer.mainForm.ShowTableForm(sender, EventArgs.Empty);
            }
            if (e.KeyValue == (char)Keys.F5)
            {
                if (FormContainer.filterForm != null)
                {
                    FormContainer.filterForm.fldFilter.Text = "";
                }
            }
        }
        public static void TableScroll(object sender, ScrollEventArgs e)
        {
            if (scrollOnOff)
            {
                int countRows = FormContainer.mainForm.Table.Rows.Count;
                int allCellHeight = FormContainer.mainForm.Table.Rows.GetRowsHeight(DataGridViewElementStates.None);
                int oneCellHeight = allCellHeight / countRows;
                int currentRows = FormContainer.mainForm.Table.VerticalScrollingOffset / oneCellHeight;
                if (e.ScrollOrientation == ScrollOrientation.VerticalScroll && countRows >= 1000)
                {
                    if (countRows - currentRows <= 100)
                    {
                        EventContainer.Invoke(sender, "UploadRows");
                        if (FormContainer.mainForm.IsFull) return;
                        FormContainer.mainForm.ScrollPointer = currentRows;
                        scrollOnOff = false;
                    }
                }
            }
        }
        public static void SaveChangedRow(object sender, EventArgs e)
        {
            if (FormContainer.mainForm.Table.Rows.Count > 1 && dataChanged && ContentMode == Mode.Viewer)
            {
                dataChanged = false;
                EventContainer.Invoke(sender, "RowChanged");
            }
        }
        public static void ShowContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ContentMode != Mode.Creator && FormContainer.mainForm.Table.DataSource != null)
            {
                FormContainer.mainForm.GridRowContext.Show(FormContainer.mainForm.Table, e.Location);
            }
        }
        public static void CellChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ContentMode == Mode.Viewer)
                dataChanged = true;
        }
        public static void ClearTable()
        {
            FormContainer.mainForm.Table.DataSource = null;
        }
        public static void SaveNewTable(object sender, EventArgs e)
        {
            EventContainer.Invoke(sender, "TableCreated");
            FormContainer.tableForm.btnActionTable.Click -= SaveNewTable;
            ContentMode = Mode.Viewer;
            FormContainer.mainForm.Table.ColumnWidthChanged -= Table_ColumnWidthChanged;
            FormContainer.mainForm.Table.CellBeginEdit -= Table_CellBeginEdit;
            ClearTable();
        }
        public static void CreateNewTable(object sender, EventArgs e)
        {
            EventContainer.Invoke(sender, "TableCreate");
            ContentMode = Mode.Creator;
            FormContainer.mainForm.Table.CellBeginEdit += Table_CellBeginEdit;
            FormContainer.mainForm.Table.ColumnWidthChanged += Table_ColumnWidthChanged;
            FormContainer.mainForm.CurrentDB = FormContainer.mainForm.TreeViewExplorer.SelectedNode.FullPath;
        }
        public static void DeleteTable(object sender, EventArgs e)
        {
            EventContainer.Invoke(sender, "TableDeleted");
        }
        private static void Table_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (FormContainer.mainForm.Table.Name == "Creator")
            {
                int cellHeigh = FormContainer.mainForm.Table.RowTemplate.Height;
                int cellWidth = FormContainer.mainForm.Table.Columns[0].Width;
                int WidthLB = FormContainer.mainForm.Table.Columns[1].Width;
                editCell = e.RowIndex;
                TypeBox.CreateTypeBox();
                if (e.ColumnIndex == FormContainer.mainForm.Table.Columns["Type"].Index)
                {
                    Point p = FormContainer.mainForm.Table.Location;
                    p.X += cellWidth + FormContainer.mainForm.Table.RowHeadersWidth;
                    p.Y += FormContainer.mainForm.Table.ColumnHeadersHeight + cellHeigh + (cellHeigh * editCell);
                    TypeBox.typeBox.Width = WidthLB;
                    TypeBox.typeBox.Location = p;
                    FormContainer.mainForm.Controls.Add(TypeBox.typeBox);
                    TypeBox.typeBox.BringToFront();
                }
                if (e.ColumnIndex == FormContainer.mainForm.Table.Columns["Name"].Index || e.ColumnIndex == FormContainer.mainForm.Table.Columns["Nullable"].Index)
                {
                    FormContainer.mainForm.Controls.Remove(TypeBox.typeBox);
                }
            }
        }
        private static void Table_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (FormContainer.mainForm.Table.Name == "Creator")
            {
                FormContainer.mainForm.Controls.Remove(TypeBox.typeBox);
            }
        }



    }
}
