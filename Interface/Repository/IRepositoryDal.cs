using Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryDal<T> where T : class
    {
        Task<T> GetbyId(int id);
        Task<IEnumerable<T>> Get();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
    }
}
