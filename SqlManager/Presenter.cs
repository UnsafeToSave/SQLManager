﻿using System;
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
            _dataComposer.DataFilter(_view.Filter);
        }

        private void RowSearched(object sender, EventArgs e)
        {
            if(_dataComposer.SearchRow(_view.SearchColumn, _view.SearchValue, _view.SelectedRowIndex, out int index))
                _view.SelectedRowIndex = index;
            else
                _message.ShowMessage("Значение не найдено.");
        }

        private void TableRenamed(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Переименовать таблицу {_view.CurrentTable}"))
            {
                _dataComposer.RenameTable(_view.CurrentDB, _view.CurrentTable, _view.TableName);
                _view.Explorer = _dataComposer.GetDBNames();
            }
        }

        private void DBRenamed(object sender, EventArgs e)
        {
            if(_message.ShowWarningMessage($"Переименовать базу {_view.CurrentDB}"))
            {
                if (!_dataComposer.IsExist(_view.DBName)) 
                    _dataComposer.RenameDB(_view.CurrentDB, _view.DBName);
                else 
                    _message.ShowMessage($"База с именем {_view.DBName} уже существует");

                _view.Explorer = _dataComposer.GetDBNames();
            }
        }

        private void TableDeleted(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage("Вы действительно хотите удалить таблицу."))
            {
                _dataComposer.DeleteTable(_view.CurrentDB, _view.CurrentTable);
                _message.ShowMessage("Таблица удалена.");
                _view.Explorer = _dataComposer.GetDBNames();
            }
        }

        private void DBDeleted(object sender, EventArgs e)
        {
            if (!_dataComposer.IsLockDB(_view.CurrentDB))
            {
                if (_message.ShowWarningMessage("Вы действительно хотите удалить базу данных."))
                {
                    _dataComposer.DeleteDB(_view.CurrentDB);
                    _view.Explorer = _dataComposer.GetDBNames();
                }
            }
            else
                _message.ShowMessage("База данных используется.");
        }

        private void TableCreated(object sender, EventArgs e)
        {
            if (_view.TableName != "")
            {
                if(_dataComposer.CreateTable(_view.CurrentDB, _view.TableName))
                {
                    _view.Explorer = _dataComposer.GetDBNames();
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
            _view.Content = _dataComposer.GetCreatorTable();
        }

        private void Disconnected(object sender, EventArgs e)
        {
            _dataComposer.Disconnected();
        }

        private void Refreshed(object sender, EventArgs e)
        {
            _view.Explorer = _dataComposer.GetDBNames();
        }

        private void DBCreated(object sender, EventArgs e)
        {
            if (_view.DBName != "")
            {
                if (_dataComposer.CreateDB(_view.DBName))
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
                if (_dataComposer.IsExist(_view.FullPath))
                {
                    _dataComposer.ChangeRow();
                }
                else
                    _message.ShowMessage("Таблицы не существует.");
            }
            else
            {
                if (_dataComposer.TryGetTable(_view.CurrentDB, _view.CurrentTable))
                    _view.Content = await Task.Run(() => _dataComposer.GetTable(_view.CurrentDB, _view.CurrentTable));
                else
                    Connection(this, EventArgs.Empty);
                _message.ShowMessage("Действие отменено.");
            }
                
        }

        private void RowDeleted(object sender, EventArgs e)
        {
            if (_message.ShowWarningMessage($"Вы действительно хотите удалить запись с Id {_view.CurrentRow.Rows[0][0].ToString()}! "))
            {
                _dataComposer.DeleteRow(_view.SelectedRowIndex);
                _message.ShowMessage("Запись удалена.");
            }
            else
                _message.ShowMessage("Действие отменено.");
        }

        private void TableSelect(object sender, EventArgs e)
        {
            if (_dataComposer.TryGetTable(_view.CurrentDB, _view.CurrentTable))
                _view.Content =  _dataComposer.GetTable(_view.CurrentDB, _view.CurrentTable);
            else
            {
                _message.ShowErrorMessage("Таблица не найдена.");
                Connection(this, EventArgs.Empty);
            }
        }

        private void Connection(object sender, EventArgs e)
        {
            if (_view.ServerName == "")
            {
                _message.ShowErrorMessage("Ошибка имени сeрвера");
                return;
            }
            _dataComposer.Connection(_view.ServerName, _view.Authentication, _view.Login, _view.Password);
            _view.Explorer = _dataComposer.GetDBNames();
        }

        private void ApplicationClose(object sender, EventArgs e)
        {
            _dataComposer.CloseApp();
        }

    }
}
