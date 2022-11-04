using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Utility
{
    public static class DataTableMapper
    {
        public static T MapTo<T>(DataRow row) where T : new()
        {
            T result = new T();
            Type type = typeof(T);

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                MethodInfo setter = property.GetSetMethod(nonPublic: false);

                if (setter == null)
                {
                    continue;
                }

                string name;
                DataColumnNameAttribute nameAttr = property.GetCustomAttribute<DataColumnNameAttribute>();

                if (nameAttr != null)
                {
                    name = nameAttr.Name;
                }
                else
                {
                    name = property.Name;
                }

                setter.Invoke(result, new object[] { row[name] });
            }

            return result;
        }

        public static IEnumerable<T> MapTo<T>(DataTable table) where T : new()
        {
            foreach (DataRow row in table.Rows)
            {
                yield return MapTo<T>(row);
            }
        }
    }
}
