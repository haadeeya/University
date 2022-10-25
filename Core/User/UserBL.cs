using Interface;
using System.Security.Cryptography;
using System.Text;
using System;
using Model;
using System.Data;
using Interface.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Registration
{
    public class UserBL : IUserBL, IRepositoryDal<User>
    {
        private readonly IUserDal _userDal;
        public UserBL()
        {
            _userDal = new UserDal();
        }

        public async Task<User> Authenticate(Login login)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(login.Password);
                byte[] passwordHash = hasher.ComputeHash(passwordBytes);

                login.Password = Convert.ToBase64String(passwordHash);
            }
            var user = await _userDal.Authenticate(login);
            var thisuser = new User();
            foreach (DataRow row in user.Rows)
            {
                thisuser.Id = Convert.ToInt32(row["UserID"]);
                thisuser.Username = row["Username"].ToString();
                thisuser.Password = row["Password"].ToString();
                thisuser.Email = row["Email"].ToString();
                thisuser.Role = (Role)Convert.ToInt32(row["Role"]);
            }
            return thisuser;
        }

        public Task<User> Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
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

        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }

        //public User Authenticate(Model.Login login)
        //{
        //    using (SHA256 hasher = SHA256.Create())
        //    {
        //        byte[] passwordBytes = Encoding.UTF8.GetBytes(login.Password);
        //        byte[] passwordHash = hasher.ComputeHash(passwordBytes);

        //        login.Password = Convert.ToBase64String(passwordHash);
        //    }
        //    var user = _userDal.Authenticate(login);
        //    var thisuser = new User();
        //    foreach (DataRow row in user.Rows)
        //    {
        //        thisuser.Id = Convert.ToInt32(row["UserID"]);
        //        thisuser.Username = row["Username"].ToString();
        //        thisuser.Password = row["Password"].ToString();
        //        thisuser.Email = row["Email"].ToString();
        //        thisuser.Role = (Role)Convert.ToInt32(row["Role"]);
        //    }
        //    return thisuser;
        //}

        //public bool Create(Model.User user)
        //{
        //    using (SHA256 hasher = SHA256.Create())
        //    {
        //        byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
        //        byte[] passwordHash = hasher.ComputeHash(passwordBytes);

        //         user.Password = Convert.ToBase64String(passwordHash);
        //    }
        //        var result = _userDal.Create(user);
        //    return result;
        //}
    }
}
