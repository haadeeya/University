using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDbCommandSubject: IDbCommand<Subject>
    {
        IDbConnection Connection { get; }
        Task<IEnumerable<Subject>> GetData(string query);
        Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, IDbTransaction transaction = null);
        Task<Subject> GetDataWithConditions(string query, List<SqlParameter> parameters);

        Task<List<Subject>> GetAll(string query);
    }
}
