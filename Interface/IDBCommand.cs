using Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDbCommand
    {
        IDbConnection Connection { get; }
        Task<DataTable> GetData(string query);
        Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, IDbTransaction transaction = null);
        Task<DataTable> GetDataWithConditions(string query, List<SqlParameter> parameters);
    }
}
