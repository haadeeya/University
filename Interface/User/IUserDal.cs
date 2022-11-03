using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserDal : IRepositoryDal<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsername(string username);
        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<bool> DeleteAsync(int userId);
        Task<DataTable> Get(Login login);
    }
}
