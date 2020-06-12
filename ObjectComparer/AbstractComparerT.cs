using System.Collections.Generic;
using System.Linq;

namespace ObjectComparer
{
    public abstract class AbstractComparer<T> : BaseComparer, IComparer<T>
    {
        protected AbstractComparer( BaseComparer parentComparer, ComparersFactory factory)
            : base( parentComparer, factory)
        {
        }

        public bool Compare(T obj1, T obj2)
        {
            return IsSimilar(obj1, obj2);
            
        }

        //public bool Compare(T obj1, T obj2)
        //{
        //    return IsSimilar(obj1, obj2);
        //}

        public abstract bool IsSimilar(T obj1, T obj2);
    }
}