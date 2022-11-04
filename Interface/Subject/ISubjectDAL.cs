using Interface.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ISubjectDAL : IRepositoryDAL<Subject>
    {
        Task<Subject> GetByIdAsync(int id);
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject> CreateAsync(Subject entity);
        Task<Subject> UpdateAsync(Subject entity);
        Task<bool> DeleteAsync(int id);
    }
}
