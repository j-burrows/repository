using System.Data;
using Repository.Business;
using Repository.Data;
using Repository.Helpers;
using RepositoryUnitTests.Business;
namespace RepositoryUnitTests.Data{
    public class MockPolymorphicDataUnit : MockBusinessUnit, IDataUnit{
        public int key { get { return attrOne; } set { attrOne = value; } }
        public string dataError { get; set; }

        public void InitFromRow(DataRow row) { }

        public void Scrub() {
            attrTwo = attrTwo.Scrub();
        }

        public override bool Equivilant(IBusinessUnit comparing){
            MockDataUnit holder;
            if ((holder = comparing as MockDataUnit) != null){
                return holder.key == key
                    && base.Equivilant(comparing);
            }
            return false;
        }

    }
}
