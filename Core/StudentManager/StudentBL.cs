using Interface.Repository;
using Interface;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Core.StudentManager
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentDAL _studentDal;

        public StudentBL(IStudentDAL studentDal)
        {
            _studentDal = studentDal;
        }

        public Task<IEnumerable<Student>> ComputeMark(List<Student> students)
        {
            foreach (var student in students)
            {
                var mark = 0;
                foreach (var studentsubject in student.Subjects)
                {
                    if (Enum.IsDefined(typeof(Results), studentsubject.Grade))
                    {
                        mark += (int)Enum.Parse(typeof(Results), studentsubject.Grade);
                    }
                }
                student.Marks = mark;
            }
            return Task.FromResult(students.AsEnumerable());
        }

        public async Task<Student> CreateAsync(Student student)
        {
            var mark = 0;
            foreach (var studentsubject in student.Subjects)
            {
                if (Enum.IsDefined(typeof(Results), studentsubject.Grade))
                {
                    mark += (int)Enum.Parse(typeof(Results), studentsubject.Grade);
                }
            }
            student.Marks = mark;

            var allstudents = await GetAllAsync();
            int accepted = allstudents.Count(x => x.Status==Status.Approved.ToString());
            student.Status = student.Marks < 10 ? Status.Rejected.ToString() : accepted < 15 ? Status.Approved.ToString() : Status.Waiting.ToString();
            return await _studentDal.CreateAsync(student);
        }
        
        public Task<bool> DeleteAsync(int studentId) => _studentDal.DeleteAsync(studentId);

        public async Task<IEnumerable<Student>> GetAllAsync() => await _studentDal.GetAllAsync();

        public Task<Student> GetByIdAsync(int id) => _studentDal.GetByIdAsync(id);

        public async Task<Student> UpdateAsync(Student student) => await _studentDal.CreateAsync(student);

        
    }
}
