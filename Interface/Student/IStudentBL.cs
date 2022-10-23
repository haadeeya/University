using Model;
using System.Collections.Generic;

namespace Interface
{
    public interface IStudentBL
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentbyId(int id);

        Student CreateStudent(Student student);

        Student UpdateStudent(Student student);

        bool DeleteStudent(int studentId);
    }
}
