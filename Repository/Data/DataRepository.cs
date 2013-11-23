/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       DataRepository.cs
 |  Purpose:    Generic collection of keyable items which can communicate with a database.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using System;
using System.Data;
using Repository.Business;
namespace Repository.Data{
    public abstract class DataRepository<T> : 
                 BusinessRepository<T>, 
                 IDataRepository<T>
                 where T : IDataUnit, new(){
        
        protected string connString;
        
        public DataRepository(): base(){
            connString = Repository.Configuration.connString;
        }

        public abstract void ExecNonReader(string query);

        public abstract void ExecNonReader(IDbCommand query);

        public abstract DataTable ExecReader(string query);

        public abstract int ExecStoredProcedure(IDbCommand query);

        public override sealed void Create(IBusinessUnit creating){
            IDataUnit converted;
            if((converted = creating as IDataUnit) != null){
                converted.Scrub();
            }
            base.Create(creating);
        }

        public override sealed void Update(IBusinessUnit updating){
            IDataUnit converted;
            if ((converted = updating as IDataUnit) != null){
                converted.Scrub();
            }
            base.Update(updating);
        }

        public override sealed void Delete(IBusinessUnit deleting){
            IDataUnit converted;
            if ((converted = deleting as IDataUnit) != null){
                converted.Scrub();
            }
            base.Delete(deleting);
        }

        public virtual void FillRepository(string query){
            DataTable results = ExecReader(query);
            foreach (DataRow row in results.Rows){
                Add(ParseDataRow(row));
            }
            Scrub();                        //Any malicious entries are cleaned.
        }

        public virtual T ParseDataRow(DataRow row) {
            //An instance is created based on the default object instantiator.
            T constructing = new T();
            constructing.InitFromRow(row);
            return constructing;
        }

        public void Scrub() { 
            foreach(IDataUnit unit in this){
                //The entire collection is removed of any malicious data.
                unit.Scrub();
            }
        }
    }
}
