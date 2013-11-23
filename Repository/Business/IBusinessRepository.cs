/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       IBusinessRepository.cs
 |  Purpose:    Generic collection with validation and verification functions.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using Repository.Presentation;
namespace Repository.Business
{
    public interface IBusinessRepository<out T> : 
                     IPresentationRepository<T> 
                     where T : IBusinessUnit{
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Create
         |  Purpose:    Adds an entry to the collection if valid.
         |  Param:      creating        Will be added to the collection if valid.
         |  Alt action: If given is not valid, no changes will be made to collection.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        void Create(IBusinessUnit creating);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Update
         |  Purpose:    Adds an entry to the collection if valid.
         |  Param:      updating        Will be updated in the collection if valid.
         |  Alt action: If given is not valid, no changes will be made to collection.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        void Update(IBusinessUnit updating);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Delete
         |  Purpose:    Adds an entry to the collection if valid.
         |  Param:      deleting        Will be deleted from the collection if valid.
         |  Alt action: If given is not valid, no changes will be made to collection.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        void Delete(IBusinessUnit deleting);

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: CreateValid
         |  Purpose:    Checks if entity is valid for entry given a set of business rules.
         |  Param:      validating  Will be checked if valid for entry.
         |  Return:     true        The given is valid to be added to the collection.
         |              false       The given isnt valid to be added to the collection.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        bool CreateValid(IBusinessUnit validating);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: UpdateValid
         |  Purpose:    Checks if entity is valid for updating given a set of business rules
         |  Param:      validating  Will be checked if valid for updating.
         |  Return:     true        The given is valid to be updated in the collection.
         |              false       The given isnt valid to be updated in the collection
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        bool UpdateValid(IBusinessUnit validating);
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: DeleteValid
         |  Purpose:    Checks if entity is valid for deletion given a set of business rules
         |  Param:      validating  Will be checked if valid for deletion.
         |  Return      true        The given is valid to be deleted from the collection.
         |              false       The given isn't valid to be deleted from the collection.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        */
        bool DeleteValid(IBusinessUnit validating);
    }
}
