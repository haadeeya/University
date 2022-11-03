using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDbCommandStudent : IDbCommand<Student>
    {
        IDbConnection Connection { get; }
        Task<DataTable> GetData(string query);
        Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, IDbTransaction transaction = null);
        Task<DataTable> GetDataWithConditions(string query, List<SqlParameter> parameters);

        Task<DataTable> GetAll(string query);
    }
}
