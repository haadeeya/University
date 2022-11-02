using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserDal : IRepositoryDal<User>
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<User> Create(User user);

        Task<User> Update(User user);

        Task<bool> Delete(int userId);
        Task<DataTable> Get(Login login);
    }
}
