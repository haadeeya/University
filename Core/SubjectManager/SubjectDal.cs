using DataAccess;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using University.Utility;
using IDbCommand = Interface.IDbCommandSubject;

namespace Core.SubjectManager
{
    public class SubjectDal : ISubjectDAL
    {
        private readonly IDbCommand _dbCommand;
        public SubjectDal()
        {
            _dbCommand = new DBCommand();
        }
        public Task<Subject> Create(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            try
            {
                string query = @"SELECT [SubjectId], [SubjectName] FROM [Subject]";
                List<SqlParameter> parameters = new List<SqlParameter>();

                var subjects =  await _dbCommand.GetAll(query);
                if(subjects == null)return null;
                return subjects;
                
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public async Task<Subject> GetById(int id)
        {
            string query = @"SELECT [SubjectName], [SubjectId] FROM [Subject] s
                                WHERE SubjectId = @SubjectId";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@SubjectId", id));

            var dt = await _dbCommand.GetDataWithConditions(query, parameters);

            return dt;
        }

        public Task<Subject> Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
