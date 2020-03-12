using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLTools;

namespace SqlManager
{
    class Presenter
    {
        private readonly IMainForm _view;
        private readonly IDataComposer _dataComposer;
        private readonly IMessageService _message;


        public Presenter(IMainForm view, IDataComposer dataComposer, IMessageService message)
        {
            _view = view;
            _dataComposer = dataComposer;
            _message = message;


            _view.Connected += _view_Connect;
            _view.TableSelected += _view_TableSelect;
            _view.RowDeleted += _view_RowDeleted;
            _view.RowChanged += _view_RowChanged;

        }

        private void _view_RowChanged(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Изменить строку"))
            {
                _dataComposer.ChangeRow(_view.CurrentRow, _view.IndexRow);
                _message.ShowMessage("Запись изменена");
            }
            else
            {
                _view.Content = _dataComposer.GetTable(_view.ServerName, _view.PathToTable);
                _message.ShowMessage("Действие отменено");
            }
                
        }

        private void _view_RowDeleted(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Вы действительно хотите удалить запись с Id {_view.CurrentRow.Rows[_view.IndexRow][0].ToString()}! "))
            {
                _dataComposer.DeleteRow(_view.IndexRow);
                _message.ShowMessage("Запись удалена");
            }
            else
                _message.ShowMessage("Действие отменено");
        }

        private void _view_TableSelect(object sender, EventArgs e)
        {

            _view.Content = _dataComposer.GetTable(_view.ServerName, _view.PathToTable);
        }

        private void _view_Connect(object sender, EventArgs e)
        {
            _view.Explorer = _dataComposer.GetDataBases(_view.ServerName);
        }

    }
}
