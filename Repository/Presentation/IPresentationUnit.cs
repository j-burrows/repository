/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       IPresentationUnit.cs
 |  Purpose:    Declares the behaviour a renderable object.
 |  Author:     Jonathan Burrows
 |  Updated:    October 5th 2013
 +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
*/
namespace Repository.Presentation{
    public interface IPresentationUnit{
        /*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         |  Subroutine: Format
         |  Purpose:    Formats the objects members for rendering.
         +-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
         */
        void Format();
    }
}
