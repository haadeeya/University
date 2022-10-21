﻿using DataAccess;
using Interface;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Core.Registration
{
    public class RegistrationDal : IRegistrationDal
    {
        private readonly IDBCommand _dBCommand;
        public RegistrationDal()
        {
            _dBCommand = new DBCommand();
        }

        public bool RegisterUser(Model.Registration registration)
        {
            string query = $"INSERT INTO [User](Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Username", registration.Username));
            parameters.Add(new SqlParameter("@Email", registration.Email));
            parameters.Add(new SqlParameter("@Password", registration.Password));
            parameters.Add(new SqlParameter("@Role", (int) registration.Role));

            var result = _dBCommand.UpdateAndInsertData(query, parameters);
            return result == 1;
        }


    }
}
