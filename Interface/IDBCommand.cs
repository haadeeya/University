﻿using Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDbCommand<T> where T : class
    {
        IDbConnection Connection { get; }
        Task<IEnumerable<T>> GetData(string query);
        Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, IDbTransaction transaction = null);
        Task<T> GetDataWithConditions(string query, List<SqlParameter> parameters);

        Task<List<T>> GetAll(string query);
    }
}
