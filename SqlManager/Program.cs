using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLTools;

namespace SqlManager
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();
            DataComposer dataAssembler = new DataComposer();
            MessageService message = new MessageService();

            Presenter presenter = new Presenter(mainForm, dataAssembler, message);
            
            Application.Run(mainForm);
        }
    }
}
