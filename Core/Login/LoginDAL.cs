using Interface.Login;
using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Utility;
using System.Data;
using System.Runtime.Remoting.Messaging;
using Core.Registration;
using Interface;
using System.Data.Common;
using DataAccess;
using System.Threading.Tasks;
using System.Linq;

namespace Core.Login
{
    public class LoginDAL : ILoginDAL
    {
        private readonly IDBCommand _dBCommand;
        public LoginDAL()
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

                var result =  _dBCommand.GetDataWithConditions(query, parameters);
                return result;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error($"Error {ex.Message}");
                throw ex;
            }
        }
    }
}
