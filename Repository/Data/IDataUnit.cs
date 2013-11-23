using System.Data;
/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       IDataUnit.cs
 |  Purpose:    Declares a keyable unit which follows business rules.
 |  Updated:    October 5th 2013
*/
// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using Repository.Business;
using Repository.Data.Security;
namespace Repository.Data{
    public interface IDataUnit : IBusinessUnit, IEncoded{
        int key { get; set; }
        string dataError { get; set; }

        void InitFromRow(DataRow row);
    }
}
