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

        public async Task<List<Student>> ComputeMarkAndStatus(List<Student> students)
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
            var studentStatuslist = await UpdateStatus(students);
            if(studentStatuslist.Count > 0)
            {
                return studentStatuslist;
            }
            return null;
        }

        public Task<Student> Create(Student student)=>_studentDal.Create(student);
        

        public Task<bool> Delete(int studentId)=>_studentDal.Delete(studentId);
        

        public async Task<IEnumerable<Student>> GetAll()=> await _studentDal.GetAll();
        

        public Task<Student> GetById(int id) => _studentDal.GetById(id);
        

        public Task<Student> Update(Student student) => _studentDal.Create(student);

        public async Task<List<Student>> UpdateStatus(List<Student> students)
        {
            for(int i=0;i<students.Count;i++)             
                students[i].Status = students[i].Marks < 10 ? Status.Rejected.ToString() : i < 15 ? Status.Approved.ToString() : Status.Waiting.ToString();

           var studentStatuslist =  await _studentDal.UpdateStatus(students);
            if(studentStatuslist) return students;
            return null;
        }
    }
}
