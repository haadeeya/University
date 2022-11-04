using Interface;
using System.Security.Cryptography;
using System.Text;
using System;
using Model;
using Interface.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Registration
{
    public class UserBL : IUserBL, IRepositoryDAL<User>
    {
        private readonly IUserDAL _userDal;

        public UserBL(IUserDAL userDal)
        {
            _userDal = userDal;
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
                    Id = Convert.ToInt32(row["UserId"]),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Email = row["Email"].ToString(),
                    Role = (Role)Convert.ToInt32(row["Role"])
                };
            }

            return null;
        }

        public async Task<User> CreateAsync(User user)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                byte[] passwordHash = hasher.ComputeHash(passwordBytes);

                user.Password = Convert.ToBase64String(passwordHash);
            }
            var newuser = await _userDal.CreateAsync(user);
            return newuser;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsername(string username) => await _userDal.GetByUsername(username);


        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
