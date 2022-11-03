using DataAccess;
using Interface;
using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using University.Utility;

namespace Core.StudentManager
{
    public class StudentDal : IStudentDAL
    {
        private readonly IDbCommandStudent _dbCommand;

        public StudentDal()
        {
            _dbCommand = new DBCommand();
        }

        public async Task<Student> Create(Student entity)
        {
            string insertStudentQuery = @"INSERT INTO [Student](StudentId, UserId, Name, Surname, NID, GuardianName, EmailAddress, DateOfBirth, PhoneNumber)
                                 VALUES(@StudentId, @UserId, @Name, @Surname, @NID, @GuardianName, @EmailAddress, @DateOfBirth, @PhoneNumber);";
            string insertSubjectQuery = @"INSERT INTO [StudentSubject](StudentId, SubjectId, Grade) VALUES(@StudentId, @SubjectId, @Grade);";


            if (_dbCommand.Connection.State == ConnectionState.Closed)
            {
                _dbCommand.Connection.Open();
            }

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
                transaction.Rollback();
                throw;
            }
            finally
            {
                _dbCommand.Connection.Close();
                transaction.Dispose();
            }

            return entity;
        }

        public Task<bool> Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
    try
    {
        string query = @"SELECT s.[StudentId], [UserId], [Name], [Surname], 
                            [NID], [GuardianName], [EmailAddress], [DateOfBirth], [PhoneNumber], [SubjectName],
                            sb.[SubjectId], [StudentSubjectId], [Grade]
                            FROM [Student] s
                            INNER JOIN [StudentSubject] ss on ss.StudentId = s.StudentId
                            INNER JOIN [Subject] sb on sb.SubjectId = ss.SubjectId";

        List<Student> allstudents = new List<Student>();
        Student student = new Student();

        var dt = await _dbCommand.GetData(query);

    var result = dt.AsEnumerable()
                .GroupBy(x => new { Id = x.Field<int>("StudentId"),
                    UserId = x.Field<int>("UserId"), Name = x.Field<string>("Name"),
                    Surname = x.Field<string>("Surname"), GuardianName = x.Field<string>("GuardianName"),
                    NID = x.Field<string>("NID"), EmailAddress = x.Field<string>("EmailAddress"),
                    DateOfBirth = x.Field<DateTime>("DateOfBirth"), PhoneNumber = x.Field<string>("PhoneNumber")
                })
                    .Select(x => new Student()
                    {
                        Id = x.Key.Id,
                        UserId = x.Key.UserId,
                        Name = x.Key.Name,
                        Surname = x.Key.Surname,
                        GuardianName = x.Key.GuardianName,
                        NID = x.Key.NID,
                        EmailAddress = x.Key.EmailAddress,
                        DateOfBirth = x.Key.DateOfBirth,
                        PhoneNumber = x.Key.PhoneNumber,
                        Subjects = x.Select(y => new StudentSubject() { 
                            StudentSubjectId = y.Field<int>("StudentSubjectId"), Grade = y.Field<string>("Grade"),
                            StudentId = y.Field<int>("StudentId"), SubjectId = y.Field<int>("SubjectId"),
                            Subject = new Subject(y.Field<int>("SubjectId"), y.Field<string>("SubjectName"))
                        }).ToList()
                    });

        return result;
    }
        catch (Exception exception)
        {
            MyLogger.GetInstance().Error($"Error {exception.Message}");
            throw;
        }
        finally
        {
            _dbCommand.Connection.Close();
        }
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
            finally
            {
                _dbCommand.Connection.Close();
            }
        }

        public Task<Student> Update(Student entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateStatus(List<Student> students)
        {
            bool isStatusUpdated = false;
            string updateStudentQuery = @"UPDATE [Student]
                                          SET [Status] = @Status
                                          WHERE [StudentId] = @StudentId;";


            if (_dbCommand.Connection.State == ConnectionState.Closed)
            {
                _dbCommand.Connection.Open();
            }

            IDbTransaction transaction = _dbCommand.Connection.BeginTransaction();
            try
            {
                
                foreach (var student in students)
                {
                    List<SqlParameter> updateStudentParameters = new List<SqlParameter>();
                    updateStudentParameters.Add((new SqlParameter("@StudentId", student.Id)));
                    updateStudentParameters.Add((new SqlParameter("@Status", student.Status)));

                    await _dbCommand.UpdateAndInsertData(updateStudentQuery, updateStudentParameters, transaction);
                }
                transaction.Commit();
                isStatusUpdated = true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                _dbCommand.Connection.Close();
                transaction.Dispose();
            }

            return isStatusUpdated;
        }
    }
}
