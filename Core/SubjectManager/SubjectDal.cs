using DataAccess;
using Interface;
using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using University.Utility;
using IDbCommand = Interface.IDbCommand;

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

                var subjects =  await _dbCommand.GetSubjects(query);
                if(subjects == null)return null;
                return subjects;
                
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<Subject> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
