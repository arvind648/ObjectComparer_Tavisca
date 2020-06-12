using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ObjectComparer.Utils;

namespace ObjectComparer
{
    public class Comparer<T> : AbstractComparer<T>
    {
        private readonly List<MemberInfo> _members;
        private readonly List<IComparerWithCondition> _conditionalComparers;

        public Comparer(BaseComparer parentComparer = null, ComparersFactory factory = null) : base(parentComparer, factory)
        {
            var properties = GetProperties(typeof(T));
            var fields = typeof(T).GetTypeInfo().GetFields().Where(f => f.IsPublic && !f.IsStatic).ToList();

            _members = properties.Union(fields.Cast<MemberInfo>()).ToList();
            _conditionalComparers = new List<IComparerWithCondition>
            {
                new GenericEnumerablesComparer( this, Factory),
            };
        }       

        public override bool IsSimilar(T obj1, T obj2)
        {            
            if (typeof(T).IsComparable())
            {                
                return DefaultValueComparer.Compare(obj1, obj2);
            }

            var conditionalComparer = _conditionalComparers.FirstOrDefault(c => c.IsMatch(typeof(T), obj1, obj2));
            if (conditionalComparer != null)
            {
                if (!conditionalComparer.IsSimilar(typeof(T), obj1, obj2)) { return false; }
            }

            if (obj1 == null || obj2 == null)
            {
                return DefaultValueComparer.Compare(obj1, obj2);
            }

            foreach (var member in _members)
            {
                var value1 = member.GetMemberValue(obj1);
                var value2 = member.GetMemberValue(obj2);
                var type = member.GetMemberType();
              
                if (!type.IsComparable())
                {
                    var objectDataComparer = Factory.GetObjectComparer(type, this);
                    if (!objectDataComparer.IsSimilar(type, value1, value2)) { return false; }
                    continue;
                }

                if (!DefaultValueComparer.Compare(value1, value2))
                {
                    return false;                   
                }
            }
            return true;
        }

        private List<PropertyInfo> GetProperties(Type type)
        {
            var properties = type.GetTypeInfo().GetProperties().Where(p =>
                p.CanRead
                && p.GetGetMethod(true).IsPublic
                && p.GetGetMethod(true).GetParameters().Length == 0
                && !p.GetGetMethod(true).IsStatic).ToList();


            return properties;
        }
    }
}
