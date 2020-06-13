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
            else
            {                
                var comparer = Singleton<ComparerFactory>.Instance.GetComparer((a ?? b).GetType());
                return comparer.AreEqual(a, b);
            }
        }
    }
}
