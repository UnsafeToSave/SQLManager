using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

            _view.ApplicationClose += _view_ApplicationClose;
            _view.Connected += _view_Connect;
            _view.TableSelected += _view_TableSelect;
            _view.RowDeleted += _view_RowDeleted;
            _view.RowChanged += _view_RowChanged;
            _view.DBCreated += _view_DBCreated;
            _view.Refreshed += _view_Refreshed;
            _view.Disconnected += _view_Disconnected;
            _view.TableCreate += _view_TableCreate;
            _view.TableCreated += _view_TableCreated;
            _view.DBDeleted += _view_DBDeleted;
            _view.TableDeleted += _view_TableDeleted;
            

        }


        private void _view_TableDeleted(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage("Вы действительно хотите удалить таблицу"))
            {
                _dataComposer.DeleteTable(_view.PathToTable);
                _message.ShowMessage("База данных удалена");
                _view.Explorer = _dataComposer.GetDataBases(_view.ServerName);
            }
            else
                _message.ShowMessage("Действие отменено");
        }

        private void _view_DBDeleted(object sender, EventArgs e)
        {
            if(_message.ShowWarningMessage("Вы действительно хотите удалить базу данных"))
            {
                _dataComposer.DeleteDB(_view.CurrentDB);
                _view.Explorer = _dataComposer.GetDataBases(_view.ServerName);
                _message.ShowMessage("База данных удалена");
            }
            else
                _message.ShowMessage("Действие отменено");
        }

        private void _view_TableCreated(object sender, EventArgs e)
        {
            if (_view.CreateTableName != "")
            {
                if(_dataComposer.CreateTable(_view.CurrentDB, _view.CreateTableName))
                {
                    _view.Explorer = _dataComposer.GetDataBases(_view.ServerName);
                    _message.ShowMessage("Таблица успешно создана");
                }
                else
                {
                    _message.ShowErrorMessage("Ошибка создания таблицы");
                }

            }
            else
                _message.ShowErrorMessage("Отсутствует имя таблицы");
        }

        private void _view_TableCreate(object sender, EventArgs e)
        {
            _view.Content = _dataComposer.GetCreateTable();
        }

        private void _view_Disconnected(object sender, EventArgs e)
        {
            _dataComposer.Disconnected();
        }

        private void _view_Refreshed(object sender, EventArgs e)
        {
            _view.Explorer = _dataComposer.GetDataBases(_view.ServerName);
        }

        private void _view_DBCreated(object sender, EventArgs e)
        {
            if (_view.CreateDBName != "")
            {
                if (_dataComposer.CreateDB(_view.CreateDBName))
                {
                    _message.ShowMessage("База данных успешно создана");
                }
                else
                    _message.ShowErrorMessage("База данных с таким именем существует");
            }
            else
                _message.ShowErrorMessage("Отсутствует имя базы");
            
        }

        private void _view_RowChanged(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Изменить строку"))
            {
                _dataComposer.ChangeRow();
                _message.ShowMessage("Запись изменена");
            }
            else
            {
                DataTable table;
                if (_dataComposer.TryGetTable(_view.PathToTable, out table))
                    _view.Content = table;
                else
                    _view_Connect(this, EventArgs.Empty);
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
            DataTable table;
            if (_dataComposer.TryGetTable(_view.PathToTable, out table))
                _view.Content = table;
            else
            {
                _message.ShowErrorMessage("Таблица не найдена");
                _view_Connect(this, EventArgs.Empty);
            }
        }

        private void _view_Connect(object sender, EventArgs e)
        {
            _view.Explorer = _dataComposer.GetDataBases(_view.ServerName);
        }

        private void _view_ApplicationClose(object sender, EventArgs e)
        {
            _dataComposer.CloseApp();
        }

    }
}
