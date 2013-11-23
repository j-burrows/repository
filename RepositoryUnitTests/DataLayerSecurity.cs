using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryUnitTests.Data;

namespace RepositoryUnitTests{
    [TestClass]
    public class DataLayerSecurity{
        [TestMethod]
        public void DataRepository_CreatingMalicousHtml_WillConvertToSafe(){
            //Arrange: A data repository and malicious entry are created
            MockSqlRepository collection = new MockSqlRepository();
            string malicious = "<div>Hello, world!</div>";
            MockDataUnit unit = new MockDataUnit { key = 5, attrTwo = malicious, attrThree = 3 };

            //Act: a create is requested
            collection.Create(unit);

            //Assert: The entry that was added no longer contains the malicious code.
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataRepository_InsertingMalicousSql_WillConvertToSafe(){
            //Arrange: data repository and data unit with sql injection is created.
            MockSqlRepository collection = new MockSqlRepository();
            string malicious = "attribute');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { key = 6, attrTwo = malicious, attrThree = 4 };

            //Act: a create is requested
            collection.Create(unit);

            //Assert: The entry that was added no longer contains sql injection code
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataRepository_CreatingMalicousSqlHtml_WillConvertToSafe(){
            //Arrange: data repository and data unit with malicous and html code is created.
            MockSqlRepository collection = new MockSqlRepository();
            string malicious = "<div>Hello, world!</div>');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { key = 6, attrTwo = malicious, attrThree = 4 };

            //Act: a create is requested
            collection.Create(unit);

            //Assert: The entry that was added no longer contains sql injection code
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataRepository_UpdatingMalicousHtml_WillConvertToSafe(){
            //Arrange: A data repository and malicious entry are created
            MockSqlRepository collection = new MockSqlRepository();
            string malicious = "<div>Hello, world!</div>";
            MockDataUnit unit = new MockDataUnit { key = 5, attrTwo = malicious, attrThree = 3 };

            //Act: an update is requested
            collection.Update(unit);

            //Assert: The entry that was added no longer contains the malicious code.
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataRepository_UpdatingMalicousSql_WillConvertToSafe()
        {
            //Arrange: data repository and data unit with sql injection is created.
            MockSqlRepository collection = new MockSqlRepository();
            string malicious = "attribute');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { key = 6, attrTwo = malicious, attrThree = 4 };

            //Act: an update is requested
            collection.Update(unit);

            //Assert: The entry that was added no longer contains sql injection code
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataRepository_UpdatingMalicousSqlHtml_WillConvertToSafe(){
            //Arrange: data repository and data unit with malicous and html code is created.
            MockSqlRepository collection = new MockSqlRepository();
            string malicious = "<div>Hello, world!</div>');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { key = 6, attrTwo = malicious, attrThree = 4 };

            //Act: an update is requested
            collection.Update(unit);

            //Assert: The entry that was added no longer contains sql injection code
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataUnit_WithHtmlMaliciousMembers_WillConvertToSafe(){
            //Arrange: a data unit with a member who contains an html tag
            string malicious = "<div>Hello, world!</div>";
            MockDataUnit unit = new MockDataUnit { attrTwo = malicious };

            //Act: A scrub is performed
            unit.Scrub();

            //Assert: the unit no longer has a malicious member
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataUnit_WithSqlMaliciousMembers_WillConvertToSafe(){
            //Arrange: a data unit with a member who contains an sql injection script
            string malicious = "attribute');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { attrTwo = malicious };

            //Act: A scrub is performed
            unit.Scrub();

            //Assert: the unit no longer has a malicious member
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataUnit_WithSqlHtmlMaliciousMembers_WillConvertToSafe(){
            //Arrange: a data unit with a member who contains html and sql injection.
            string malicious = "<div>Hello, world!</div>');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { attrTwo = malicious };

            //Act: A scrub is performed
            unit.Scrub();

            //Assert: the unit no longer has a malicious member
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }

        [TestMethod]
        public void DataRepository_WithCorruptedEntries_WillConvertToSafe() {
            //Arrange: a data repository with corrupt additions made directly to it.
            string malicious = "<div>Hello, world!</div>');DROP TABLE dbo.Users;--";
            MockDataUnit unit = new MockDataUnit { attrTwo = malicious};
            MockSqlRepository collection = new MockSqlRepository();
            collection.Add(unit);

            //Act: a scrub is performed on the collection
            collection.Scrub();

            //Assert: the unit contained in the repository no longer has malicious member.
            Assert.AreNotEqual(malicious, unit.attrTwo);
        }
    }
}
