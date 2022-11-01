using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ISubjectDAL : IRepositoryDal<Subject>
    {
        Task<Subject> GetById(int id);
        Task<IEnumerable<Subject>> Get();
        Task<Subject> Create(Subject entity);
        Task<Subject> Update(Subject entity);
        Task<bool> Delete(int id);
    }
}
