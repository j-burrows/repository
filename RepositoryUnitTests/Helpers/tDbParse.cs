using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Data;

namespace RepositoryUnitTests.Helpers{
    [TestClass]
    public class tDbParse{
        [TestMethod]
        public void Int_ColumnToInt_ReturnsInt() {
            int value = 1;
            int parsed = value.ToInt();
            Assert.AreEqual(value, parsed);
        }
        
        [TestMethod]
        public void NullInt_ColumnToInt_ReturnsFallback() {
            int fallback = -1;
            int? value = null;
            int parsed = value.ToInt(fallback);
            Assert.AreEqual(fallback, parsed);
        }

        [TestMethod]
        public void String_ColumnToStr_ReturnsString() {
            string value = "";
            string parsed = value.ToStr();
            Assert.AreEqual(value, parsed);
        }
        
        [TestMethod]
        public void NullString_ColumnToStr_ReturnsFallback() {
            string value = null;
            string fallback = "fallback";
            string parsed = value.ToStr(fallback);
            Assert.AreEqual(fallback, parsed);
        }

        [TestMethod]
        public void Char_ColumnToChar_ReturnsChar() {
            char value = 'c';
            char parsed = value.ToChar();
            Assert.AreEqual(value, parsed);
        }

        [TestMethod]
        public void NullChar_ColumnToChar_ReturnsFallback() {
            char? value = null;
            char fallback = 'f';
            char parsed = value.ToChar(fallback);
            Assert.AreEqual(parsed, fallback);
        }

        [TestMethod]
        public void DateTime_ColumnToDateTime_ReturnsDateTime() {
            DateTime value = DateTime.MinValue;
            DateTime parsed = value.ToDateTime();
            Assert.AreEqual(value, parsed);
        }

        [TestMethod]
        public void NullDateTime_ColumnToDateTime_ReturnsFallback() {
            DateTime? value = null;
            DateTime fallback = DateTime.MinValue;
            DateTime parsed = value.ToDateTime(fallback);
            Assert.AreEqual(parsed, fallback);
        }

        [TestMethod]
        public void Decimal_ColumnToDecimal_ReturnsDecimal() {
            decimal value = decimal.MinValue;
            decimal parsed = value.ToDec();
            Assert.AreEqual(value, parsed);
        }

        [TestMethod]
        public void NullDecimal_ColumnToDecimal_ReturnsFallback() {
            decimal? value = null;
            decimal fallback = decimal.MinValue;
            decimal parsed = value.ToDec(fallback);
            Assert.AreEqual(parsed, fallback);
        }

        [TestMethod]
        public void NonNullObject_WhenCheckedIfNulltype_IsNotNulltype() {
            string nullable = string.Empty;
            bool isNulltype = nullable.IsNulltype();
            Assert.IsFalse(isNulltype);
        }
        
        [TestMethod]
        public void NullObject_WhenCheckedIfNulltype_IsNulltype() {
            string nullable = null;
            bool isNulltype = nullable.IsNulltype();
            Assert.IsTrue(isNulltype);
        }
    }
}
