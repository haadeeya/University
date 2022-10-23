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
        public Model.Student CreateStudent(Model.Student student)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteStudent(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Model.Student GetStudentbyId(int id)
        {
            try
            {
                string query = $"SELECT * FROM [Student] WHERE StudentID = @StudentId";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@StudentId", id));
                Model.Student student = new Model.Student();
                Model.StudentSubject studentSubject = new StudentSubject();

                var dt = _dBCommand.GetDataWithConditions(query, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    query = $"SELECT * FROM [StudentSubject] WHERE StudentID = @StudentId";

                    parameters.Add(new SqlParameter("@StudentId", id));
                    studentSubject.StudentID = Convert.ToInt32(row["StudentID"]);
                    studentSubject.StudentSubjectId = Convert.ToInt32(row["StudentSubjectID"]);
                    studentSubject.SubjectID = Convert.ToInt32(row["Subject"]);
                    studentSubject.Grade = row["Grade"].ToString();
                    student.Subjects.Add(studentSubject);
                    student.StudentId = Convert.ToInt32(row["StudentID"]);
                    student.Surname = row["Surname"].ToString();
                    student.Name = row["Name"].ToString();
                    student.GuardianName = row["Guardian"].ToString();
                    student.EmailAddress = row["Email"].ToString();
                    student.NID = row["NID"].ToString();
                    student.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                    student.PhoneNumber = row["PhoneNumber"].ToString();
                }
                return student;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        public IEnumerable<Model.Student> GetStudents()
        {
            throw new System.NotImplementedException();
        }

        public Model.Student UpdateStudent(Model.Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
