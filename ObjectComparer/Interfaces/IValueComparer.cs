namespace ObjectComparer
{
    public interface IValueComparer
    {
        bool Compare(object obj1, object obj2);

        string ToString(object value);
    }
}
