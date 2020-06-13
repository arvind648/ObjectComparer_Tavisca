﻿using System;
using System.Collections;
using System.Collections.Generic;
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

            object[] a1 = e1.Cast<Object>().ToArray();
            object[] a2 = e2.Cast<Object>().ToArray();
            if (a1 == null || a2 == null) return a1 == a2;

            List<object> a2List = new List<object>(a2);
            if (a1.Length != a2.Length)
                return false;
            for (int i = 0; i < a1.Length; i++)
            {
                var match = a2List.Find(x => Comparer.Compare(a1[i], x) == true);
                if (match == null)
                {
                    return false;
                }
                else a2List.Remove(match);
            }
            return true;
        }


    }
}