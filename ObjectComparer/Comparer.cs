using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectComparer.Utils;

namespace ObjectComparer
{
    public class Comparer : AbstractComparer
    {
        public static bool AreSimilar<T>(T first, T second)
        {
            return true;
        }
        private static string CalculateDifferencesMethodName
        {
            get { return ExtensionMethods.GetMethodName<Comparer<object>>(x => x.CalculateDifferences(null, null)); }
        }

        public Comparer( BaseComparer parentComparer = null, IComparersFactory factory = null) : base( parentComparer, factory)
        {
        }

        public override IEnumerable<Difference> CalculateDifferences(Type type, object obj1, object obj2)
        {
            var ObjectComparerMethod = typeof(IComparersFactory).GetTypeInfo().GetMethods().First(m => m.IsGenericMethod);
            var ObjectComparerGenericMethod = ObjectComparerMethod.MakeGenericMethod(type);
            var comparer = ObjectComparerGenericMethod.Invoke(Factory, new object[] {  this });
            var genericType = typeof(IComparer<>).MakeGenericType(type);
            var method = genericType.GetTypeInfo().GetMethod(CalculateDifferencesMethodName, new[] { type, type });

            return (IEnumerable<Difference>)method.Invoke(comparer, new[] { obj1, obj2 });
        }
    }
}

