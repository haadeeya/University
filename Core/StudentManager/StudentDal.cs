﻿using DataAccess;
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
        public async Task<Student> Create(Student entity)
        {
            try
            {
                string query = @"INSERT INTO [Student](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role);
                                 INSERT INTO [StudentSubject](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Email", entity.EmailAddress));


                var result = await _dbCommand.UpdateAndInsertData(query, parameters);

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

        public Task<IEnumerable<Student>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetbyId(int id)
        {
            try
            {
                string query = @"SELECT * FROM [Student] s
                                INNER JOIN StudentSubject ss on ss.StudentID = s.StudentID
                                INNER JOIN Subject sb on sb.ID = ss.SubjectID
                                WHERE s.StudentID = @StudentId";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@StudentId", id));

                Model.Student student = new Model.Student();
                List<StudentSubject> allsubjects = new List<StudentSubject>();

                var dt = await _dbCommand.GetDataWithConditions(query, parameters);

                if (dt.Rows.Count > 0) { 
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

        public Task<Student> Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
