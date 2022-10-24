using System.Collections.Generic;

namespace Model
{
    public class Subject
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public Subject(int id, string name)
        {
            ID = id;
            SubjectName = name;
        }
    }
}
