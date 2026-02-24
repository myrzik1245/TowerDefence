using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.EntryPoints.SceneEntryPoints
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        public abstract IEnumerator Initialize(DIContainer container, IInputSceneArgs inputSceneArgs);
        public abstract void Run();
    }
}
