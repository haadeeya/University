using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserBL : IRepositoryBL<User>
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetById(int id);
        Task<User> Authenticate(Login login);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<bool> Delete(int userId);
    }
}
