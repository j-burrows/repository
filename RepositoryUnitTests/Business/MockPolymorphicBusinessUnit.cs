using Repository.Business;
using RepositoryUnitTests.Presentation;
namespace RepositoryUnitTests.Business{
    public class MockPolymorphicBusinessUnit : MockPresentationUnit, IBusinessUnit{
        public MockPolymorphicBusinessUnit(){}
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: CreateValid
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        public virtual bool CreateValid(){
            return attrOne != -1;
        }

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: UpdateValid
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        public virtual bool UpdateValid(){
            return attrTwo != null
                && attrTwo.Length > 0
                && attrTwo.Length <= 32;
        }

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: DeleteValid
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        public virtual bool DeleteValid(){
            return true;
        }

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Equivilant
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        public virtual bool Equivilant(IBusinessUnit comparing){
            MockBusinessUnit boxed;
            if ((boxed = (comparing as MockBusinessUnit)) != null){
                return (
                        boxed.attrTwo == null
                        && boxed.attrTwo == null
                    )
                    || (
                        boxed.attrTwo != null
                        && attrTwo != null
                        && boxed.attrTwo.Equals(attrTwo)
                    );
            }
            return false;
        }
    }
}
