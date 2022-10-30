using DataAccess;
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
        public async Task<Student> Create(Student entity)
        {
            try
            {
                string query = @"INSERT INTO [Student] 
                                 VALUES(@StudentId, @UserId, @NID, @Name, @Surname, @GuardianName, @EmailAddress, @DateOfBirth, @PhoneNumber);
                                 INSERT INTO [StudentSubject] VALUES(@SStudentId, @SubjectId, @Grade)";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@StudentId", entity.Id));
                parameters.Add(new SqlParameter("@UserId", entity.UserId));
                parameters.Add(new SqlParameter("@NID", entity.NID));
                parameters.Add(new SqlParameter("@Name", entity.Name));
                parameters.Add(new SqlParameter("@Surname", entity.Surname));
                parameters.Add(new SqlParameter("@GuardianName", entity.GuardianName));
                parameters.Add(new SqlParameter("@EmailAddress", entity.EmailAddress));
                parameters.Add(new SqlParameter("@DateOfBirth", entity.DateOfBirth));
                parameters.Add(new SqlParameter("@PhoneNumber", entity.PhoneNumber));
                foreach(var subject in entity.Subjects)
                {
                    parameters.Add(new SqlParameter("@SStudentId", subject.StudentId));
                    parameters.Add(new SqlParameter("@SubjectId", subject.SubjectId));
                    parameters.Add(new SqlParameter("@Grade", subject.Grade));
                }


                var result =  await _dbCommand.UpdateAndInsertData(query, parameters);

                return result > 0 ? entity : null;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        public Task<bool> Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Student>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetbyId(int id)
        {
            try
            {
                string query = @"SELECT * FROM [Student] s
                                INNER JOIN StudentSubject ss on ss.StudentId = s.StudentId
                                INNER JOIN Subject sb on sb.SubjectId = ss.SubjectId
                                WHERE s.StudentId = @StudentId";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@StudentId", id));

                Student student = new Student();
                List<StudentSubject> allsubjects = new List<StudentSubject>();

                var dt =  await _dbCommand.GetDataWithConditions(query, parameters);

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
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<Student> Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
