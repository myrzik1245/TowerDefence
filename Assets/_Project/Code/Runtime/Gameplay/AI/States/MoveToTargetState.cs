using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class MoveToTargetState : State, IUpdatableState
    {
        private readonly IBlackboard _blackboard;
        private readonly IMovable _movable;
        
        public MoveToTargetState(IBlackboard blackboard, IMovable movable)
        {
            _blackboard = blackboard;
            _movable = movable;
        }
        
        public void Update(float deltaTime)
        {
            if (_blackboard.TryGetData(BlackboardKeys.Target, out Transform target))
            {
                if (target != null)
                {
                    Vector3 direction = (target.position - _movable.Position.Value).normalized;
                
                    _movable.Move(direction, deltaTime);
                }
            }
        }
    }
}
