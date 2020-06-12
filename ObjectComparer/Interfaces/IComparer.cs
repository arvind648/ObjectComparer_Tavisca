using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectComparer
{
    public interface IComparer 
    {      

        bool Compare(Type type, object obj1, object obj2);

        bool Compare<T>(T obj1, T obj2);

        bool IsSimilar(Type type, object obj1, object obj2);

        bool IsSimilar<T>(T obj1, T obj2);
    }
}