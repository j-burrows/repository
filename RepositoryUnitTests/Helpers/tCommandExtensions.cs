using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Data;
namespace RepositoryUnitTests.Helpers{
    [TestClass]
    public class tCommandExtensions{
        [TestMethod]
        public void SqlDbTypeFactory_WhenGivenInt_ReturnsSqlDbInt() {
            int Value = 1;
            SqlDbType SqlValueType = Value.SqlDbTypeFactory();
            Assert.IsTrue(SqlValueType == SqlDbType.Int);
        }

        [TestMethod]
        public void SqlDbTypeFactory_WhenGivenString_ReturnsSqlDbVarChar() {
            string Value = string.Empty;
            SqlDbType SqlValueType = Value.SqlDbTypeFactory();
            Assert.IsTrue(SqlValueType == SqlDbType.VarChar);
        }
        
        [TestMethod]
        public void SqlDbTypeFactory_WhenGivenDateTime_ReturnsSqlDbDateTime() {
            DateTime Value = DateTime.MinValue;
            SqlDbType SqlValueType = Value.SqlDbTypeFactory();
            Assert.IsTrue(SqlValueType == SqlDbType.DateTime);
        }
        
        [TestMethod]
        public void SqlDbTypeFactory_WhenGivenDecimal_ReturnsSqlDbDecimal() {
            decimal Value = decimal.MinValue;
            SqlDbType SqlValueType = Value.SqlDbTypeFactory();
            Assert.IsTrue(SqlValueType == SqlDbType.Decimal);
        }
        
        [TestMethod]
        public void SqlDbTypeFactory_WhenGivenBool_ReturnsSqlDbBool() {
            bool Value = false;
            SqlDbType SqlValueType = Value.SqlDbTypeFactory();
            Assert.IsTrue(SqlValueType == SqlDbType.Bit);
        }

        [TestMethod]
        public void CommandWhenAddParam_IncreasesSizeByOne() { 
            using(SqlCommand command = new SqlCommand("")){
                command.AddParam("Value", "Parameter");
                Assert.AreEqual(1, command.Parameters.Count);
            }
        }
        
        [TestMethod]
        public void CommandWhenAddParam_CreatesParamWithName() { 
            using(SqlCommand command = new SqlCommand()){
                command.AddParam("Parameter", "Value");
                Assert.AreEqual("@Parameter", command.Parameters[0].ParameterName);
            }
        }
        
        [TestMethod]
        public void CommandWhenAddParam_CreatesParamWithValue() { 
            using(SqlCommand command = new SqlCommand()){
                command.AddParam("Parameter", "Value");
                Assert.AreEqual("Value", command.Parameters[0].Value);
            }
        }

        [TestMethod]
        public void CommandWhenAdd_N_Params_IncreasesSizeByN() { 
            using(SqlCommand command = new SqlCommand()){
                command.AddParams(
                    new Param("Name1", "Value1"),
                    new Param("Name2", "Value2"),
                    new Param("Name3", "Value3")
                );
                Assert.AreEqual(3, command.Parameters.Count);
            }
        }
        
        [TestMethod]
        public void CommandWhenAdd_N_Params_HasMatchingNames() { 
            using(SqlCommand command = new SqlCommand()){
                command.AddParams(
                    new Param("Name1", "Value1"),
                    new Param("Name2", "Value2"),
                    new Param("Name3", "Value3")
                );
                Assert.AreEqual("@Name3", command.Parameters[2].ParameterName);
            }
        }
        
        [TestMethod]
        public void CommandWhenAdd_N_Params_HasMatchingValues() { 
            using(SqlCommand command = new SqlCommand()){
                command.AddParams(
                    new Param("Name1", "Value1"),
                    new Param("Name2", "Value2"),
                    new Param("Name3", "Value3")
                );
                Assert.AreEqual("Value2", command.Parameters[1].Value);
            }
        }

    }
}
