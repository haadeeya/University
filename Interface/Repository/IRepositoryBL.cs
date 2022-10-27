using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IRepositoryBL<T> where T : class
    {
        T GetbyId(int id);
        IEnumerable<T> Get();
        T Create(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
