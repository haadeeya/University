using Interface;
using System.Collections.Generic;

namespace Core.Student
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentDal _studentDal;
        public StudentBL()
        {
            _studentDal = new StudentDal();
        }
        public Model.Student CreateStudent(Model.Student student)
        {
            var newstudent = _studentDal.CreateStudent(student);
            return newstudent;
        }

        public bool DeleteStudent(int studentId)
        {
            var deletestudent = _studentDal.DeleteStudent(studentId);
            return deletestudent = true ? true : false;
        }

        public Model.Student GetStudentbyId(int id)
        {
            var currentStudent = _studentDal.GetStudentbyId(id);
            return currentStudent;
        }

        public IEnumerable<Model.Student> GetStudents()
        {
            var allStudents = _studentDal.GetStudents();
            return allStudents;
        }

        public Model.Student UpdateStudent(Model.Student student)
        {
            var updatestudent = _studentDal.CreateStudent(student);
            return updatestudent;
        }
    }
}
