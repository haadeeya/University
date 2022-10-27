using Interface.Repository;
using Model;
using System.Collections.Generic;

namespace Core.StudentManager
{
    public class StudentBL : IRepositoryBL<Student>
    {
        private readonly IRepositoryDal<Student> _studentDal;
        public StudentBL()
        {
            _studentDal = new StudentDal();
        }

        public Student Create(Student student)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int studentId)
        {
            var deletestudent =  _studentDal.Delete(studentId);
            return deletestudent = true ? true : false;
        }

        public IEnumerable<Student> Get()
        {
            var allStudents = _studentDal.Get();
            return allStudents;
        }

        public Student GetbyId(int id)
        {
            var currentStudent = _studentDal.GetbyId(id);
            return currentStudent;
        }

        public Student Update(Student student)
        {
            var updatestudent = _studentDal.Create(student);
            return updatestudent;
        }
        //public Model.Student Create(Model.Student student)
        //{
        //    var newstudent = _studentDal.Create(student);
        //    return newstudent;
        //}

        //public bool Delete(int studentId)
        //{
        //    var deletestudent = _studentDal.Delete(studentId);
        //    return deletestudent = true ? true : false;
        //}

        //public Model.Student GetbyId(int id)
        //{
        //    var currentStudent = _studentDal.GetbyId(id);
        //    return currentStudent;
        //}

        //public IEnumerable<Model.Student> Get()
        //{
        //    var allStudents = _studentDal.Get();
        //    return allStudents;
        //}

        //public Model.Student Update(Model.Student student)
        //{
        //    var updatestudent = _studentDal.Create(student);
        //    return updatestudent;
        //}

    }
}
