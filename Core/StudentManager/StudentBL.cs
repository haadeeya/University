using Interface.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.StudentManager
{
    public class StudentBL : IRepositoryBL<Student>
    {
        private readonly IRepositoryDal<Student> _studentDal;
        public StudentBL()
        {
            _studentDal = new StudentDal();
        }

        public async Task<Student> Create(Student student)
        {
            var newstudent = await _studentDal.Create(student);
            return newstudent != null ? newstudent : null;
        }

        public async Task<bool> Delete(int studentId)
        {
            var isDeleted = await _studentDal.Delete(studentId);
            return isDeleted = true ? true : false;
        }

        public Task<IEnumerable<Student>> Get()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var allStudents = await _studentDal.Get();
            return allStudents;
        }

        public async Task<Student> GetbyId(int id)
        {
            var currentStudent = await _studentDal.GetbyId(id);
            return currentStudent;
        }

        public async Task<Student> Update(Student student)
        {
            var updatestudent = await _studentDal.Create(student);
            return updatestudent;
        }

    }
}
