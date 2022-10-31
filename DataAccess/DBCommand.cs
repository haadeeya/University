using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Interface;
using System.Threading.Tasks;
using IDbCommand = Interface.IDbCommand;
using Model;
using System;
using System.Security.Cryptography;
using University.Utility;
using System.Collections;

namespace DataAccess
{
    public class DBCommand : IDbCommand
    {
        private readonly IDBConnection _dbConnection;

        public IDbConnection Connection => _dbConnection.Connection;

        public DBCommand()
        {
            _dbConnection = new DBConnection();
        }

        public async Task<DataTable> GetData(string query)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
            await _dbConnection.CloseConnection();
            return dt;
        }

        public async Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, IDbTransaction transaction = null)
        {
            int numberOfRowsAffected = 0;
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                if (_dbConnection.Connection.State == ConnectionState.Closed)
                    await _dbConnection.OpenConnection();
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = (SqlTransaction)transaction;
                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                
                numberOfRowsAffected = cmd.ExecuteNonQuery();
            }
            await _dbConnection.CloseConnection();
            return numberOfRowsAffected;
        }

        public async Task<DataTable> GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
            await _dbConnection.CloseConnection();
            return dt;
        }
    }
}

//string insertStudentQuery = @"INSERT INTO [Student]
//                                 VALUES(@StudentId, @UserId, @Name, @Surname, @NID, @GuardianName, @EmailAddress, @DateOfBirth, @PhoneNumber);";
//string insertSubjectQuery = @"INSERT INTO [StudentSubject] VALUES(@StudentId, @SubjectId, @Grade);";


//SqlCommand command = new SqlCommand();
//command.CommandText = insertStudentQuery;
//command.Connection = _dbConnection.Connection;

//var transaction = _dbConnection.Connection.BeginTransaction();
//command.Transaction = transaction;

//command.Parameters.Add(new SqlParameter("@StudentId", student.Id));
//command.Parameters.Add(new SqlParameter("@UserId", student.UserId));
//command.Parameters.Add(new SqlParameter("@NID", student.NID));
//command.Parameters.Add(new SqlParameter("@Name", student.Name));
//command.Parameters.Add(new SqlParameter("@Surname", student.Surname));
//command.Parameters.Add(new SqlParameter("@GuardianName", student.GuardianName));
//command.Parameters.Add(new SqlParameter("@EmailAddress", student.EmailAddress));
//command.Parameters.Add(new SqlParameter("@DateOfBirth", student.DateOfBirth));
//command.Parameters.Add(new SqlParameter("@PhoneNumber", student.PhoneNumber));
//command.ExecuteNonQuery();

//foreach (var subject in student.Subjects)
//{
//    using (SqlCommand command2 = new SqlCommand())
//    {
//        command2.Connection = _dbConnection.Connection;
//        command2.CommandText = insertSubjectQuery;
//        command2.Transaction = transaction;
//        command2.Parameters.Add((new SqlParameter("@StudentId", subject.StudentId)));
//        command2.Parameters.Add((new SqlParameter("@SubjectId", subject.SubjectId)));
//        command2.Parameters.Add((new SqlParameter("@Grade", subject.Grade)));
//        command2.ExecuteNonQuery();
//    }
//}
//try
//{
//    transaction.Commit();
//    return true;
//}
//catch (Exception exception)
//{
//    transaction.Rollback();
//    MyLogger.GetInstance().Error($"Error {exception.Message}");
//    throw;
//}
