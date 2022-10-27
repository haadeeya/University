using DataAccess;
using Interface;
using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using University.Utility;
using IDbCommand = Interface.IDbCommand;

namespace Core.SubjectManager
{
    public class SubjectDal : IRepositoryDal<Subject>
    {
        private readonly IDbCommand _dbCommand;
        public SubjectDal()
        {
            _dbCommand = new DBCommand();
        }
        public Subject Create(Subject entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subject> Get()
        {
            try
            {
                string query = @"SELECT [SubjectId], [SubjectName] FROM [Subject]";
                List<SqlParameter> parameters = new List<SqlParameter>();

                Subject thissubject;
                List<Subject> allsubjects = new List<Subject>();

                var dt =  _dbCommand.GetData(query);

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
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                return null;
            }
        }

        public Subject GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        public Subject Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
