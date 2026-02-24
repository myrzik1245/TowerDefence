using UnityEngine;

namespace _Project.Code.Runtime.Utility.AssetsManagment
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}