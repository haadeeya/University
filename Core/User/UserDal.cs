using DataAccess;
using Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace Core.Registration
{
    public class UserDal : IUserDal
    {
        private readonly IDBCommand _dBCommand;
        public UserDal()
        {
            _dBCommand = new DBCommand();
        }

        public DataTable Authenticate(Model.Login login)
        {
            try
            {
                string query = $"SELECT * FROM [User] WHERE Username = @Username AND Password = @Password";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Username", login.Username));
                parameters.Add(new SqlParameter("@Password", login.Password));

                var result = _dBCommand.GetDataWithConditions(query, parameters);
                return result;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        public bool Create(Model.User user)
        {
            try { 
            string query = $"INSERT INTO [User](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Username", user.Username));
            parameters.Add(new SqlParameter("@Email", user.Email));
            parameters.Add(new SqlParameter("@Password", user.Password));
            parameters.Add(new SqlParameter("@Role", (int) user.Role));

            var result = _dBCommand.UpdateAndInsertData(query, parameters);
            
            return result == 1;
            }
            catch(Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                return false;
            }
        }


    }
}
