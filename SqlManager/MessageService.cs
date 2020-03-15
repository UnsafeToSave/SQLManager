using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlManager
{
    public interface IMessageService
    {
        void ShowMessage(string message);
        bool ShowWarningMessage(string message);

        void ShowErrorMessage(string message);
    }
    class MessageService:IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ShowWarningMessage(string message)
        {
            if(MessageBox.Show(message, "Сообщение",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                return true;
            }
            return false;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
