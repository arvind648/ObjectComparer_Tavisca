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
        
        public Comparer( BaseComparer parentComparer = null, ComparersFactory factory = null) : base( parentComparer, factory)
        {
        }

        public override bool IsSimilar(Type type, object obj1, object obj2)
        {
            var ObjectComparerMethod = typeof(ComparersFactory).GetTypeInfo().GetMethods().First(m => m.IsGenericMethod);
            var ObjectComparerGenericMethod = ObjectComparerMethod.MakeGenericMethod(type);
            var comparer = ObjectComparerGenericMethod.Invoke(Factory, new object[] {  this });
            var genericType = typeof(IComparer<>).MakeGenericType(type);
            var method = genericType.GetTypeInfo().GetMethod("IsSimilar", new[] { type, type });

            return (bool)method.Invoke(comparer, new[] { obj1, obj2 });
        }       
    }
}

