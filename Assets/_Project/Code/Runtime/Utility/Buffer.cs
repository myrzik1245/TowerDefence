namespace _Project.Code.Runtime.Utility
{
    public class Buffer<T>
    {
        public T[] Items;
        public int Count;

        public Buffer(int size)
        {
            Items = new T[size];
            Count = 0;
        }
    }
}
