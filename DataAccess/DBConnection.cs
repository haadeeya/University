using static DataAccess.DBConnection;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
        public class DBConnection : IDBConnection
        {

            public const string connectionString = @"server=localhost;database=StudentRegistration;uid=wbpoc;pwd=sql@tfs2008";


            public SqlConnection Connection { get; set; }



            public DBConnection()

            {

                Connection = new SqlConnection(connectionString);

                OpenConnection();

            }



            public Task OpenConnection()

            {

                try

                {

                    if (Connection.State == System.Data.ConnectionState.Open)

                    {

                        Connection.Close();

                    }

                    // without this, authentication works but update needs this to be able to work correctly !!!

                    Connection.Open();

                }

                catch (SqlException ex)

                {
                    throw ex;
                }

                return Task.CompletedTask;
            }


            public Task CloseConnection()

            {

                if (Connection != null && Connection.State == System.Data.ConnectionState.Open)

                {

                    Connection.Close();

                    Connection.Dispose();

                }
                return Task.CompletedTask;
        }

        }
}
