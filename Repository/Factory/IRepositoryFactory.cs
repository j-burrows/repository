using System.Collections.Generic;
using Repository.Data;
namespace Repository.Factory{
    public interface IRepositoryFactory{
        IDataRepository<T> Construct<T>(params object[] args) where T : IDataUnit;
    }
}
