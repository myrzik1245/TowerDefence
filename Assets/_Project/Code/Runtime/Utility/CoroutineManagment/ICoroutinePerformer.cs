using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Utility.CoroutineManagment
{
    public interface ICoroutinePerformer
    {
        Coroutine StartPerform(IEnumerator coroutine);
        void StopPerform(Coroutine coroutine);
    }
}