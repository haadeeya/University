using Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryDal<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
    }
}
