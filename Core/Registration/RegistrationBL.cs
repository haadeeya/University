using Interface;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Core.Registration
{
    public class RegistrationBL : IRegistrationBL
    {
        private readonly IRegistrationDal _registrationDal;
        public RegistrationBL()
        {
            _registrationDal = new RegistrationDal();
        }

        public bool RegisterUser(Model.Registration registration)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(registration.Password);
                byte[] passwordHash = hasher.ComputeHash(passwordBytes);

                 registration.Password = Convert.ToBase64String(passwordHash);
            }
                var result = _registrationDal.RegisterUser(registration);
            return result;
        }
    }
}
