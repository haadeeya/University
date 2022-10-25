using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDBConnection
    {
         Task OpenConnection();
         Task CloseConnection();
        SqlConnection Connection { get; set; }
    }
}
