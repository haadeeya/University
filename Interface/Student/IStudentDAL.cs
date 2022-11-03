using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IStudentDAL:IRepositoryDal<Student>
    {
        Task<Student> GetByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> CreateAsync(Student entity);
        Task<Student> UpdateAsync(Student entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateStatusAsync(List<Student> students);
    }
}
