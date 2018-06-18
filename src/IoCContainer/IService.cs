namespace IoCContainer
{
    public interface IService<T> where T : class
    {
        string Add(T item);

        string Update(T item);

        string Remove(T item);
    }
}