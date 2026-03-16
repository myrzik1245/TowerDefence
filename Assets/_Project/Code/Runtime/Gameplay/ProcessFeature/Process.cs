using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ProcessFeature
{
    public class Process
    {
        private readonly float _time;
        private readonly ReactiveVariable<float> _progress = new();
        
        public Process(float time)
        {
            _time = time;
        }

        public IReadOnlyReactiveVariable<float> Progress => _progress;
        
        public IEnumerator StartProcess(Action callback)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < _time)
            {
                elapsedTime += Time.deltaTime;
                _progress.Value = elapsedTime / _time;
                
                yield return null;
            }

            _progress.Value = 1;
            callback?.Invoke();
        }
    }
}
