namespace IoCContainer
{
    public class EfService<T> : IService<T> where T : class
    {
        public virtual string Add(T item)
        {
            return "Add " + typeof(T).Name;
        }

        public virtual string Update(T item)
        {
            return "Update " + typeof(T).Name;
        }

        public virtual string Remove(T item)
        {
            return "Remove " + typeof(T).Name;
        }
    }
}