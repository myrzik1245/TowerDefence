using System;
using System.Collections;

namespace _Project.Code.Runtime.Utility.ConfigManagment.Loaders
{
    public interface IConfigLoader
    {
        IEnumerator LoadAsync(Action<Type, object> onLoad);
    }
}