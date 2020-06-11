namespace ObjectComparer
{
    public interface IValueComparer<in T> : IValueComparer
    {
        bool Compare(T obj1, T obj2);

        string ToString(T value);
    }
}