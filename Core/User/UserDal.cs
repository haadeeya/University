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
using IDbCommand = Interface.IDbCommand;

namespace Core.Registration
{
    public class UserDal : IUserDal, IRepositoryDal<User>
    {
        private readonly IDbCommand _dbCommand;
        public UserDal()
        {
            _dbCommand = new DBCommand();
        }

        public async Task<User> Create(User newuser)
        {
            try
            {
                string query = $"INSERT INTO [User](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Username", newuser.Username));
                parameters.Add(new SqlParameter("@Email", newuser.Email));
                parameters.Add(new SqlParameter("@Password", newuser.Password));
                parameters.Add(new SqlParameter("@Role", (int)newuser.Role));

                var result =  await _dbCommand.UpdateAndInsertData(query, parameters);

                return result > 0 ? newuser : null;
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<bool> Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        async Task<DataTable> IUserDal.Get(Login login)
        {
            try
            {
                string query = @"SELECT [UserId], [Username], [Email], [Password], [Role] 
                                FROM [User] WHERE Username = @Username AND Password = @Password";
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Username", login.Username));
                parameters.Add(new SqlParameter("@Password", login.Password));

                var result = await _dbCommand.GetDataWithConditions(query, parameters);
                return result;
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }


    }
}
