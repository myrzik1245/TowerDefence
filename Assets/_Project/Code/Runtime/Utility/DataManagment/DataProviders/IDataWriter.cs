namespace _Project.Code.Runtime.Utility.DataManagment.DataProviders
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}
