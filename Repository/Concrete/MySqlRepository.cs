/*-- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
 |  File:       MySqlRepository.cs
 |  Purpose:    Generic ollection of items which can communicate with MySql databases.
 |  Updated:    October 5th 2013
*/// --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
using System.Data;
using MySql.Data.MySqlClient;
using Repository.Data;
namespace Repository.Data{
    public abstract class MySqlRepository<T> : DataRepository<T> where T : IDataUnit, new(){
        public override void ExecNonReader(string query){
            using (MySqlConnection cnx = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, cnx)){
                cnx.Open();

                cmd.ExecuteNonQueryAsync();
            }
        }

        public override void ExecNonReader(IDbCommand query){
            using (MySqlConnection conn = new MySqlConnection(connString)){
                query.Connection = conn;
                conn.Open();

                (query as MySqlCommand).ExecuteNonQueryAsync();
            }
        }

        public override DataTable ExecReader(string query){
            //All database objects required for read are declared.
            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            using (DataSet dataset = new DataSet()){
                adapter.Fill(dataset);
                return dataset.Tables[0];
            }
        }

        public override int ExecStoredProcedure(IDbCommand cmd){
            int result;
            /*Disposable items required to create the transaction are declared.*/
            using (MySqlConnection conn = new MySqlConnection(connString)){
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter returnValue = new MySqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                result = (int)returnValue.Value;
            }
            return result;
        }
    }
}
