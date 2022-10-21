using static DataAccess.DBConnection;
using System.Data.SqlClient;

namespace DataAccess
{
        public class DBConnection : IDBConnection
        {

            public const string connectionString = @"server=localhost;database=StudentRegistration;uid=wbpoc;pwd=sql@tfs2008";


            public SqlConnection connection { get; set; }



            public DBConnection()

            {

                connection = new SqlConnection(connectionString);

                OpenConnection();

            }



            public void OpenConnection()

            {

                try

                {

                    if (connection.State == System.Data.ConnectionState.Open)

                    {

                        connection.Close();

                    }

                    // without this, authentication works but update needs this to be able to work correctly !!!

                    connection.Open();

                }

                catch (SqlException ex)

                {

                    throw ex;

                }

            }



            public void CloseConnection()

            {

                if (connection != null && connection.State == System.Data.ConnectionState.Open)

                {

                    connection.Close();

                    connection.Dispose();

                }

            }

        }
}
