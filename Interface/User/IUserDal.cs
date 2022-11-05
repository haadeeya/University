using Interface.Repository;
using Model;
using Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserDAL : IRepositoryDAL<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsername(string username);
        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<bool> DeleteAsync(int userId);
        Task<User> GetCredentialsAsync(Login login);
    }
}
