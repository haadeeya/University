using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Interface
{
    public interface IStudentDal
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentbyId(int id);

        Student CreateStudent(Student student);

        Student UpdateStudent(Student student);

        bool DeleteStudent(int studentId);
    }
}
