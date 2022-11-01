using Interface.Repository;
using Interface;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.StudentManager
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentDAL _studentDal;

        public StudentBL()
        {
            _studentDal = new StudentDal();
        }

        public Task<Student> Create(Student student)
        {
            return _studentDal.Create(student);
        }

        public Task<bool> Delete(int studentId)
        {
            return _studentDal.Delete(studentId);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentDal.GetAll();
        }

        public Task<Student> GetById(int id)
        {
            return _studentDal.GetById(id);
        }

        public Task<Student> Update(Student student)
        {
            return _studentDal.Create(student);
        }

    }
}
