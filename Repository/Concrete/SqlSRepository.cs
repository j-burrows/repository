/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       SqlSRepository.cs
 |  Purpose:    Generic collection of items which can communicate with Sql Server database.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Repository.Data;
namespace Repository.Data{
    public abstract class SqlSRepository<T> : DataRepository<T> where T : IDataUnit, new(){
        public override void ExecNonReader(string query){
            using(TransactionScope scope = new TransactionScope())
            using (SqlConnection cnx = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, cnx)){
                try{
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    scope.Complete();
                }
                catch(Exception e){
                    throw new Exception("Could not perform database action: " + e.Message);
                }
                finally{
                    cnx.Close();
                }
            }
        }

        public override void ExecNonReader(IDbCommand query){
            using(TransactionScope scope = new TransactionScope())
            using (SqlConnection conn = new SqlConnection(connString)) {
                query.Connection = conn;
                query.CommandType = CommandType.StoredProcedure;
                try{
                    conn.Open();
                    (query as SqlCommand).ExecuteNonQuery();
                    scope.Complete();
                }
                catch(Exception e){
                    throw new Exception("Could not perform database action: " + e.Message);
                }
                finally{
                    conn.Close();
                }
            }
        }

        public override DataTable ExecReader(string query){
            using(TransactionScope scope = new TransactionScope())
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            using (DataSet dataset = new DataSet()){
                try{
                    adapter.Fill(dataset);
                    scope.Complete();
                    return dataset.Tables[0];   //First table result from query.
                }
                catch(Exception e){
                    throw new Exception("Could not perform database action: " + e.Message);
                }
            }
        }

        public override int ExecStoredProcedure(IDbCommand cmd){
            int result;
            
            using(TransactionScope scope = new TransactionScope())
            using(SqlConnection conn = new SqlConnection(connString)){
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                try{
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    scope.Complete();
                }
                catch(Exception e){
                    throw new Exception("Could not perform database action: " + e.Message);
                }
                finally{
                    conn.Close();                
                }

                result = (int) returnValue.Value;
            }
            return result;
        }
    }
}
