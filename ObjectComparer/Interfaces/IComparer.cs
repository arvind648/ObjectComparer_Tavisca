using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectComparer
{
    public interface IComparer 
    {      
        bool Compare(Type type, object obj1, object obj2, out IEnumerable<Difference> differences);

        bool Compare<T>(T obj1, T obj2, out IEnumerable<Difference> differences);

        bool Compare(Type type, object obj1, object obj2);

        bool Compare<T>(T obj1, T obj2);

        IEnumerable<Difference> CalculateDifferences(Type type, object obj1, object obj2);

        IEnumerable<Difference> CalculateDifferences<T>(T obj1, T obj2);
    }
}