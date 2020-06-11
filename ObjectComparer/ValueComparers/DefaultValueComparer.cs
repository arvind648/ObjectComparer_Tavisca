namespace ObjectComparer
{
    public class DefaultValueComparer : IValueComparer
    {
        private static volatile IValueComparer _instance;
        public static IValueComparer Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DefaultValueComparer();
                    }
                }

                return _instance;
            }
        }

        private static readonly object SyncRoot = new object();

        public bool Compare(object obj1, object obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return obj1 == obj2;
            }

            return obj1.Equals(obj2);
        }

        public string ToString(object value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}
