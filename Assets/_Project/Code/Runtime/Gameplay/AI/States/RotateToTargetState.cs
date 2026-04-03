using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class RotateToTargetState : State, IUpdatableState
    {
        private readonly IRotatable _rotatable;
        private readonly IBlackboard _blackboard;
        private readonly IPositionProvider _positionProvider;
        
        public RotateToTargetState(IRotatable rotatable, IBlackboard blackboard, IPositionProvider positionProvider)
        {
            _rotatable = rotatable;
            _blackboard = blackboard;
            _positionProvider = positionProvider;
        }

        public void Update(float deltaTime)
        {
            if (_blackboard.TryGetData(BlackboardKeys.Target, out Transform target))
            {
                Vector3 direction = (target.position - _positionProvider.Position.Value).normalized;
                _rotatable.Rotate(direction, deltaTime);
            }
        }
    }
}