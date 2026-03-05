namespace _Project.Code.Runtime.Gameplay.AI
{
    public interface IBlackboard
    {
        void WriteData(string key, object value);
        bool TryGetData<TData>(string key, out TData data);
    }
}
