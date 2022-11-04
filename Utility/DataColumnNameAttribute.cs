using System;

namespace Utility
{
    public class DataColumnNameAttribute : Attribute
    {
        public string Name { get; }

        public DataColumnNameAttribute(string name)        {
            Name = name;
        }
    }
}
