using System.Data;
using Repository.Data;
namespace Repository.Factory{
    public interface IDataUnitFactory{
        T Construct<T>() where T : IDataUnit, new();

        T ConstructFromRow<T>(DataRow parsing) where T : class, IDataUnit, new();
    }
}
