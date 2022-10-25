using System.Collections.Generic;

namespace Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public Subject(int id, string name)
        {
            Id = id;
            SubjectName = name;
        }
    }
}
