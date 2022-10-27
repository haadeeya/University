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

namespace Core.StudentManager
{
    public class StudentDal : IRepositoryDal<Student>
    {
        private readonly Interface.IDbCommand _dbCommand;
        public StudentDal()
        {
            _dbCommand = new DBCommand();
        }
        public Student Create(Student entity)
        {
            try
            {
                string query = @"INSERT INTO [Student](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role);
                                 INSERT INTO [StudentSubject](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Email", entity.EmailAddress));


                var result =  _dbCommand.UpdateAndInsertData(query, parameters);

                return result > 0 ? entity : null;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        public bool Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Student> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student GetbyId(int id)
        {
            try
            {
                string query = @"SELECT * FROM [Student] s
                                INNER JOIN StudentSubject ss on ss.StudentId = s.StudentId
                                INNER JOIN Subject sb on sb.SubjectId = ss.SubjectId
                                WHERE s.StudentId = @StudentId";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@StudentId", id));

                Model.Student student = new Model.Student();
                List<StudentSubject> allsubjects = new List<StudentSubject>();

                var dt =  _dbCommand.GetDataWithConditions(query, parameters);

                if (dt.Rows.Count > 0) { 
                    foreach (DataRow row in dt.Rows)
                    { 
                        student.Id = Convert.ToInt32(row["StudentId"]);
                        student.Surname = row["Surname"].ToString();
                        student.Name = row["Name"].ToString();
                        student.GuardianName = row["GuardianName"].ToString();
                        student.EmailAddress = row["EmailAddress"].ToString();
                        student.NID = row["NID"].ToString();
                        student.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                        student.PhoneNumber = row["PhoneNumber"].ToString();
                        allsubjects.Add(new StudentSubject(Convert.ToInt32(row["StudentId"]), Convert.ToInt32(row["SubjectId"]), Convert.ToInt32(row["StudentSubjectId"]), new Subject(Convert.ToInt32(row["SubjectId"]), row["SubjectName"].ToString()), row["Grade"].ToString()));
                        student.Subjects = allsubjects;
                    }
                    return student;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        public Student Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
