namespace _Project.Code.Runtime.Utility.DataManagment.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}
