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

            var userDt = await _userDal.Get(login);

            if (userDt.Rows.Count > 0)
            {
                var row = userDt.Rows[0];

                return new User()
                {
                    Id = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Email = row["Email"].ToString(),
                    Role = (Role)Convert.ToInt32(row["Role"])
                };
            }

            return null;
        }

        public async Task<User> Create(User user)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                byte[] passwordHash = hasher.ComputeHash(passwordBytes);

                user.Password = Convert.ToBase64String(passwordHash);
            }
            var newuser = await _userDal.Create(user);
            return newuser;
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
    }
}
