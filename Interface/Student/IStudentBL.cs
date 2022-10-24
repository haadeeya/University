using Interface.Repository;
using Model;
using System.Collections.Generic;

namespace Interface
{
    public interface IStudentBL
    {
        IEnumerable<Student> Get();
        Student GetbyId(int id);

        Student Create(Student student);

        Student Update(Student student);

        bool Delete(int studentId);
    }
}
