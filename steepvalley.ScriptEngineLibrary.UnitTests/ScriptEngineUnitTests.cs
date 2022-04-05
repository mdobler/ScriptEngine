using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace steepvalley.ScriptEngineLibrary.UnitTests
{
    [TestClass]
    public class ScriptEngineUnitTests
    {
        private string _connectionString = "";
        private Dictionary<string, string> _valueReplacements = new Dictionary<string, string>();

        [TestInitialize]
        public void TestInit()
        {
            _connectionString = ScriptEngine.BuildConnectionString(
                "db-server", 
                "VisionDemo76", 
                false, 
                "sa", 
                "D0ntpanic@SV");

            _valueReplacements.Add("PKey", "replace(newid(), '-', '')");
            _valueReplacements.Add("Company", "@ActiveCompany");
            _valueReplacements.Add("EMOrg", "@OrgCode");
        }


        [TestMethod]
        public void TestSelectStatementHardcoded()
        {
            var output = ScriptEngine.CreateSelectStatement(
                "PR",
                new string[] { "WBS1", "WBS2", "WBS3" },
                "ChargeType = 'H'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true);

            Assert.IsTrue(output.Contains("select"));
            Assert.IsFalse(output.Contains("from PR"));
        }

        [TestMethod]
        public void TestSelectStatement()
        {
            var output = ScriptEngine.CreateSelectStatement(
                "PR",
                new string[] { "WBS1", "WBS2", "WBS3" },
                "ChargeType = 'H'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                false);

            Assert.IsTrue(output.Contains("select"));
            Assert.IsTrue(output.Contains("from PR"));
        }

        [TestMethod]
        public void TestSelectStatementWithReplacements()
        {
            var output = ScriptEngine.CreateSelectStatement(
                "LedgerAR",
                new string[] { "PKey" },
                "TransDate Between '2005-01-01' and '2005-01-31'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true,
                _valueReplacements
                );

            Assert.IsTrue(output.Contains("select"));
            Assert.IsTrue(output.Contains("replace(newid(), '-', '')"));
            Assert.IsTrue(output.Contains("@OrgCode"));
            Assert.IsFalse(output.Contains("from LedgerAR"));
        }

        [TestMethod]
        public void TestUpdateStatement()
        {
            var output = ScriptEngine.CreateUpdateStatement(
                "PR",
                new string[] { "WBS1", "WBS2", "WBS3" },
                new string[] { "Name", "LongName", "Status" },
                "ChargeType = 'H'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true);

            Assert.IsTrue(output.Contains("update PR"));
            Assert.IsTrue(output.Contains("set [Name] ="));
        }

        [TestMethod]
        public void TestUpdateStatementWithReplacements()
        {
            var output = ScriptEngine.CreateUpdateStatement(
                "LedgerAR",
                new string[] { "PKey" },
                new string[] { } ,
                "TransDate Between '2005-01-01' and '2005-01-31'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true,
                _valueReplacements);

            Assert.IsTrue(output.Contains("update LedgerAR"));
            Assert.IsTrue(output.Contains("@OrgCode"));
        }

        [TestMethod]
        public void TestInsertStatement()
        {
            var output = ScriptEngine.CreateInsertStatement(
                "PR",
                new string[] { "WBS1", "WBS2", "WBS3" },
                "ChargeType = 'H'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true);

            Assert.IsTrue(output.Contains("insert into"));
        }

        [TestMethod]
        public void TestInsertStatementWithReplacements()
        {
            var output = ScriptEngine.CreateInsertStatement(
                "LedgerAR",
                new string[] { "PKey" },
                "TransDate Between '2005-01-01' and '2005-01-31'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true,
                _valueReplacements);

            Assert.IsTrue(output.Contains("insert into LedgerAR"));
            Assert.IsTrue(output.Contains("replace(newid(), '-', '')"));
            Assert.IsTrue(output.Contains("@OrgCode"));
        }

        [TestMethod]
        public void TestDeleteStatementWOCheck()
        {
            var output = ScriptEngine.CreateDeleteStatement(
                "PR",
                new string[] { "WBS1", "WBS2", "WBS3" },
                "ChargeType = 'H'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                false);

            Assert.IsTrue(output.Contains("delete"));
        }

        [TestMethod]
        public void TestDeleteStatementWCheck()
        {
            var output = ScriptEngine.CreateDeleteStatement(
                "PR",
                new string[] { "WBS1", "WBS2", "WBS3" },
                "ChargeType = 'H'",
                "WBS3 desc, WBS2 desc, WBS1 desc",
                _connectionString,
                true);

            Assert.IsTrue(output.Contains("delete"));
            Assert.IsTrue(output.Contains("if exists"));
        }
    }
}