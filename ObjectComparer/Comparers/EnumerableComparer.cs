using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Comparers
{
    internal class EnumerableComparer : BaseComparer
    {
        public EnumerableComparer() : base() { }

        public override bool IsComparable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>))
                return true;
            if (type.IsArray)
                return true;
            if (typeof(IEnumerable).IsAssignableFrom(type))
                return true;
            return false;
        }

        protected override bool Compare(object a, object b)
        {
            IEnumerable e1 = a as IEnumerable;
            IEnumerable e2 = b as IEnumerable;
            if (e1 == null || e2 == null) return e1 == e2;

            List<object> a1 = e1.Cast<object>().ToList();
            List<object> a2 = e2.Cast<Object>().ToList();

            if (a1 == null || a2 == null) return a1 == a2;
            if (a1.Count != a2.Count) return false;

            a1.Sort();
            a2.Sort();

            for (int i = 0; i < a1.Count; i++)
            {
                var match = Comparer.Compare(a1[i], a2[i]);
                if (!match) return false;
            }
            return true;
        }


    }
}
