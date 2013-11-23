/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       IDataRepository.cs
 |  Purpose:    Declares the behaviour of a collection of keyable items, and how to interact
 |              with a database.
 |  Author:     Jonathan Burrows
 |  Updated:    October 5th 2013
 +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
*/
using System.Data;
using Repository.Business;
using Repository.Data.Security;
namespace Repository.Data{
    public interface IDataRepository<out T> : 
                     IBusinessRepository<T>,
                     IEncoded
                     where T : IDataUnit{
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: ExecNonReader
         |  Purpose:    Executes a query to a database which does not require a response.
         |  Param:      query       The transaction which will take place.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        void ExecNonReader(string query);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: ExecNonReader
         |  Purpose:    Executes a query to a database which does not require a response.
         |  Param:      query       The transaction which will take place.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        void ExecNonReader(IDbCommand query);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Function:   ExecReader
         |  Purpose:    Executes a query to a database and returns the resulting dataset.
         |  Param:      query       The transaction which will take place.
         |  Return:     DataTable   The first resulting entry in the transaction dataset.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        DataTable ExecReader(string query);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Function:   ExecStoredProcedure
         |  Purpose:    Executes a stored procedure and returns the return value.
         |  Param:      query       The transaction which will take place.
         |  Return:     T           The return value of the stored procedure once complete.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        int ExecStoredProcedure(IDbCommand query);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: FillRepository
         |  Purpose:    Generates a result from a query, and parses that result into a set
         |              of items which are stored in the collection
         |  Param:      query       The transaction which will create the dataset to fill
         |                          the collection.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        void FillRepository(string query);
    }
}
