using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDbCommand
    {
        Task<DataTable> GetData(string query);
        Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters);
        Task<DataTable> GetDataWithConditions(string query, List<SqlParameter> parameters);
    }
}
