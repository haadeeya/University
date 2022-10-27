using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDbCommand
    {
        DataTable GetData(string query);
        int UpdateAndInsertData(string query, List<SqlParameter> parameters);
        DataTable GetDataWithConditions(string query, List<SqlParameter> parameters);
    }
}
