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
        private readonly ITools _tools;
        private readonly IMessageService _message;


        public Presenter(IMainForm view, ITools tools, IMessageService message)
        {
            _view = view;
            _tools = tools;
            _message = message;

            _view.ApplicationClose += ApplicationClose;
            _view.Connected += Connection;
            _view.TableSelected += TableSelect;
            _view.RowDeleted += RowDeleted;
            _view.RowChanged += RowChanged;
            _view.DBCreated += DBCreated;
            _view.Refreshed += Refreshed;
            _view.Disconnected += Disconnected;
            _view.TableCreate += TableCreate;
            _view.TableCreated += TableCreated;
            _view.DBDeleted += DBDeleted;
            _view.TableDeleted += TableDeleted;
            _view.DBRenamed += DBRenamed;
            _view.TableRenamed += TableRenamed;
            _view.RowSearched += RowSearched;
            _view.DataFiltered += DataFiltered;
        }

        private void DataFiltered(object sender, EventArgs e)
        {
            _tools.DataFilter(_view.Filter);
        }

        private void RowSearched(object sender, EventArgs e)
        {
            if(_tools.SearchRow(_view.SearchColumn, _view.SearchValue, _view.SelectedRowIndex, out int index))
                _view.SelectedRowIndex = index;
            else
                _message.ShowMessage("Значение не найдено.");
        }

        private async void TableRenamed(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Переименовать таблицу {_view.CurrentTable}"))
            {
                _tools.RenameTable(_view.CurrentDB, _view.CurrentTable, _view.TableName);
                _view.Explorer = await _tools.GetDBNames();
            }
        }

        private async void DBRenamed(object sender, EventArgs e)
        {
            if(_message.ShowWarningMessage($"Переименовать базу {_view.CurrentDB}"))
            {
                if (!_tools.IsExist(_view.DBName)) 
                    _tools.RenameDB(_view.CurrentDB, _view.DBName);
                else 
                    _message.ShowMessage($"База с именем {_view.DBName} уже существует");

                _view.Explorer = await _tools.GetDBNames();
            }
        }

        private async void TableDeleted(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage("Вы действительно хотите удалить таблицу."))
            {
                _tools.DeleteTable(_view.CurrentDB, _view.CurrentTable);
                _message.ShowMessage("Таблица удалена.");
                _view.Explorer = await _tools.GetDBNames();
            }
        }

        private async void DBDeleted(object sender, EventArgs e)
        {
            if (!_tools.IsLockDB(_view.CurrentDB))
            {
                if (_message.ShowWarningMessage("Вы действительно хотите удалить базу данных."))
                {
                    _tools.DeleteDB(_view.CurrentDB);
                    _view.Explorer = await _tools.GetDBNames();
                }
            }
            else
                _message.ShowMessage("База данных используется.");
        }

        private async void TableCreated(object sender, EventArgs e)
        {
            if (_view.TableName != "")
            {
                if(_tools.CreateTable(_view.CurrentDB, _view.TableName))
                {
                    _view.Explorer = await _tools.GetDBNames();
                    _message.ShowMessage("Таблица успешно создана.");
                }
                else
                {
                    _message.ShowErrorMessage("Ошибка создания таблицы.");
                }

            }
            else
                _message.ShowErrorMessage("Отсутствует имя таблицы.");
        }

        private void TableCreate(object sender, EventArgs e)
        {
            _view.Content = _tools.GetCreatorTable();
        }

        private void Disconnected(object sender, EventArgs e)
        {
            _tools.Disconnected();
        }

        private async void Refreshed(object sender, EventArgs e)
        {
            _view.Explorer = await _tools.GetDBNames();
        }

        private void DBCreated(object sender, EventArgs e)
        {
            if (_view.DBName != "")
            {
                if (_tools.CreateDB(_view.DBName))
                {
                    _message.ShowMessage("База данных успешно создана.");
                }
                else
                    _message.ShowErrorMessage("База данных с таким именем существует.");
            }
            else
                _message.ShowErrorMessage("Отсутствует имя базы.");
            
        }

        private async void RowChanged(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Изменить строку."))
            {
                if (_tools.IsExist(_view.FullPath))
                {
                    _tools.ChangeRow();
                }
                else
                    _message.ShowMessage("Таблицы не существует.");
            }
            else
            {
                if (_tools.TryGetTable(_view.CurrentDB, _view.CurrentTable))
                    _view.Content = await Task.Run(() => _tools.GetTable(_view.CurrentDB, _view.CurrentTable));
                else
                    Connection(this, EventArgs.Empty);
                _message.ShowMessage("Действие отменено.");
            }
                
        }

        private void RowDeleted(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Вы действительно хотите удалить запись с Id {_view.CurrentRow.Rows[0][0].ToString()}! "))
            {
                _tools.DeleteRow(_view.SelectedRowIndex);
                _message.ShowMessage("Запись удалена.");
            }
            else
                _message.ShowMessage("Действие отменено.");
        }

        private async void TableSelect(object sender, EventArgs e)
        {
            if (_tools.TryGetTable(_view.CurrentDB, _view.CurrentTable))
                _view.Content =  await _tools.GetTable(_view.CurrentDB, _view.CurrentTable);
            else
            {
                _message.ShowErrorMessage("Таблица не найдена.");
                Connection(this, EventArgs.Empty);
            }
        }

        private async void Connection(object sender, EventArgs e)
        {
            if (_view.ServerName == "")
            {
                _message.ShowErrorMessage("Ошибка имени сeрвера");
                return;
            }
            _tools.Connection(_view.ServerName, _view.Authentication, _view.Login, _view.Password);
            _view.Explorer = await _tools.GetDBNames();
        }

        private void ApplicationClose(object sender, EventArgs e)
        {
            _tools.CloseApp();
        }

    }
}
