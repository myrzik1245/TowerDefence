using System;
using System.Collections;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Utility.Timer
{
    public class TimerService : IDisposable
    {
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly float _endTime;
        private Coroutine _coroutine;
        
        private readonly ReactiveVariable<float> _currentTime = new();
        private readonly ReactiveVariable<bool> _isDone = new();
        private readonly ReactiveEvent _ended = new();

        public TimerService(ICoroutinePerformer coroutinePerformer, float endTime)
        {
            _coroutinePerformer = coroutinePerformer;
            _endTime = endTime;
        }

        public IReadOnlyReactiveVariable<float> CurrentTime => _currentTime;
        public IReadOnlyReactiveVariable<bool> IsDone => _isDone;
        public IReadOnlyReactiveEvent Ended => _ended;

        public void Start()
        {
            if (_coroutine !=  null)
                Stop();

            _coroutine = _coroutinePerformer.StartPerform(Process());
        }

        public void Stop()
        {
            if (_coroutine != null)
                _coroutinePerformer.StopPerform(_coroutine);
        }

        public void Dispose()
        {
            Stop();
        }

        private IEnumerator Process()
        {
            _isDone.Value = false;
            _currentTime.Value = _endTime;
            
            while (_currentTime.Value > 0)
            {
                _currentTime.Value = Mathf.Clamp(_currentTime.Value -= Time.deltaTime,  0, _endTime);
                yield return null;
            }
            
            _ended.Invoke();
            _isDone.Value = true;
        }
    }
}