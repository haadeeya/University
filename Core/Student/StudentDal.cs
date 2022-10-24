using DataAccess;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;
namespace Core.Student
{
    public class StudentDal : IStudentDal
    {
        private readonly IDBCommand _dBCommand;
        public StudentDal()
        {
            _dBCommand = new DBCommand();
        }
        public Model.Student Create(Model.Student student)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Model.Student GetbyId(int id)
        {
            try
            {
                //string query = $"SELECT * FROM [Student] WHERE StudentID = @StudentId";
                //List<SqlParameter> parameters = new List<SqlParameter>();

                //parameters.Add(new SqlParameter("@StudentId", id));
                //Model.Student student = new Model.Student();
                //Model.StudentSubject studentSubject = new StudentSubject();
                //Model.Subject subject = new Subject();

                //var dt = _dBCommand.GetDataWithConditions(query, parameters);
                //foreach (DataRow row in dt.Rows)
                //{
                //    query = $"SELECT * FROM [StudentSubject] WHERE StudentID = @StudentId";

                //    parameters.Add(new SqlParameter("@StudentId", id));
                //    studentSubject.StudentId = Convert.ToInt32(row["StudentID"]);
                //    studentSubject.SubjectId = Convert.ToInt32(row["SubjectID"]);

                //    query = $"SELECT [Name] FROM [Subject] WHERE SubjectID = @SubjectId";
                //    parameters.Add(new SqlParameter("@SubjectId", studentSubject.SubjectId));
                //    subject.ID = studentSubject.SubjectId;
                //    subject.Name = row["Name"].ToString();
                //    studentSubject.Subject = subject;

                //    studentSubject.Grade = row["Grade"].ToString();
                //    student.Subjects.Add(studentSubject);
                //    student.StudentId = Convert.ToInt32(row["StudentID"]);
                //    student.Surname = row["Surname"].ToString();
                //    student.Name = row["Name"].ToString();
                //    student.GuardianName = row["Guardian"].ToString();
                //    student.EmailAddress = row["Email"].ToString();
                //    student.NID = row["NID"].ToString();
                //    student.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                //    student.PhoneNumber = row["PhoneNumber"].ToString();
                //}

                string query = @"SELECT * FROM [Student] s
                                INNER JOIN StudentSubject ss on ss.StudentID = s.StudentID
                                INNER JOIN Subject sb on sb.ID = ss.SubjectID
                                WHERE s.StudentID = @StudentId";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@StudentId", id));

                Model.Student student = new Model.Student();
                List<StudentSubject> allsubjects = new List<StudentSubject>();

                var dt = _dBCommand.GetDataWithConditions(query, parameters);

                foreach (DataRow row in dt.Rows)
                { 
                    student.Id = Convert.ToInt32(row["StudentID"]);
                    student.Surname = row["Surname"].ToString();
                    student.Name = row["Name"].ToString();
                    student.GuardianName = row["GuardianName"].ToString();
                    student.EmailAddress = row["EmailAddress"].ToString();
                    student.NID = row["NID"].ToString();
                    student.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                    student.PhoneNumber = row["PhoneNumber"].ToString();
                    allsubjects.Add(new StudentSubject(Convert.ToInt32(row["StudentID"]), Convert.ToInt32(row["SubjectID"]), Convert.ToInt32(row["StudentSubjectID"]), new Subject(Convert.ToInt32(row["ID"]), row["SubjectName"].ToString()), row["Grade"].ToString()));
                    student.Subjects = allsubjects;

                }

                return student;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        public IEnumerable<Model.Student> Get()
        {
            throw new System.NotImplementedException();
        }

        public Model.Student Update(Model.Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
