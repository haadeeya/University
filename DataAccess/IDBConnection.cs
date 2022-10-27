using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDBConnection
    {
         /*Task*/ void OpenConnection();
         /*Task*/ void CloseConnection();
        SqlConnection Connection { get; set; }
    }
}
