using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserDal : IRepositoryDal<User>
    {
        IEnumerable<User> Get();
        User GetbyId(int id);
        DataTable Get(Login login);

        User Create(User user);

        User Update(User user);

        bool Delete(int userId);
    }
}
