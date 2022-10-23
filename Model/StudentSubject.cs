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
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public string Grade { get; set; }
    }
}
