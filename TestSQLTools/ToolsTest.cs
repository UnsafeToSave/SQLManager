using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLTools;
using System.Windows.Forms;
using System.Data;

namespace TestSQLTools
{
    [TestFixture]
    public class ToolsTest
    {

        SQLTools.SQLTools dataComposer = new SQLTools.SQLTools();



        [SetUp]
        public void Test_Connection()
        {
            dataComposer.Connection("(localdb)\\MSSQLLocalDB", System.Data.SqlClient.SqlAuthenticationMethod.NotSpecified);
        }

        [Test]
        public void Test_GetDBNames()
        {
            TreeNode[] nodes;
            List<string> dbNames = new List<string>();

            nodes = dataComposer.GetDBNames();
            foreach (var db in nodes)
            {
                dbNames.Add(db.Text);
            }

            Assert.Contains("master", dbNames);
            Assert.Contains("model", dbNames);
            Assert.Contains("msdb", dbNames);
            Assert.Contains("tempdb", dbNames);
            Assert.Contains("aero", dbNames);
        }

        [Test]
        public void Test_TryGetTable()
        {
            Assert.IsTrue(dataComposer.TryGetTable("aero", "Company"));
            Assert.IsTrue(dataComposer.TryGetTable("aero", "Pass_in_trip"));
            Assert.IsTrue(dataComposer.TryGetTable("aero", "Passenger"));
            Assert.IsTrue(dataComposer.TryGetTable("aero", "Trip"));
            Assert.IsFalse(dataComposer.TryGetTable("aero", "Tickets"));
            Assert.IsFalse(dataComposer.TryGetTable("aero", "Planes"));
        }

        [Test]
        public void Test_GetCreatorTable()
        {

            DataTable testTable;

            testTable = dataComposer.GetCreatorTable();

            Assert.AreEqual("Creator", testTable.TableName);
            Assert.AreEqual(3, testTable.Columns.Count);
            Assert.AreEqual(typeof(string), testTable.Columns[0].DataType);
            Assert.AreEqual(typeof(string), testTable.Columns[1].DataType);
            Assert.AreEqual(typeof(bool), testTable.Columns[2].DataType);
        }

        [Test]
        public void Test_GetTable()
        {
            DataTable tableTest;
            DataRow newRow1;
            DataRow newRow5;

            tableTest = dataComposer.GetTable("aero", "AeroTest");
            newRow1 = tableTest.NewRow();
            newRow1["Id"] = 1;
            newRow1["Name"] = "Name1";
            newRow5 = tableTest.NewRow();
            newRow5["Id"] = 5;
            newRow5["Name"] = "Name5";

            Assert.AreEqual("AeroTest", tableTest.TableName);
            Assert.AreEqual(10, tableTest.Rows.Count);
            Assert.AreEqual(2, tableTest.Columns.Count);
            Assert.AreEqual(newRow1.ItemArray, tableTest.Rows[0].ItemArray);
            Assert.AreEqual(newRow5.ItemArray, tableTest.Rows[4].ItemArray);
        }

        [Test]
        public void Test_ChangeRow()
        {
            DataTable tableTest;
            DataTable newTable;
            DataRow newRow;

            tableTest = dataComposer.GetTable("aero", "AeroTest");
            newRow = tableTest.NewRow();
            newRow["Id"] = 11;
            newRow["Name"] = "Name11";
            tableTest.Rows.Add(newRow);
            dataComposer.ChangeRow();
            newTable = dataComposer.GetTable("aero", "AeroTest");

            Assert.AreEqual(11, newTable.Rows.Count);
        }

        [Test]
        public void Test_DeleteRow()
        {
            DataTable tableTest;

            tableTest = dataComposer.GetTable("aero", "AeroTest");
            dataComposer.DeleteRow(10);

            Assert.AreNotEqual(tableTest, dataComposer.GetTable("aero", "AeroTest"));
        }
    }
}
