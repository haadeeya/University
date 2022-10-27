using Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryDal<T> where T : class
    {
        T GetbyId(int id);
        IEnumerable<T> Get();
        T Create(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
