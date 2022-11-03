using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryBL<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
