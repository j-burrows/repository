/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       IBusinessRepository.cs
 |  Purpose:    Renderable object which has validation and verification functions.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using Repository.Presentation;

namespace Repository.Business{
    public interface IBusinessUnit : IPresentationUnit{
        ReadsafeDictionary ValidationErrors { get; set; }

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: CreateValid
         |  Purpose:    Returns whether the object is valid for adding to a collection.
         |  Return:     true        The entity is valid to be entered.
         |              false       The entity is not valid to be entered.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        bool CreateValid();

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: UpdateValid
         |  Purpose:    Returns whether the object is valid for updating in a collection.
         |  Return:     true        The entity is valid to be updated.
         |              false       The entity is not valid to be updated.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        bool UpdateValid();

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: DeleteValid
         |  Purpose:    Returns whether the object is valid for updating in a collection.
         |  Return:     true        The entity is valid to be deleted.
         |              false       The entity is not valid to be deleted.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        bool DeleteValid();

        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Equivilant
         |  Purpose:    Returns whether the object is equal, in business terms, to another
         |              business unit.
         |  Return:     true        The entity matches the given in business context.
         |              false       The entity does not match the given in businn context.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        bool Equivilant(IBusinessUnit comparing);
    }
}
