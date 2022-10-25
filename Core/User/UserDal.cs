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

namespace Core.Registration
{
    public class UserDal : IUserDal,IRepositoryDal<User>
    {
        private readonly Interface.IDbCommand _dbCommand;
        public UserDal()
        {
            _dbCommand = new DBCommand();
        }

        public Task<User> Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        async Task<DataTable> IUserDal.Authenticate(Login login)
        {
            try
            {
                string query = $"SELECT * FROM [User] WHERE Username = @Username AND Password = @Password";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Username", login.Username));
                parameters.Add(new SqlParameter("@Password", login.Password));

                var result = await _dbCommand.GetDataWithConditions(query, parameters);
                return result;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }

        //public DataTable Authenticate(Model.Login login)
        //{
        //    try
        //    {
        //        string query = $"SELECT * FROM [User] WHERE Username = @Username AND Password = @Password";
        //        List<SqlParameter> parameters = new List<SqlParameter>();

            //        parameters.Add(new SqlParameter("@Username", login.Username));
            //        parameters.Add(new SqlParameter("@Password", login.Password));

            //        var result = _dBCommand.GetDataWithConditions(query, parameters);
            //        return result;
            //    }
            //    catch (Exception ex)
            //    {
            //        MyLogger.GetInstance().Error($"Error {ex.Message}");
            //        throw ex;
            //    }
            //}

            //public bool Create(Model.User user)
            //{
            //    try { 
            //    string query = $"INSERT INTO [User](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
            //    List<SqlParameter> parameters = new List<SqlParameter>();

            //    parameters.Add(new SqlParameter("@Username", user.Username));
            //    parameters.Add(new SqlParameter("@Email", user.Email));
            //    parameters.Add(new SqlParameter("@Password", user.Password));
            //    parameters.Add(new SqlParameter("@Role", (int) user.Role));

            //    var result = _dBCommand.UpdateAndInsertData(query, parameters);

            //    return result == 1;
            //    }
            //    catch(Exception ex)
            //    {
            //        MyLogger.GetInstance().Error($"Error {ex.Message}");
            //        return false;
            //    }
            //}


    }
}
