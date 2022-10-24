using Interface;
using Interface.Repository;
using System.Collections.Generic;

namespace Core.Student
{
    public class StudentBL/*<T> : IRepository<T> where T: Model.Student*/ : IStudentDal
    {
        private readonly IStudentDal _studentDal;
        public StudentBL()
        {
            _studentDal = new StudentDal();
        }
        public Model.Student Create(Model.Student student)
        {
            var newstudent = _studentDal.Create(student);
            return newstudent;
        }

        public bool Delete(int studentId)
        {
            var deletestudent = _studentDal.Delete(studentId);
            return deletestudent = true ? true : false;
        }

        public Model.Student GetbyId(int id)
        {
            var currentStudent = _studentDal.GetbyId(id);
            return currentStudent;
        }

        public IEnumerable<Model.Student> Get()
        {
            var allStudents = _studentDal.Get();
            return allStudents;
        }

        public Model.Student Update(Model.Student student)
        {
            var updatestudent = _studentDal.Create(student);
            return updatestudent;
        }

        //T IRepository<T>.GetbyId(int id)
        //{
        //    var currentStudent = _studentDal.GetbyId(id);
        //    return (T)currentStudent;
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    var allStudents = _studentDal.Get();
        //    return (IEnumerable<T>)allStudents;
        //}

        //bool IRepository<T>.Delete(T entity)
        //{
        //    var deletestudent = _studentDal.Delete(entity.Id);
        //    return deletestudent = true ? true : false;
        //}

        //public T Create(T entity)
        //{
        //    var newstudent = _studentDal.Create(entity);
        //    return (T)newstudent;
        //}

        //public T Update(T entity)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
