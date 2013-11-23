/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       BusinessRepository.cs
 |  Purpose:    Generic collectin of verifiable and validatable objects.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using Repository.Presentation;
using System.Linq;
namespace Repository.Business{
    public class BusinessRepository<T> :
                 PresentationRepository<T>,
                 IBusinessRepository<T>
                 where T : IBusinessUnit{
        public virtual void Create(IBusinessUnit creating) {
            if(CreateValid(creating)){
                CreateEval((T)creating);
            }
        }

        public virtual void Update(IBusinessUnit updating) { 
            if(UpdateValid(updating)){
                UpdateEval((T)updating);
            }
        }

        public virtual void Delete(IBusinessUnit deleting) { 
            if(DeleteValid(deleting)){
                DeleteEval((T)deleting);
            }
        }

        public virtual bool CreateValid(IBusinessUnit validating) {
            return validating is T
                && validating.CreateValid()
                && !this.Any(x => x.Equivilant(validating));
        }

        public virtual bool UpdateValid(IBusinessUnit validating){
            return validating is T
                && validating.UpdateValid()
                && this.Any(x => x.Equivilant(validating));
        }

        public virtual bool DeleteValid(IBusinessUnit validating){
            return validating is T
                && validating.DeleteValid()
                && this.Any(x => x.Equivilant(validating));
        }

        protected virtual void CreateEval(T creating){
            Add((T)creating);
            
        }

        protected virtual void UpdateEval(T updating){
            for (int i = 0; i < Count; i++){
                if (updating.Equivilant(this[i])){
                    this[i] = (T)updating;
                }
            }            
        }

        protected virtual void DeleteEval(T deleting){
            for (int i = 0; i < Count; i++){
                if (deleting.Equivilant(this[i])){
                    RemoveAt(i);
                }
            }
        }
    }
}
