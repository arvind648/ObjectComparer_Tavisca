using System;
using System.Collections.Generic;

namespace ObjectComparer.Comparers
{
    public static class Comparer
    {
        public static bool AreSimilar<T>(T a, T b)
        {
            if (a == null || b == null) { return EqualityComparer<T>.Default.Equals(a, b); }
            if (a.GetType() != b.GetType()) return false;

            return Compare(a, b);
        }
        internal static bool Compare(object a, object b)
        {
            if (a == null || b == null)
                return a == b;
            else if (a == b || a.Equals(b)) return true;
            else
            {
                try
                {
                    IComparer comparer = Singleton<ComparerFactory>.Instance.GetComparer(a.GetType());
                    return comparer.Compare(a, b);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return false;
                }
            }
        }
    }
}
