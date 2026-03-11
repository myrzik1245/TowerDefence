namespace _Project.Code.Runtime.Utility.DataManagment.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);

        TData Deserialize<TData>(string serializedData);
    }
}
