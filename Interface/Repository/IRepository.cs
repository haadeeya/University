using Model;
using System.Collections;
using System.Collections.Generic;

namespace Interface.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetbyId(int id);
        IEnumerable<T> GetAll();
        T Create(T entity);
        T Update(T entity);
        bool Delete(T entity);
    }
}
