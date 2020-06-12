using System;
using System.Reflection;

namespace ObjectComparer
{
    internal interface IComparerWithCondition : IComparer
    {
        bool IsMatch(Type type, object obj1, object obj2);
      
        //bool SkipMember(Type type, MemberInfo member);
    }
}
