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

        public async Task<IEnumerable<Subject>> Get()
        {
            try
            {
                string query = @"SELECT [SubjectId], [SubjectName] FROM [Subject]";
                List<SqlParameter> parameters = new List<SqlParameter>();

                Subject thissubject;
                List<Subject> allsubjects = new List<Subject>();

                var dt =  await _dbCommand.GetData(query);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        thissubject = new Subject(Convert.ToInt32(row["SubjectId"]), row["SubjectName"].ToString());
                        allsubjects.Add(thissubject);
                    }
                    return allsubjects;
                }
                else
                {
                    return null;
                }
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
