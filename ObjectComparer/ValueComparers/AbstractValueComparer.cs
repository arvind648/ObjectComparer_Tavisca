namespace ObjectComparer
{
    public abstract class AbstractValueComparer : IValueComparer
    {
        public abstract bool Compare(object obj1, object obj2);

        public virtual string ToString(object value)
        {
            return value?.ToString();
        }
    }
}
