using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Business;
using Repository.Business.Protocols;
using RepositoryUnitTests;
using RepositoryUnitTests.Business;
using RepositoryUnitTests.Data;

namespace DebugTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Arrange: Create an empty repository
            BusinessRepository<MockBusinessUnit> collection = new MockBusinessRepository();

            //Act: an entry is added
            collection.Create(new MockBusinessUnit());
            /*
            Repository.Configuration.connString = "Server=localhost;Database=ApplicationData;Trusted_Connection=True;";
            string connString = Repository.Configuration.connString;
            int result;
             * /
            /*Disposable items required to create the transaction are declared.*/
            /*
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("TESTING_PROCEDURES", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("@i", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add(returnValue);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                result = (int)returnValue.Value;
            }
            Console.WriteLine(result);
            */
        }
    }
}
