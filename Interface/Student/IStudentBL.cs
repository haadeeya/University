using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IStudentBL: IRepositoryBL<Student>
    {
        Task<Student> GetByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<List<Student>> ComputeMarkAndStatusAsync(List<Student> students);
        Task<Student> CreateAsync(Student entity);
        Task<Student> UpdateAsync(Student entity);
        Task<bool> DeleteAsync(int id);
        Task<List<Student>> UpdateStatusAsync(List<Student> students);
    }
}
