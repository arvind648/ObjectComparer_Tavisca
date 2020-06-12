using System;
using System.Collections.Generic;
using System.Reflection;
using ObjectComparer.Utils;

namespace ObjectComparer
{
    internal abstract class AbstractEnumerablesComparer : AbstractComparer, IComparerWithCondition
    {
        protected AbstractEnumerablesComparer( BaseComparer parentComparer,
            ComparersFactory factory)
            : base( parentComparer, factory)
        {
        }  

        public virtual bool SkipMember(Type type, MemberInfo member)
        {
            if (type.InheritsFrom(typeof(Array)))
            {
                if (member.Name == "LongLength")
                {
                    return true;
                }
            }

            if (type.InheritsFrom(typeof(List<>)))
            {
                if (member.Name == ExtensionMethods.GetMemberInfo(() => new List<string>().Capacity).Name)
                {
                    return true;
                }
            }

            return false;
        }

        public abstract override bool IsSimilar(Type type, object obj1, object obj2);

        public abstract bool IsMatch(Type type, object obj1, object obj2);
    }
}