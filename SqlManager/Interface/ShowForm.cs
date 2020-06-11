using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlManager.InterfaceHandler
{
    public static class ShowForm
    {
        public static void ShowDBForm(object sender, EventArgs e)
        {
            if (FormContainer.dbForm == null)
            {
                FormContainer.dbForm = new DBForm();
                FormContainer.dbForm.MenuPanel.MouseDown += FormContainer.mainForm.MoveForm;
                FormContainer.dbForm.btnClose.Click += Menu.CloseForm;
                FormContainer.dbForm.btnActionDB.Click += FormContainer.mainForm.CreateDB;
                FormContainer.dbForm.fldDBName.KeyDown += FormContainer.mainForm.CreateDB;
            }
            FormContainer.dbForm.fldDBName.Text = "";
            FormContainer.dbForm.ShowDialog(FormContainer.mainForm);
        }
    }
}
