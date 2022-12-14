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
using Utility;

namespace Core.Registration
{
    public class UserDAL : IUserDAL, IRepositoryDAL<User>
    {
        private readonly SqlConnection _conn;

        public UserDAL(SqlConnection conn)
        {
            _conn = conn;
        }

        public async Task<User> CreateAsync(User user)
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);

            try
            {
                string query = $"INSERT INTO [User](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
                List<SqlParameter> parameters = new List<SqlParameter>();

                foreach (var property in user.GetType().GetProperties())
                {
                    if (property.Name.Equals("UserId")) continue;
                    parameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(user, null)));
                }

                int rows = await helper.UpdateAndInsertData(query, parameters);

                return rows > 0 ? user : null;
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<bool> DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsername(string username)
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);

            try
            {
                string query = @"SELECT [UserId] , [Username], [Password], [Email], [Role]
                                FROM [User] WHERE Username = @Username";
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Username", username)
                };

                DataTable dataTable = await helper.GetData(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                return DataTableMapper.MapTo<User>(dataTable.Rows[0]);
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetCredentialsAsync(Login login)
        {
            ConnectionHelper helper = new ConnectionHelper(_conn);

            try
            {
                string query = @"SELECT [UserId], [Username], [Email], [Password], [Role] 
                                FROM [User] WHERE Username = @Username AND Password = @Password";
                List<SqlParameter> parameters = new List<SqlParameter>();

                foreach (var property in login.GetType().GetProperties())
                {
                    parameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(login, null)));
                }

                DataTable dataTable = await helper.GetData(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                return DataTableMapper.MapTo<User>(dataTable.Rows[0]);
            }
            catch (Exception exception)
            {
                MyLogger.GetInstance().Error($"Error {exception.Message}");
                throw;
            }
        }
    }
}
