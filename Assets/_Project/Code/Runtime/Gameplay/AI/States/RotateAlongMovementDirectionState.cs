using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class RotateAlongMovementDirectionState : State, IUpdatableState
    {
        private IMovable _movable;
        private IRotatable _rotatable;
        
        public RotateAlongMovementDirectionState(IMovable movable, IRotatable rotatable)
        {
            _movable = movable;
            _rotatable = rotatable;
        }

        public void Update(float deltaTime)
        {
            Vector3 direction = _movable.MoveDirection.Value;
            direction.y = 0;
            
            _rotatable.Rotate(direction, deltaTime);
        }
    }
}
