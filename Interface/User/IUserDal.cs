using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserDal : IRepositoryDal<User>
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetbyId(int id);
        Task<DataTable> Get(Login login);

        Task<User> Create(User user);

        Task<User> Update(User user);

        Task<bool> Delete(int userId);
    }
}
