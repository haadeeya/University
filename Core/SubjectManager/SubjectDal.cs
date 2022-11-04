using DataAccess;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using University.Utility;

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
            try
            {
                string query = @"SELECT [SubjectId], [SubjectName] FROM [Subject]";
                List<SqlParameter> parameters = new List<SqlParameter>();

                DataTable dataTable =  await (new ConnectionHelper(_conn)).GetData(query);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }
            
                List<Subject> subjects = new List<Subject>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Subject subject = new Subject(Convert.ToInt32(row["SubjectId"]), row["SubjectName"].ToString());
                    subjects.Add(subject);
                }

                return subjects;
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
