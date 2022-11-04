using DataAccess;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using University.Utility;
using Utility;

namespace Core.SubjectManager
{
    public class SubjectDAL : ISubjectDAL
    {
        private readonly SqlConnection _conn;

        public SubjectDAL(SqlConnection conn)
        {
            _conn = conn;
        }

        public Task<Subject> CreateAsync(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);
            try
            {
                string query = @"SELECT [SubjectId], [SubjectName] FROM [Subject]";
                List<SqlParameter> parameters = new List<SqlParameter>();
                
                DataTable dataTable = await helper.GetData(query, parameters);


                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                return DataTableMapper.MapTo<Subject>(dataTable).ToList();
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<Subject> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> UpdateAsync(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
