using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ProcessFeature
{
    public class Process
    {
        private readonly float _time;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ReactiveVariable<float> _progress = new();
        private readonly ReactiveEvent _ended = new();

        public Process(float time, ICoroutinePerformer coroutinePerformer)
        {
            _time = time;
            _coroutinePerformer = coroutinePerformer;
        }

        public IReadOnlyReactiveVariable<float> Progress => _progress;
        public IReadOnlyReactiveEvent Ended => _ended;
        
        public void StartProcess(Action callback)
        {
            _coroutinePerformer.StartPerform(Run(callback));
        }

        private IEnumerator Run(Action callback)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < _time)
            {
                elapsedTime += Time.deltaTime;
                _progress.Value = elapsedTime / _time;
                
                yield return null;
            }

            _ended.Invoke();
            callback?.Invoke();
        }
    }
}
