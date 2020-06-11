using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectComparer.Utils;

namespace ObjectComparer
{
    internal abstract class AbstractDynamicObjectsComprer<T> : AbstractComparer, IComparerWithCondition
    {
        protected AbstractDynamicObjectsComprer(BaseComparer parentComparer, IComparersFactory factory) : base(parentComparer, factory)
        {
        }

        public override IEnumerable<Difference> CalculateDifferences(Type type, object obj1, object obj2)
        {
            var castedObject1 = (T)obj1;
            var castedObject2 = (T)obj2;
            var propertyKeys1 = GetProperties(castedObject1);
            var propertyKeys2 = GetProperties(castedObject2);

            var propertyKeys = propertyKeys1.Union(propertyKeys2);

            foreach (var propertyKey in propertyKeys)
            {
                var existsInObject1 = propertyKeys1.Contains(propertyKey);
                var existsInObject2 = propertyKeys2.Contains(propertyKey);
                object value1 = null;
                if (existsInObject1)
                {
                    TryGetMemberValue(castedObject1, propertyKey, out value1);
                }

                object value2 = null;
                if (existsInObject2)
                {
                    TryGetMemberValue(castedObject2, propertyKey, out value2);
                }

                var propertyType = (value1 ?? value2)?.GetType() ?? typeof(object);
                var customComparer = OverridesCollection.GetComparer(propertyType) ??
                                     OverridesCollection.GetComparer(propertyKey);
                var valueComparer = customComparer ?? DefaultValueComparer;


                if (value1 != null && value2 != null && value1.GetType() != value2.GetType())
                {
                    var valueComparer2 = OverridesCollection.GetComparer(value2.GetType()) ??
                        OverridesCollection.GetComparer(propertyKey) ??
                        DefaultValueComparer;
                    yield return new Difference(propertyKey, valueComparer.ToString(value1), valueComparer2.ToString(value2),
                        DifferenceTypes.TypeMismatch);
                    continue;
                }

                if (value1 == null && value2 != null && value2.GetType().GetTypeInfo().IsValueType ||
                    value2 == null && value1 != null && value1.GetType().GetTypeInfo().IsValueType)
                {
                    var valueComparer2 = value2 != null ?
                        OverridesCollection.GetComparer(value2.GetType()) ?? OverridesCollection.GetComparer(propertyKey) ?? DefaultValueComparer :
                        DefaultValueComparer;
                    yield return new Difference(propertyKey, valueComparer.ToString(value1), valueComparer2.ToString(value2),
                        DifferenceTypes.TypeMismatch);
                    continue;
                }

                if (customComparer != null)
                {
                    if (!customComparer.Compare(value1, value2))
                    {
                        yield return new Difference(propertyKey, customComparer.ToString(value1), customComparer.ToString(value2));
                    }

                    continue;
                }

                var comparer = Factory.GetObjectComparer(propertyType, this);
                foreach (var failure in comparer.CalculateDifferences(propertyType, value1, value2))
                {
                    yield return failure.InsertPath(propertyKey);
                }
            }
        }

        public abstract bool IsMatch(Type type, object obj1, object obj2);

        public abstract bool IsStopComparison(Type type, object obj1, object obj2);

        public abstract bool SkipMember(Type type, MemberInfo member);

        protected abstract IList<string> GetProperties(T obj);

        protected abstract bool TryGetMemberValue(T obj, string propertyName, out object value);
    }
}