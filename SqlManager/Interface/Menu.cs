using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlManager.InterfaceHandler
{
    public static class Menu
    {
        public static void MoveForm(object sender, MouseEventArgs e)
        {
            Message m = default;
            switch ((sender as Panel).Parent.Name)
            {
                case "MainForm":
                    FormContainer.mainForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.mainForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "ConnectionForm":
                    FormContainer.connectionForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.connectionForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "DBForm":
                    FormContainer.dbForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.dbForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "TableForm":
                    FormContainer.tableForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.tableForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "SearchForm":
                    FormContainer.searchForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.searchForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "FilterForm":
                    FormContainer.filterForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.filterForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
                case "QueryForm":
                    FormContainer.queryForm.MenuPanel.Capture = false;
                    m = Message.Create(FormContainer.queryForm.Handle, 161, new IntPtr(2), IntPtr.Zero);
                    break;
            }
            FormContainer.mainForm.m = m;
        }
        public static void CloseForm(object sender, EventArgs e)
        {
            switch ((sender as Button).Parent.Parent.Name)
            {
                case "MainForm":
                    FormContainer.mainForm.Close();
                    break;
                case "ConnectionForm":
                    FormContainer.connectionForm.Close();
                    Application.Exit();
                    break;
                case "DBForm":
                    FormContainer.dbForm.Close();
                    break;
                case "TableForm":
                    FormContainer.tableForm.Close();
                    break;
                case "SearchForm":
                    FormContainer.searchForm.Close();
                    break;
                case "FilterForm":
                    FormContainer.filterForm.Close();
                    break;
                case "QueryForm":
                    FormContainer.queryForm.Close();
                    break;
            }
        }
        public static void MinimizeWindow(object sender, EventArgs e)
        {
            FormContainer.mainForm.WindowState = FormWindowState.Minimized;
        }
        public static void MaximizeWindow(object sender, EventArgs e)
        {
            FormContainer.mainForm.WindowState = FormWindowState.Maximized;
        }
    }
}
