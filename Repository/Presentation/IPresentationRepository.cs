/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       IPresentationRepository.cs
 |  Purpose:    Declares the behaviour for a collection of renderable objects.
 |  Author:     Jonathan Burrows
 |  Updated:    October 5th 2013
 +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
*/
using System.Collections.Generic;
namespace Repository.Presentation{
    public interface IPresentationRepository<out T> : 
                     IEnumerable<T>
                     where T : IPresentationUnit{
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Format
         |  Purpose:    Format all members of the repository to be renderable.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        void Format();
    }
}
