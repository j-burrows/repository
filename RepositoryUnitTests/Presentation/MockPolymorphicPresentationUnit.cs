using Repository.Presentation;
using RepositoryUnitTests.Base;
namespace RepositoryUnitTests.Presentation{
    public class MockPolymorphicPresentationUnit : MockUnit, IPresentationUnit{
        public virtual void Format(){
            if (attrTwo == null){
                attrTwo = string.Empty;
            }
        }
    }
}
