using System;

namespace ObjectComparer
{
    public class ComparersFactory : IComparersFactory
    {
        public virtual IComparer<T> GetObjectComparer<T>( BaseComparer parentComparer = null)
        {
            return new Comparer<T>( parentComparer, this);
        }

        public IComparer GetObjectComparer(Type type, BaseComparer parentComparer = null)
        {
            return new Comparer( parentComparer, this);
        }
    }
}
