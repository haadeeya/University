using static DataAccess.DBConnection;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccess
{
    public class DBConnection : IDBConnection
    {
        public string connectionString;

        public SqlConnection Connection { get; set; }

        public DBConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Connection = new SqlConnection(connectionString);

            OpenConnection();
        }

        public void/*Task*/ OpenConnection()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }

                Connection.Open();
            }

            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public void/*Task*/ CloseConnection()
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
            }
            //return Task.CompletedTask;
        }

    }
}
