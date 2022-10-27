using System.Collections.Generic;

namespace Model
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Subject(int id, string name)
        {
            SubjectId = id;
            SubjectName = name;
        }
    }
}
