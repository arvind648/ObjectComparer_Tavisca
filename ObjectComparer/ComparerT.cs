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

        public Comparer( BaseComparer parentComparer = null, IComparersFactory factory = null)
        : base( parentComparer, factory)
        {
            var properties = GetProperties(typeof(T), new List<Type>());
            var fields = typeof(T).GetTypeInfo().GetFields().Where(f =>
                f.IsPublic && !f.IsStatic).ToList();
            _members = properties.Union(fields.Cast<MemberInfo>()).ToList();
            _conditionalComparers = new List<IComparerWithCondition>
            {               
                new GenericEnumerablesComparer( this, Factory),             
            };            
        }

        public override IEnumerable<Difference> CalculateDifferences(T obj1, T obj2)
        {
            return CalculateDifferences(obj1, obj2, null);
        }

        internal IEnumerable<Difference> CalculateDifferences(T obj1, T obj2, MemberInfo memberInfo)
        {
            var comparer = memberInfo != null
                ? OverridesCollection.GetComparer(memberInfo)
                : OverridesCollection.GetComparer(typeof(T));

            if (typeof(T).IsComparable() ||
                comparer != null)
            {
                comparer = comparer ?? DefaultValueComparer;
                if (!comparer.Compare(obj1, obj2))
                {
                    yield return
                        new Difference(string.Empty, comparer.ToString(obj1),
                            comparer.ToString(obj2));
                }

                yield break;
            }

            var conditionalComparer = _conditionalComparers.FirstOrDefault(c => c.IsMatch(typeof(T), obj1, obj2));
            if (conditionalComparer != null)
            {
                foreach (var difference in conditionalComparer.CalculateDifferences(typeof(T), obj1, obj2))
                {
                    yield return difference;
                }             
            }

            if (obj1 == null || obj2 == null)
            {
                if (!DefaultValueComparer.Compare(obj1, obj2))
                {
                    yield return new Difference(string.Empty, DefaultValueComparer.ToString(obj1), DefaultValueComparer.ToString(obj2));
                }

                yield break;
            }         

            foreach (var member in _members)
            {
                var value1 = member.GetMemberValue(obj1);
                var value2 = member.GetMemberValue(obj2);
                var type = member.GetMemberType();

                if (conditionalComparer != null && conditionalComparer.SkipMember(typeof(T), member))
                {
                    continue;
                }

                var valueComparer = DefaultValueComparer;
                var hasCustomComparer = false;

                var comparerOverride = OverridesCollection.GetComparer(member);
                if (comparerOverride != null)
                {
                    valueComparer = comparerOverride;
                    hasCustomComparer = true;
                }

                if (!hasCustomComparer
                    && !type.IsComparable())
                {
                    var objectDataComparer = Factory.GetObjectComparer(type, this);

                    foreach (var failure in objectDataComparer.CalculateDifferences(type, value1, value2))
                    {
                        yield return failure.InsertPath(member.Name);
                    }

                    continue;
                }

                if (!valueComparer.Compare(value1, value2))
                {
                    yield return new Difference(member.Name, valueComparer.ToString(value1), valueComparer.ToString(value2));
                }
            }
        }

        private List<PropertyInfo> GetProperties(Type type, List<Type> processedTypes)
        {
            var properties = type.GetTypeInfo().GetProperties().Where(p =>
                p.CanRead
                && p.GetGetMethod(true).IsPublic
                && p.GetGetMethod(true).GetParameters().Length == 0
                && !p.GetGetMethod(true).IsStatic).ToList();
            processedTypes.Add(type);

            if (type.GetTypeInfo().IsInterface)
            {
                foreach (var parrentInterface in type.GetTypeInfo().GetInterfaces())
                {
                    if (processedTypes.Contains(parrentInterface))
                    {
                        continue;
                    }

                    properties = properties
                        .Union(GetProperties(parrentInterface, processedTypes))
                        .Distinct()
                        .ToList();
                }
            }

            return properties;
        }
    }
}
