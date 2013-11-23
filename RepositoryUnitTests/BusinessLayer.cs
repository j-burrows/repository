using System;
/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       BusinessLayer.cs
 |  Purpose:    Contains tests on Business Units and Business Repositories.
 |  Author:     Jonathan Burrows
 |  Updated:    October 5th 2013
 +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
*/
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Business;
using Repository.Business.Protocols;
using RepositoryUnitTests.Business;

namespace RepositoryUnitTests{
    [TestClass]
    public class BusinessLayerTests{
        [TestMethod]
        public void BusinessUnit_TwoIdentical_AreEquivilant() {
            //Arrange: Two business equivilant units are created
            MockBusinessUnit alpha = new MockBusinessUnit { attrOne = 1, attrTwo = "One", attrThree = 1 };
            MockBusinessUnit beta = new MockBusinessUnit { attrOne = 2, attrTwo = "One", attrThree = 2 };

            //Act: Compare the two via equivilance
            bool equal = alpha.Equivilant(beta);

            //Assert: The two are equal
            Assert.AreEqual(equal, true);
        }
        [TestMethod]
        public void BusinessUnit_TwoDifferent_AreNotEquivilant(){
            //Arrange: Two business different units are created.
            MockBusinessUnit alpha = new MockBusinessUnit { attrOne = 1, attrTwo = "One", attrThree = 1 };
            MockBusinessUnit beta = new MockBusinessUnit { attrOne = 2, attrTwo = "Two", attrThree = 2 };

            //Act: Compare the two via equivilance
            bool equal = alpha.Equivilant(beta);

            //Assert: The two are equal
            Assert.AreEqual(equal, false);
        }
        [TestMethod]
        public void BusinessUnit_TwoDifferentPolymorphs_AreNotEquivilant() { 
            //Arrange: Two business different polymorphic units are created
            IBusinessUnit normal = new MockBusinessUnit { attrOne = 1, attrTwo = "1", attrThree = 1 };
            IBusinessUnit polymorph = new MockPolymorphicBusinessUnit { attrOne = 2, attrTwo = "2", attrThree = 2 };

            //Act: Compare the two via equivilance
            bool equal = normal.Equivilant(polymorph);

            //Assert: The two are not equal.
            Assert.AreEqual(equal, false);
        }
        [TestMethod]
        public void BusinessUnit_TwoIdenticalPolymorphs_AreNotEquivilant(){
            //Arrange: Two business identical polymorphic units are created
            IBusinessUnit normal = new MockBusinessUnit { attrOne = 1, attrTwo = "1", attrThree = 1 };
            IBusinessUnit polymorph = new MockPolymorphicBusinessUnit { attrOne = 1, attrTwo = "1", attrThree = 1 };

            //Act: Compare the two via equivilance
            bool equal = normal.Equivilant(polymorph);

            //Assert: The two are not equal.
            Assert.AreEqual(equal, false);
        }

        [TestMethod]
        public void BusinessRepository_AddingUniqueEntry_IncreasesSizeByOne(){
            //Arrange: Create an empty repository
            BusinessRepository<MockBusinessUnit> collection = new MockBusinessRepository();

            //Act: an entry is added
            collection.Create(new MockBusinessUnit());

            //Assert: The size is now one
            Assert.AreEqual(collection.Count, 1);
        }

        [TestMethod]
        public void BusinessRepository_AddingUniquePolymorph_DoesNotIncreaseSize() { 
            //Arrange: Create an empty repository
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            
            //Act: an entry is added
            collection.Create(new MockPolymorphicBusinessUnit());

            //Assert: The size is still zero.
            Assert.AreEqual(collection.Count, 0);
        }

        [TestMethod]
        public void BusinessRepository_RemovingEntry_DecreasesSize() { 
            //Arrange: Create a repository with one entry with a unique string attribute.
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            MockBusinessUnit unit = new MockBusinessUnit { attrTwo = "Identifier" };
            collection.Create(unit);
            Assert.AreEqual(collection.Count, 1);

            //Act: remove the added entry from the repository
            collection.Delete(unit);

            //Assert: the size is now at zero
            Assert.AreEqual(collection.Count, 0);
        }

        [TestMethod]
        public void BusinessRepository_RemovingNonExistantEntry_DoesNotDecreaseSize(){
            //Arrange: Create a repository with one entry with a unique string attribute.
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            MockBusinessUnit unit = new MockBusinessUnit { attrTwo = "Identifier" };
            collection.Create(unit);
            int Size_Before = collection.Count;

            //Act: remove the added entry from the repository
            collection.Delete(new MockBusinessUnit { attrTwo = "Is not stored"});

            //Assert: the size has not changed
            Assert.AreEqual(collection.Count, Size_Before);
        }

        [TestMethod]
        public void BusinessRepository_RemovingExistingPolymorph_DoesNotDecreaseSize()
        {
            //Arrange: Create a repository with one entry with a unique string attribute.
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            MockBusinessUnit unit = new MockBusinessUnit { attrTwo = "Identifier" };
            collection.Create(unit);
            int Size_Before = collection.Count;

            //Act: remove a polymorphic entry which would be considered business equivilant
            collection.Delete(new MockPolymorphicBusinessUnit { attrTwo = "Identifier" });

            //Assert: the size has not changed
            Assert.AreEqual(collection.Count, Size_Before);
        }

        [TestMethod]
        public void BusinessRepository_EditingEntries_ChangesAttributes(){
            //Arrange: Create a repository with one entry with a unique string attribute.
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            MockBusinessUnit unit = new MockBusinessUnit { attrOne = 1, attrTwo = "Identifier" };
            collection.Create(unit);
            int attrOne = unit.attrOne;

            //Act: Change the entry and update the repository to reflect
            unit = new MockBusinessUnit { attrOne = 2, attrTwo = "Identifier" };
            collection.Update(unit);

            //Assert: The first entry will have an attribute of two.
            Assert.AreEqual(attrOne, 1);
            Assert.AreEqual(collection.ToArray()[0].attrOne, 2);
        }

        [TestMethod]
        public void BusinessRepository_EditingEntries_OnlyChangesTargeted() {
            //Arrange: Create a repository with two entries
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            int prevFirstAttrOne = 1;
            int prevSecondAttrOne = 1;
            collection.Create(new MockBusinessUnit { attrOne=prevFirstAttrOne, attrTwo="Targeted"});
            collection.Create(new MockBusinessUnit { attrOne=prevSecondAttrOne, attrTwo="Not targeted"});

            //Act: An edit is made to the targetted entry
            collection.Update(new MockBusinessUnit { attrOne=2, attrTwo="Targeted"});

            //Assert: The targetted entry has changed to 2, the other is still at 1
            Assert.AreEqual(collection.ToArray()[0].attrOne, 2);
            Assert.AreEqual(collection.ToArray()[1].attrOne, 1);
        }

        [TestMethod]
        public void BusinessRepository_EditingPolymorphic_DoesNotChangeCollection(){
            //Arrange: Create a repository a single entry
            BusinessRepository<MockBusinessUnit> collection = new BusinessRepository<MockBusinessUnit>();
            int prevFirstAttrOne = 1;
            collection.Create(new MockBusinessUnit { attrOne = prevFirstAttrOne, attrTwo = "Targeted" });

            //Act: An edit attempt is made by passing an equivilant polymorph
            collection.Update(new MockPolymorphicBusinessUnit { attrOne = 2, attrTwo = "Targeted" });

            //Assert: The element in the collection has not been changed.
            Assert.AreEqual(collection.ToArray()[0].attrOne, prevFirstAttrOne);
        }

        [TestMethod]
        public void Rule_GivenValidSet_ReturnsTrue() { 
            //Arrange: a rule which checks if an integer is even and even number is declared
            Repository.Business.Protocols.Rule rule = 
                Repository.Business.Protocols.Rule.WithPredicateAndMessage(
                x => Convert.ToInt32(x) % 2 == 0,
                ""
            );
            int evenNumber = 2;

            //Act: the rule is tested with the even number.
            bool passes = rule.passes(evenNumber);

            //Assert: the rule passed the test
            Assert.IsTrue(passes);
        }

        [TestMethod]
        public void Rule_GivenValidSet_ReturnsFalse() { 
            //Arrange: a rule which checks if an integer is even and odd number are declared
            Repository.Business.Protocols.Rule rule = 
                Repository.Business.Protocols.Rule.WithPredicateAndMessage(
                x => Convert.ToInt32(x) % 2 == 0,
                ""
            );
            int oddNumber = 1;

            //Act: the rule is tested with the odd number.
            bool passes = rule.passes(oddNumber);

            //Assert: the rule does not pass the test
            Assert.IsFalse(passes);
        }

        [TestMethod]
        public void ProtocolWhenBelowMinLength_WhenCheckedIfPass_ReturnsFalse(){
            //Arrange: a protocol with a constrain containing a min length and a string 
            //below that length is created.
            Repository.Business.Protocols.Premise minLengthConstraint = 
                new Repository.Business.Protocols.Premise{ 
                minLength = 2
            };
            Protocol minLengthProtocol = Protocol.WithPremise(
                minLengthConstraint,
                "One"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = minLengthProtocol.passes("1");

            //Assert: the protocol fails the test.
            Assert.IsFalse(passes);
        }

        [TestMethod]
        public void ProtocolWhenAboveMinLength_WhenCheckedIfPass_ReturnsTrue(){
            //Arrange: a protocol with a constrain containing a min length and a string 
            //above that length is created.
            Repository.Business.Protocols.Premise minLengthConstraint = 
                new Repository.Business.Protocols.Premise{ 
                minLength = 2
            };
            Protocol minLengthProtocol = Protocol.WithPremise(
                minLengthConstraint,
                "LongString"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = minLengthProtocol.passes("123");

            //Assert: the protocol passed the test.
            Assert.IsTrue(passes);
        }

        [TestMethod]
        public void ProtocolWhenAboveMaxLength_WhenCheckedIfPass_ReturnsFalse(){
            //Arrange: a protocol with a constrain containing a max length and a string 
            //above that length is created.
            Repository.Business.Protocols.Premise maxLengthConstraint = 
                new Repository.Business.Protocols.Premise{ 
                minLength = 2,
                maxLength = 5
            };
            Protocol maxLengthProtocol = Protocol.WithPremise(
                maxLengthConstraint,
                "Long String"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = maxLengthProtocol.passes("123456");

            //Assert: the protocol fails the test.
            Assert.IsFalse(passes);
        }

        [TestMethod]
        public void ProtocolWhenBelowMaxLength_WhenCheckedIfPass_ReturnsTrue(){
            //Arrange: a protocol with a constrain containing a max length and a string 
            //above that length is created.
            Repository.Business.Protocols.Premise maxLengthConstraint = 
                new Repository.Business.Protocols.Premise{ 
                minLength = 2,
                maxLength = 5
            };
            Protocol maxLengthProtocol = Protocol.WithPremise(
                maxLengthConstraint,
                "Long String"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = maxLengthProtocol.passes("1234");

            //Assert: the protocol passes the test.
            Assert.IsTrue(passes);
        }

        [TestMethod]
        public void ProtocolWhenNullableAndNull_WhenCheckedIfPass_ReturnsTrue(){
            //Arrange: a protocol with a constrain containing a max length and a string 
            //above that length is created.
            Repository.Business.Protocols.Premise nullableConstraint = 
                new Repository.Business.Protocols.Premise{ 
                nullable = true
            };
            Protocol nullableProtocol = Protocol.WithPremise(
                nullableConstraint,
                "Null string"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = nullableProtocol.passes(null);

            //Assert: the protocol passes the test.
            Assert.IsTrue(passes);
        }

        [TestMethod]
        public void ProtocolWhenNullableAndEmpty_WhenCheckedIfPass_ReturnsTrue(){
            //Arrange: a protocol with a constrain containing a max length and a string 
            //above that length is created.
            Repository.Business.Protocols.Premise nullableConstraint = 
                new Repository.Business.Protocols.Premise{ 
                nullable = true
            };
            Protocol nullableProtocol = Protocol.WithPremise(
                nullableConstraint,
                "Empty string"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = nullableProtocol.passes(null);

            //Assert: the protocol passes the test.
            Assert.IsTrue(passes);
        }

        [TestMethod]
        public void ProtocolWhenNotNullableAndEmpty_WhenCheckedIfPass_ReturnsFalse(){
            //Arrange: a protocol with a constrain containing a max length and a string 
            //above that length is created.
            Repository.Business.Protocols.Premise nullableConstraint = 
                new Repository.Business.Protocols.Premise{ 
                nullable = false
            };
            Protocol nullableProtocol = Protocol.WithPremise(
                nullableConstraint,
                "Empty string"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = nullableProtocol.passes(null);

            //Assert: the protocol passes the test.
            Assert.IsFalse(passes);
        }

        [TestMethod]
        public void ProtocolWhenNotNullableAndNull_WhenCheckedIfPass_ReturnsFalse(){
            Repository.Business.Protocols.Premise nullableConstraint = 
                new Repository.Business.Protocols.Premise{ 
                nullable = false
            };
            Protocol nullableProtocol = Protocol.WithPremise(
                nullableConstraint,
                "Null string"
            );

            //Act: the protocol is checked to pass the test.
            bool passes = nullableProtocol.passes(null);

            //Assert: the protocol passes the test.
            Assert.IsFalse(passes);
        }
        
        [TestMethod]
        public void ProtocolStack_CreateRule_WhenAboveMinLength_ReturnsTrue() {
            Repository.Business.Protocols.Premise standardConstraint =
                new Repository.Business.Protocols.Premise{
                minLength = 2,
                maxLength = 10,
                nullable = false
            };
            string target = "123";
            ProtocolStack protocolStack = ProtocolStack.WithPremise(
                standardConstraint,
                "valid string"
            );

            //Act: Check if the create rules pass the test
            bool passes = protocolStack.Create.passes("123");

            //Assert: the test has passed
            Assert.IsTrue(passes);
        }
        
        [TestMethod]
        public void ProtocolStack_CreateRule_WhenBelowMinLength_ReturnsFalse() {
            Repository.Business.Protocols.Premise standardConstraint =
                new Repository.Business.Protocols.Premise{
                minLength = 2,
                maxLength = 10,
                nullable = false
            };
            string target = "1";
            ProtocolStack protocolStack = ProtocolStack.WithPremise(
                standardConstraint,
                "invalid string"
            );

            //Act: Check if the create rules pass the test
            bool passes = protocolStack.Create.passes(target);

            //Assert: the test has passed
            Assert.IsFalse(passes);
        }
        
        [TestMethod]
        public void ProtocolStack_DeleteRule_WhenCheckedIfPass_ReturnsTrue() {
            Repository.Business.Protocols.Premise standardConstraint =
                new Repository.Business.Protocols.Premise{
                minLength = 2,
                maxLength = -1,
                nullable = false
            };
            string target = "";
            ProtocolStack protocolStack = ProtocolStack.WithPremise(
                standardConstraint,
                "valid string"
            );

            //Act: Check if the delete rules pass the test
            bool passes = protocolStack.Delete.passes(target);

            //Assert: the test has passed
            Assert.IsTrue(passes);
        }
    }
}
