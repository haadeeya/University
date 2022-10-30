using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudentSubject
    {
        public int StudentSubjectId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }    
        public string Grade { get; set; }

        public StudentSubject() { }

        public StudentSubject(int studentId, int subjectId,int studentSubjectId, Subject subject, string grade)
        {
            StudentId = studentId;
            SubjectId = subjectId;
            StudentSubjectId = studentSubjectId;
            Subject = subject;
            Grade = grade;
        }
    }
}
