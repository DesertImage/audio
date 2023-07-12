namespace DesertImage.Audio
{
    internal interface IPool<T> where T : IPoolable
    {
        void Register(int count);
        T GetInstance();
        void ReturnInstance(T instance);
    }
}