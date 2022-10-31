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
            string insertStudentQuery = @"INSERT INTO [Student]
                                 VALUES(@StudentId, @UserId, @Name, @Surname, @NID, @GuardianName, @EmailAddress, @DateOfBirth, @PhoneNumber);";
            string insertSubjectQuery = @"INSERT INTO [StudentSubject] VALUES(@StudentId, @SubjectId, @Grade);";


            IDbTransaction transaction = _dbCommand.Connection.BeginTransaction();

            try
            {
                List<SqlParameter> insertStudentParameters = new List<SqlParameter>();
                insertStudentParameters.Add(new SqlParameter("@StudentId", entity.Id));
                insertStudentParameters.Add(new SqlParameter("@UserId", entity.UserId));
                insertStudentParameters.Add(new SqlParameter("@NID", entity.NID));
                insertStudentParameters.Add(new SqlParameter("@Name", entity.Name));
                insertStudentParameters.Add(new SqlParameter("@Surname", entity.Surname));
                insertStudentParameters.Add(new SqlParameter("@GuardianName", entity.GuardianName));
                insertStudentParameters.Add(new SqlParameter("@EmailAddress", entity.EmailAddress));
                insertStudentParameters.Add(new SqlParameter("@DateOfBirth", entity.DateOfBirth));
                insertStudentParameters.Add(new SqlParameter("@PhoneNumber", entity.PhoneNumber));

                await _dbCommand.UpdateAndInsertData(insertStudentQuery, insertStudentParameters, transaction);

                foreach (var subject in entity.Subjects)
                {
                    List<SqlParameter> insertSubjectParameters = new List<SqlParameter>();

                    insertSubjectParameters.Add((new SqlParameter("@StudentId", subject.StudentId)));
                    insertSubjectParameters.Add((new SqlParameter("@SubjectId", subject.SubjectId)));
                    insertSubjectParameters.Add((new SqlParameter("@Grade", subject.Grade)));

                    await _dbCommand.UpdateAndInsertData(insertSubjectQuery, insertSubjectParameters, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                //transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }

            return entity;
        }

        public Task<bool> Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Student>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetById(int id)
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

                var dt = await _dbCommand.GetDataWithConditions(query, parameters);

                if (dt.Rows.Count > 0)
                {
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
