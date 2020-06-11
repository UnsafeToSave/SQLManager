using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlManager.InterfaceHandler
{
    public static class TypeBox
    {
        public static ListBox typeBox;
        public static void CreateTypeBox()
        {
            if (typeBox == null)
            {
                typeBox = new ListBox();
                typeBox.MouseClick += TypeBox_MouseClick;
                typeBox.Name = "TypeBox";
                typeBox.BackColor = Color.FromArgb(63, 64, 74);
                typeBox.ForeColor = Color.White;
                typeBox.BorderStyle = BorderStyle.None;
                foreach (var type in GetListTypes())
                {
                    typeBox.Items.Add(type);
                }
            }
        }
        private static void TypeBox_MouseClick(object sender, MouseEventArgs e)
        {
            FormContainer.mainForm.Table.CurrentCell = FormContainer.mainForm.Table.Rows[TableHandler.editCell].Cells[1];
            FormContainer.mainForm.Table.BeginEdit(true);
            FormContainer.mainForm.Table.Rows[TableHandler.editCell].Cells[1].Value = typeBox.SelectedItem;
            FormContainer.mainForm.Controls.Remove(typeBox);
            FormContainer.mainForm.Table.EndEdit();
        }
        private static List<string> GetListTypes()
        {
            var Types = new List<string>();
            Types.Add("BIT");
            Types.Add("TINYINT");
            Types.Add("SMALLINT");
            Types.Add("INT");
            Types.Add("BIGINT");
            Types.Add("DECIMAL(18, 0)");
            Types.Add("NUMERIC(18, 0)");
            Types.Add("SMALLMONEY");
            Types.Add("MONEY");
            Types.Add("FLOAT");
            Types.Add("REAL");
            Types.Add("DATE");
            Types.Add("TIME(7)");
            Types.Add("DATETIME");
            Types.Add("DATETIME2(7)");
            Types.Add("SMALLDATETIME(7)");
            Types.Add("DATETIMEOFFSET");
            Types.Add("CHAR(10)");
            Types.Add("VARCHAR");
            Types.Add("NVARCHAR(24)");
            Types.Add("BINARY(50)");
            Types.Add("VARBINARY");
            Types.Add("UNIQUEIDENTIFIER");
            Types.Add("TIMESTAMP");
            Types.Add("CURSOR");
            Types.Add("HIERARCHYID");
            Types.Add("SQL_VARIANT");
            Types.Add("XML");
            Types.Add("TABLE");
            Types.Add("GEOGRAPHY");
            Types.Add("GEOMETRY");
            return Types;
        }
    }
}
