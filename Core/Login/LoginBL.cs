using Interface.Login;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Data;
using Model;
using Core.Registration;

namespace Core.Login
{
    public class LoginBL : ILoginBL
    {
        private readonly ILoginDAL _loginDal;
        public LoginBL()
        {
            _loginDal = new LoginDAL();
        }
        public User Authenticate(Model.Login login)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(login.Password);
                byte[] passwordHash = hasher.ComputeHash(passwordBytes);

                login.Password = Convert.ToBase64String(passwordHash);
            }
            var user = _loginDal.Authenticate(login);
            var thisuser = new User();
            foreach (DataRow row in user.Rows)
            {
                thisuser.UserId = Convert.ToInt32(row["UserID"]);
                thisuser.Username = row["Username"].ToString();
                thisuser.Password = row["Password"].ToString();
                thisuser.Email = row["Email"].ToString();
                thisuser.Role = (Role)Convert.ToInt32(row["Role"]);
            }
            return thisuser;
        }
    }
}
