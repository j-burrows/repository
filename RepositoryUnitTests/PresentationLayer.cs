/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       PresentationLayer.cs
 |  Purpose:    Contains tests on Presentation Units and Presentation Repositories.
 |  Author:     Jonathan Burrows
 |  Updated:    October 5th 2013
 +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Business;
using Repository.Presentation;
using RepositoryUnitTests.Business;
using RepositoryUnitTests.Presentation;

namespace RepositoryUnitTests{
    
    [TestClass]
    public class UnitTest1{
        [TestMethod]
        public void PresentationUnit_WithNullString_FormatMakesEmpty(){ 
            //Arrange: a presentation unit is initialized with a null member
            MockPresentationUnit unformatted = new MockPresentationUnit();
            unformatted.attrTwo = null;

            //Act: the format method is called
            unformatted.Format();

            //Assert: The string is now not null
            Assert.AreEqual(unformatted.attrTwo, string.Empty);
        }

        [TestMethod]
        public void PresentationRepository_MultipleNullStrings_FormatMakesAllEmpty() { 
            //Arrange: a presentation repository with multiple units of unformatted strings
            MockPresentationRepository collection = 
                new MockPresentationRepository();
            collection.Add(new MockPresentationUnit { attrTwo=null});
            collection.Add(new MockPresentationUnit());

            //Act: The presentation repositorie's format function is called
            collection.Format();

            collection.ForEach(x => Assert.AreEqual(x.attrTwo, string.Empty));
        }

        [TestMethod]
        public void BusinessRepository_MultipleNullStrings_FormatMakesAllEmpty()
        {
            //Arrange: a presentation repository with multiple units of unformatted strings
            BusinessRepository<MockBusinessUnit> collection =
                new MockBusinessRepository();
            collection.Create(new MockBusinessUnit { attrTwo = null });
            collection.Create(new MockBusinessUnit { attrTwo = null});

            //Act: The presentation repositorie's format function is called
            collection.Format();

            collection.ForEach(x => Assert.AreEqual(x.attrTwo, string.Empty));
        }
    }
}
