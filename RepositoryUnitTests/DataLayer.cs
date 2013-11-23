/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       DataLayer.cs
 |  Purpose:    Contains tests on Data Units and Data Repositories.
 |  Author:     Jonathan Burrows
 |  Updated:    October 5th 2013
 +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
*/
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Data;
using RepositoryUnitTests.Data;

namespace RepositoryUnitTests{
    [TestClass]
    public class DataLayer
    {
        [TestMethod]
        public void DataUnit_TwoIdentical_AreEquivilant() { 
            //Arrange: Create two matching data units
            MockDataUnit alpha = new MockDataUnit { key = 1, attrTwo="One", attrThree=3};
            MockDataUnit beta = new MockDataUnit { key = 1, attrTwo="One", attrThree=2};

            //Act: Compare the two via equivilance
            bool equal = alpha.Equivilant(beta);

            //Assert: The two are equal
            Assert.AreEqual(equal, true);
        }

        [TestMethod]
        public void DataUnit_TwoDifferent_AreNotEquivilant() {
            //Arrange: Create two different data units
            MockDataUnit alpha = new MockDataUnit { key = 1, attrTwo = "One", attrThree = 3 };
            MockDataUnit beta = new MockDataUnit { key = 2, attrTwo = "One", attrThree = 3 };

            //Act: Compare the two via equivilance
            bool equal = alpha.Equivilant(beta);

            //Assert: The two are equal
            Assert.AreEqual(equal, false);
        }
        [TestMethod]
        public void BusinessUnit_TwoDifferentPolymorphs_AreNotEquivilant(){
            //Arrange: Two business different polymorphic units are created
            IDataUnit normal = new MockDataUnit { key = 1, attrTwo = "1", attrThree = 1 };
            IDataUnit polymorph = new MockPolymorphicDataUnit { key = 2, attrTwo = "2", attrThree = 2 };

            //Act: Compare the two via equivilance
            bool equal = normal.Equivilant(polymorph);

            //Assert: The two are not equal.
            Assert.AreEqual(equal, false);
        }
        [TestMethod]
        public void BusinessUnit_TwoIdenticalPolymorphs_AreNotEquivilant(){
            //Arrange: Two business identical polymorphic units are created
            IDataUnit normal = new MockDataUnit { attrOne = 1, attrTwo = "1", attrThree = 1 };
            IDataUnit polymorph = new MockPolymorphicDataUnit { attrOne = 1, attrTwo = "1", attrThree = 1 };

            //Act: Compare the two via equivilance
            bool equal = normal.Equivilant(polymorph);

            //Assert: The two are not equal.
            Assert.AreEqual(equal, false);
        }

        [TestMethod]
        public void DataRepository_Initialisation_AutomaticallyFillRepository() { 
            //Arrange: Initialise a data repository
            MockSqlRepository collection = new MockSqlRepository();

            //Assert: The size is more than zero.
            Assert.AreNotEqual(collection, 0);
        }

        [TestMethod]
        public void DataRepository_Update_CollectionChanges() {
            //Arrange: Initialise a data repository
            MockSqlRepository collection = new MockSqlRepository();
            
            //Act: The entry is targeted for update
            collection.Update(new MockDataUnit { attrOne=0, attrTwo="Entry 0", attrThree=5});

            //Assert: The first entry has been changed to two
            Assert.AreEqual(collection.FirstOrDefault().attrThree,5);
        }
        [TestMethod]
        public void DataRepository_ValidDeletion_DecreasesCollectionSize() { 
            //Arrange: Initialise a data repository
            MockSqlRepository collection = new MockSqlRepository();
            int Collection_Size = collection.Count;

            //Act: a deletion is targeted for existing entry
            collection.Delete(new MockDataUnit { key=0, attrTwo="Entry 0"});

            //Assert: The size of the collection has decreased by one.
            Assert.AreEqual(Collection_Size-1, collection.Count);
        }
        [TestMethod]
        public void DataRepository_InvalidDeletion_DoesNotDecreaseCollectionSize(){
            //Arrange: Initialise a data repository
            MockSqlRepository collection = new MockSqlRepository();
            int Collection_Size = collection.Count;

            //Act: a deletion is targeted for non existing entry
            collection.Delete(new MockDataUnit { key = 0, attrTwo = "Entry 1" });

            //Assert: The size of the collection has not changed
            Assert.AreEqual(Collection_Size, collection.Count);
        }
        [TestMethod]
        public void DataRepository_InsertionOfUniqueItem_IncreasesSizeByOne() { 
            //Arrange: Initialise a data repository
            MockSqlRepository collection = new MockSqlRepository();
            int Collection_Size = collection.Count;

            //Act: a create is requested
            collection.Create(new MockDataUnit { key = 4, attrTwo="Entry 4", attrThree=4});

            //Assert: The size of the collection has increased by one.
            Assert.AreEqual(Collection_Size+1, collection.Count);
        }
        [TestMethod]
        public void DataRepository_InsertionOfExistingItem_DoesNotIncreaseSize() {
            //Arrange: Initialise a data repository
            MockSqlRepository collection = new MockSqlRepository();
            int Collection_Size = collection.Count;

            //Act: a create is requested for an existing entry
            collection.Create(new MockDataUnit { key = 0, attrTwo = "Entry 0", attrThree = 0 });

            //Assert: The size of the collection has not changed.
            Assert.AreEqual(Collection_Size, collection.Count);
        }

    }
}
