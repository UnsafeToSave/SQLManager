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

        DataComposer dataComposer = new DataComposer();



        [Test]
        public void Test_Connection()
        {
            dataComposer.Connection("(localdb)\\MSSQLLocalDB", System.Data.SqlClient.SqlAuthenticationMethod.NotSpecified);
        }
        [Test]
        public void Test_GetDBNames()
        {
            TreeNode[] nodes = dataComposer.GetDBNames();
            List<string> dbNames = new List<string>();
            List<string> tableNames = new List<string>();
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

            DataTable testTable = dataComposer.GetCreatorTable();

            Assert.AreEqual("Creator", testTable.TableName);
            Assert.AreEqual(3, testTable.Columns.Count);
            Assert.AreEqual(typeof(string), testTable.Columns[0].DataType);
            Assert.AreEqual(typeof(string), testTable.Columns[1].DataType);
            Assert.AreEqual(typeof(bool), testTable.Columns[2].DataType);
        }

        [Test]
        public void Test_GetTable()
        {
            DataTable tableTest = dataComposer.GetTable("aero", "AeroTest");
            DataRow row1 = tableTest.NewRow();
            DataRow row10 = tableTest.NewRow();
            row1["Id"] = 1;
            row1["Name"] = "Name1";
            row10["Id"] = 10;
            row10["Name"] = "Name10";

            
            Assert.AreEqual("AeroTest", tableTest.TableName);
            Assert.AreEqual(10, tableTest.Rows.Count);
            Assert.AreEqual(2, tableTest.Columns.Count);
            Assert.AreEqual(row1.ItemArray, tableTest.Rows[0].ItemArray);
            Assert.AreEqual(row10.ItemArray, tableTest.Rows[9].ItemArray);
        }
        [Test]
        public void Test_ChangeRow()
        {
            Test_Connection();

            DataTable tableTest = dataComposer.GetTable("aero", "AeroTest");
            DataRow row = tableTest.NewRow();
            row["Id"] = 11;
            row["Name"] = "Name11";
            tableTest.Rows.Add(row);
            dataComposer.ChangeRow();

            Assert.AreEqual(11, tableTest.Rows.Count);
        }
        [Test]
        public void Test_DeleteRow()
        {
            DataTable tableTest = dataComposer.GetTable("aero", "AeroTest");
            dataComposer.DeleteRow(10);

            Assert.AreNotEqual(tableTest, dataComposer.GetTable("aero", "AeroTest"));
        }
    }
}
