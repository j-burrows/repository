/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       PresentationRepository.cs
 |  Purpose:    Defines the behaviour for a collection of renderable objects.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using System.Collections.Generic;
namespace Repository.Presentation{
    public class PresentationRepository<T> : 
                 List<T>,
                 IPresentationRepository<T> 
                 where T : IPresentationUnit{
        public void Format() {
            ForEach(x => x.Format());
        }
    }
}
