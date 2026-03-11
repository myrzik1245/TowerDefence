namespace _Project.Code.Runtime.Utility.DataManagment.KeysStorage
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}
