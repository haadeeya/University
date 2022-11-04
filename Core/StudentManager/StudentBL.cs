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

        public StudentBL(IStudentDAL studentDal)
        {
            _studentDal = studentDal;
        }

        public async Task<List<Student>> ComputeMarkAndStatusAsync(List<Student> students)
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
            var studentStatuslist = await UpdateStatusAsync(students);
            if (studentStatuslist.Count > 0)
            {
                return studentStatuslist;
            }
            return null;
        }

        public Task<Student> CreateAsync(Student student) => _studentDal.CreateAsync(student);

        public Task<bool> DeleteAsync(int studentId) => _studentDal.DeleteAsync(studentId);

        public async Task<IEnumerable<Student>> GetAllAsync() => await _studentDal.GetAllAsync();

        public Task<Student> GetByIdAsync(int id) => _studentDal.GetByIdAsync(id);

        public Task<Student> UpdateAsync(Student student) => _studentDal.CreateAsync(student);

        public async Task<List<Student>> UpdateStatusAsync(List<Student> students)
        {
            for (int i = 0; i < students.Count; i++)
                students[i].Status = students[i].Marks < 10 ? Status.Rejected.ToString() : i < 15 ? Status.Approved.ToString() : Status.Waiting.ToString();

            var studentStatuslist = await _studentDal.UpdateStatusAsync(students);
            if (studentStatuslist) return students;
            return null;
        }
    }
}
