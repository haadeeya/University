using Interface.Repository;
using Interface;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Core.StudentManager
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentDAL _studentDal;

        public StudentBL()
        {
            _studentDal = new StudentDal();
        }

        public Task<List<Student>> ComputeMark(List<Student> students)
        {
            foreach (var student in students)
            {
                var mark = 0;
                foreach (var studentsubject in student.Subjects)
                {
                    if(Enum.IsDefined(typeof(Results), studentsubject.Grade))
                    {
                        mark += (int)Enum.Parse(typeof(Results), studentsubject.Grade);
                    }
                }
                student.Marks = mark;
            }
            return Task.FromResult(students);
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
