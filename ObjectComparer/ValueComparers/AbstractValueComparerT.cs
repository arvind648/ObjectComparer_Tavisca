namespace ObjectComparer
{
    public abstract class AbstractValueComparer<T> : AbstractValueComparer, IValueComparer<T>
    {
        public abstract bool Compare(T obj1, T obj2);

        public virtual string ToString(T value)
        {
            return base.ToString(value);
        }

        public override bool Compare(object obj1, object obj2)
        {
            return Compare((T)obj1, (T)obj2);
        }
    }
}
