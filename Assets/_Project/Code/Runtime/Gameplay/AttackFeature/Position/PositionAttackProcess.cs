using _Project.Code.Runtime.Gameplay.ProcessFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Position
{
    public class PositionAttackProcess
    {
        private readonly Process _process;
        
        public PositionAttackProcess(ICoroutinePerformer coroutinePerformer, Vector3 startPosition, Vector3 endPosition, float attackTime)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            _process = new Process(attackTime, coroutinePerformer);
        }

        public IReadOnlyReactiveVariable<float> Progress => _process.Progress;
        public IReadOnlyReactiveEvent Ended => _process.Ended;
        public Vector3 StartPosition { get; } 
        public Vector3 EndPosition { get; }

        public void StartProcess(Action callback)
        {
            _process.StartProcess(callback);
        }
    }
}
