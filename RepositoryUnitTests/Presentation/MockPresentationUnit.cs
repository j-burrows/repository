using Repository.Presentation;
using RepositoryUnitTests.Base;
namespace RepositoryUnitTests.Presentation{
    public class MockPresentationUnit : MockUnit, IPresentationUnit{
        public virtual void Format(){
            if (attrTwo == null){
                attrTwo = string.Empty;
            }
        }
    }
}
