using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserBL : IRepositoryBL<User>
    {
        IEnumerable<User> Get();
        User GetbyId(int id);
        User Authenticate(Login login);

        User Create(User user);

        User Update(User user);

        bool Delete(int userId);
    }
}
