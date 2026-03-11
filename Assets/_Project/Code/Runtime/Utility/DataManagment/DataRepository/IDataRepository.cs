using System;
using System.Collections;

namespace _Project.Code.Runtime.Utility.DataManagment.DataRepository
{
    public interface IDataRepository
    {
        IEnumerator Read(string key, Action<string> onRead);
        IEnumerator Write(string key, string serializedData);
        IEnumerator Remove(string key);
        IEnumerator Exists(string key, Action<bool> onExistsResult);
    }
}
