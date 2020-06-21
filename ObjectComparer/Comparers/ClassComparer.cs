using ObjectComparer.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Comparers
{
    internal class ClassComparer : IComparer
    {
        public ClassComparer() { }// : base() { }

        public  bool IsComparable(Type type)
        {
            return type.IsClass && !type.IsArray;
        }

        public bool Compare(object a, object b)
        {
            if (a.GetType() != b.GetType())
                return false;
            var type = a.GetType();
            var readableProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                     .Where(p => p.CanRead == true)
                                     .ToArray();           

            foreach (var property in readableProperties)
            {
                object valueA = property.GetValue(a);
                object valueB = property.GetValue(b);
                var areSame = Comparer.Compare(valueA, valueB);
                if (!areSame) return false;
            }
            return true;
        }


    }
}
