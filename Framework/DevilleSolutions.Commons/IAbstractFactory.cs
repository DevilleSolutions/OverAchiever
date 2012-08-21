namespace DevilleSolutions.Commons
{
    public interface IAbstractFactory<T>
        where T : class
    {
        T Create();
        void Dispose(T instance);
    }
}