using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectComparer
{
    public abstract class AbstractComparer : BaseComparer, IComparer
    {
        protected AbstractComparer( BaseComparer parentComparer, ComparersFactory factory)
            : base( parentComparer, factory)
        {
        }

        public abstract bool IsSimilar(Type type, object obj1, object obj2);

        public bool IsSimilar<T>(T obj1, T obj2)
        {
            return IsSimilar(typeof(T), obj1, obj2);
        }

        //public bool Compare(Type type, object obj1, object obj2, out IEnumerable<Difference> differences)
        //{
        //    differences = CalculateDifferences(type, obj1, obj2);

        //    return !differences.Any();
        //}

        public bool Compare(Type type, object obj1, object obj2)
        {
            return IsSimilar(type, obj1, obj2);
        }

        //public bool Compare<T>(T obj1, T obj2, out IEnumerable<Difference> differences)
        //{
        //    return Compare(typeof(T), obj1, obj2, out differences);
        //}

        public bool Compare<T>(T obj1, T obj2)
        {
            return Compare(typeof(T), obj1, obj2);
        }
    }
}