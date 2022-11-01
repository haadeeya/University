using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IStudentBL: IRepositoryBL<Student>
    {
        Task<Student> GetById(int id);
        Task<IEnumerable<Student>> GetAll();
        Task<Student> Create(Student entity);
        Task<Student> Update(Student entity);
        Task<bool> Delete(int id);
    }
}
