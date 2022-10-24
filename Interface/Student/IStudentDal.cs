using System.Collections.Generic;
using System.Threading.Tasks;
using Interface.Repository;
using Model;

namespace Interface
{
    public interface IStudentDal
    {
        IEnumerable<Student> Get();
        Student GetbyId(int id);

        Student Create(Student student);

        Student Update(Student student);

        bool Delete(int studentId);
    }
}
