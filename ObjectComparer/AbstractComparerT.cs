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

        public bool Compare(T obj1, T obj2, out IEnumerable<Difference> differences)
        {
            differences = CalculateDifferences(obj1, obj2);

            return !differences.Any();
        }

        public bool Compare(T obj1, T obj2)
        {
            return !CalculateDifferences(obj1, obj2).Any();
        }

        public abstract IEnumerable<Difference> CalculateDifferences(T obj1, T obj2);
    }
}