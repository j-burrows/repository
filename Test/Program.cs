using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Business;
using Repository.Concrete;
using Repository.Data;
using Repository.Presentation;
namespace Test
{
    class fun : IPresentationUnit {
        public void Format() { }
    }
    class bun : fun, IBusinessUnit {
        public bool CreateValid() { return true; }
        public bool UpdateValid() { return true; }
        public bool DeleteValid() { return true; }
        public bool Equivilant(IBusinessUnit comparing) { return true; }
    }
    class MockDataUnit : bun, IDataUnit{
        public int key { get; set; }
        public MockDataUnit(DataRow row) { }
        public MockDataUnit() { }
    }
    class MockDataRepository : SqlSRepository<MockDataUnit> {
        public override MockDataUnit ParseDataRow(DataRow row){
            return new MockDataUnit(row);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<IPresentationUnit> m = new BusinessRepository<bun>();
            IBusinessRepository<IBusinessUnit> n = new BusinessRepository<bun>();
            IEnumerable<fun> o = new MockDataRepository();
            n.Create(new bun());
            n.Update(new bun());
            Console.WriteLine(n.Count());
        }
    }
}
