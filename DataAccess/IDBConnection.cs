using System.Data.SqlClient;

namespace DataAccess
{
    public interface IDBConnection
    {
        void OpenConnection();
        void CloseConnection();
        SqlConnection connection { get; set; }
    }
}
