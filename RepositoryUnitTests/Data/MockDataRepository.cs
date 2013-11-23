using System.Data;
using Repository.Data;

namespace RepositoryUnitTests.Data{
    public class MockSqlRepository : SqlSRepository<MockDataUnit>{
        public MockSqlRepository() : base(){
            FillRepository("");
        }
        
        public override void FillRepository(string query){
            for (int i = 0; i < 3; i++)
            {
                Add(new MockDataUnit { attrOne = i, attrTwo = "Entry " + i.ToString(), attrThree = 1 });
            }
        }
        public override MockDataUnit ParseDataRow(DataRow row){
            return new MockDataUnit();
        }
    }
}
