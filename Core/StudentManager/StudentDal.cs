using DataAccess;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using University.Utility;

namespace Core.StudentManager
{
    public class StudentDAL : IStudentDAL
    {
        private readonly SqlConnection _conn;

        public StudentDAL(SqlConnection conn)
        {
            _conn = conn;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            string insertStudentQuery = @"INSERT INTO [Student](StudentId, UserId, Name, Surname, NID, GuardianName, EmailAddress, DateOfBirth, PhoneNumber, Status)
                                 VALUES(@StudentId, @UserId, @Name, @Surname, @NID, @GuardianName, @EmailAddress, @DateOfBirth, @PhoneNumber, @Status);";
            string insertSubjectQuery = @"INSERT INTO [StudentSubject](StudentId, SubjectId, Grade) VALUES(@StudentId, @SubjectId, @Grade);";


            ConnectionHelper helper = new ConnectionHelper(_conn);
            SqlTransaction transaction = await helper.BeginTransaction();

            try
            {
                List<SqlParameter> insertStudentParameters = new List<SqlParameter>();
                Type studentType = student.GetType();
                //insertStudentParameters.Add(new SqlParameter("@StudentId", entity.Id));
                //insertStudentParameters.Add(new SqlParameter("@UserId", entity.UserId));
                //insertStudentParameters.Add(new SqlParameter("@NID", entity.NID));
                //insertStudentParameters.Add(new SqlParameter("@Name", entity.Name));
                //insertStudentParameters.Add(new SqlParameter("@Surname", entity.Surname));
                //insertStudentParameters.Add(new SqlParameter("@GuardianName", entity.GuardianName));
                //insertStudentParameters.Add(new SqlParameter("@EmailAddress", entity.EmailAddress));
                //insertStudentParameters.Add(new SqlParameter("@DateOfBirth", entity.DateOfBirth));
                //insertStudentParameters.Add(new SqlParameter("@PhoneNumber", entity.PhoneNumber));
                //insertStudentParameters.Add(new SqlParameter("@Status", entity.Status));

                foreach (var prop in student.GetType().GetProperties())
                {
                    Debug.WriteLine(IsPrimitive(prop.PropertyType));
                    if (IsPrimitive(prop.PropertyType) && prop.Name!="Marks")
                    {
                        insertStudentParameters.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(student, null)));
                    }
                }

                await helper.UpdateAndInsertData(insertStudentQuery, insertStudentParameters, transaction);

                foreach (var subject in student.Subjects)
                {
                    List<SqlParameter> insertSubjectParameters = new List<SqlParameter>();
                    Type subjectType = subject.GetType();

                    foreach(var property in subject.GetType().GetProperties())
                    {
                        if (property.Name.Equals("StudentId") || property.Name.Equals("SubjectId") || property.Name.Equals("Grade"))
                        {
                            insertSubjectParameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(subject, null)));
                        }
                    }
                    //insertSubjectParameters.Add(new SqlParameter("@StudentId", subject.StudentId));
                    //insertSubjectParameters.Add(new SqlParameter("@SubjectId", subject.SubjectId));
                    //insertSubjectParameters.Add(new SqlParameter("@Grade", subject.Grade));

                    await helper.UpdateAndInsertData(insertSubjectQuery, insertSubjectParameters, transaction);
                }

                transaction.Commit();
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }

            return student;
        }

        public Task<bool> DeleteAsync(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);

            try
            {
                string query = @"SELECT s.[StudentId], [UserId], [Name], [Surname], 
                            [NID], [GuardianName], [EmailAddress], [DateOfBirth], [PhoneNumber], [SubjectName],
                            sb.[SubjectId], [StudentSubjectId], [Grade], [Status]
                            FROM [Student] s
                            INNER JOIN [StudentSubject] ss on ss.StudentId = s.StudentId
                            INNER JOIN [Subject] sb on sb.SubjectId = ss.SubjectId";

                DataTable dataTable = await helper.GetData(query);
                IEnumerable<Student> result = dataTable.AsEnumerable()
                    .GroupBy(x => new
                    {
                        StudentId = x.Field<int>("StudentId"),
                        UserId = x.Field<int>("UserId"),
                        Name = x.Field<string>("Name"),
                        Surname = x.Field<string>("Surname"),
                        GuardianName = x.Field<string>("GuardianName"),
                        NID = x.Field<string>("NID"),
                        EmailAddress = x.Field<string>("EmailAddress"),
                        DateOfBirth = x.Field<DateTime>("DateOfBirth"),
                        PhoneNumber = x.Field<string>("PhoneNumber"),
                        Status = x.Field<string>("Status")
                    })
                    .Select(x => new Student()
                    {
                        StudentId = x.Key.StudentId,
                        UserId = x.Key.UserId,
                        Name = x.Key.Name,
                        Surname = x.Key.Surname,
                        GuardianName = x.Key.GuardianName,
                        NID = x.Key.NID,
                        EmailAddress = x.Key.EmailAddress,
                        DateOfBirth = x.Key.DateOfBirth,
                        PhoneNumber = x.Key.PhoneNumber,
                        Status = x.Key.Status,
                        Subjects = x.Select(y => new StudentSubject()
                        {
                            StudentSubjectId = y.Field<int>("StudentSubjectId"),
                            Grade = y.Field<string>("Grade"),
                            StudentId = y.Field<int>("StudentId"),
                            SubjectId = y.Field<int>("SubjectId"),
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
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);

            try
            {
                string query = @"SELECT * FROM [Student] s
                                INNER JOIN StudentSubject ss on ss.StudentId = s.StudentId
                                INNER JOIN Subject sb on sb.SubjectId = ss.SubjectId
                                WHERE s.StudentId = @StudentId";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@StudentId", id));

                DataTable dataTable = await helper.GetData(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                Student student = new Student();
                List<StudentSubject> subjects = new List<StudentSubject>();

                foreach (DataRow row in dataTable.Rows)
                {
                    student.StudentId = Convert.ToInt32(row["StudentId"]);
                    student.Surname = row["Surname"].ToString();
                    student.Name = row["Name"].ToString();
                    student.GuardianName = row["GuardianName"].ToString();
                    student.EmailAddress = row["EmailAddress"].ToString();
                    student.NID = row["NID"].ToString();
                    student.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                    student.PhoneNumber = row["PhoneNumber"].ToString();

                    subjects.Add(new StudentSubject(
                        Convert.ToInt32(row["StudentId"]),
                        Convert.ToInt32(row["SubjectId"]),
                        Convert.ToInt32(row["StudentSubjectId"]),
                        new Subject(Convert.ToInt32(row["SubjectId"]),
                        row["SubjectName"].ToString()),
                        row["Grade"].ToString())
                    );

                    student.Subjects = subjects;
                }

                return student;

            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<Student> UpdateAsync(Student entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateStatusAsync(List<Student> students)
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);
            SqlTransaction transaction = await helper.BeginTransaction();

            try
            {
                foreach (Student student in students)
                {
                    string updateStudentQuery = @"UPDATE [Student]
                                                  SET [Status] = @Status
                                                  WHERE [StudentId] = @StudentId;";

                    List<SqlParameter> updateStudentParameters = new List<SqlParameter>();
                    updateStudentParameters.Add((new SqlParameter("@StudentId", student.StudentId)));
                    updateStudentParameters.Add((new SqlParameter("@Status", student.Status)));

                    await helper.UpdateAndInsertData(updateStudentQuery, updateStudentParameters, transaction);
                }

                transaction.Commit();

                return true;
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private static bool IsPrimitive(Type t)
        {
            return new[] {
            typeof(string),
            typeof(char),
            typeof(byte),
            typeof(sbyte),
            typeof(ushort),
            typeof(short),
            typeof(uint),
            typeof(int),
            typeof(ulong),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(DateTime),
        }.Contains(t);
        }

    }
}
