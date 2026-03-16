using _Project.Code.Runtime.Gameplay.ProcessFeature;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public class PositionAttackProcess
    {
        private readonly Process _process;
        
        public PositionAttackProcess(Vector3 startPosition, Vector3 endPosition, float attackTime)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            _process = new Process(attackTime);
        }

        public IReadOnlyReactiveVariable<float> Progress => _process.Progress;
        public Vector3 StartPosition { get; } 
        public Vector3 EndPosition { get; }

        public IEnumerator StartProcess(Action callback)
        {
            yield return _process.StartProcess(callback);
        }
    }
}
