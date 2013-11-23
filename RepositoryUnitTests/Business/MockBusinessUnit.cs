using Repository.Business;
using Repository.Business.Protocols;
using RepositoryUnitTests.Presentation;
namespace RepositoryUnitTests.Business{
    public class MockBusinessUnit : MockPresentationUnit, IBusinessUnit{
        public static readonly ProtocolStack attrOneProtocol = ProtocolStack.WithPremise(
            new Premise(), "attrOne");
        public static readonly ProtocolStack attrTwoProtocol = ProtocolStack.WithPremise(
            new Premise { nullable = true, maxLength = 32}, "attrTwo");
        public static readonly ProtocolStack attrThreeProtocol = ProtocolStack.WithPremise(
            new Premise(), "attrThree");

        public MockBusinessUnit(){}

        public virtual bool CreateValid(){
            bool isValid = true;
            if(!attrOneProtocol.Create.passes(attrOne)){
                isValid = false;
            }
            if(!attrTwoProtocol.Create.passes(attrTwo)){
                isValid = false;
            }
            if(!attrThreeProtocol.Create.passes(attrThree)){
                isValid = false;
            }
            return isValid;
        }

        public virtual bool UpdateValid(){
            bool isValid = true;
            if(!attrOneProtocol.Update.passes(attrOne)){
                isValid = false;
            }
            if(!attrTwoProtocol.Update.passes(attrTwo)){
                isValid = false;
            }
            if(!attrThreeProtocol.Update.passes(attrThree)){
                isValid = false;
            }
            return isValid;
        }

        public virtual bool DeleteValid(){
            bool isValid = true;
            if(!attrOneProtocol.Delete.passes(attrOne)){
                isValid = false;
            }
            if(!attrTwoProtocol.Delete.passes(attrTwo)){
                isValid = false;
            }
            if(!attrThreeProtocol.Delete.passes(attrThree)){
                isValid = false;
            }
            return isValid;
        }

        public virtual bool Equivilant(IBusinessUnit comparing){
            MockBusinessUnit boxed;
            if ((boxed = (comparing as MockBusinessUnit)) != null){

                return(
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
