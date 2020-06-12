using System.Collections.Generic;

namespace ObjectComparer
{
    public interface IComparer<in T>
    {

        bool Compare(T obj1, T obj2);

        bool IsSimilar(T obj1, T obj2);
    }
}