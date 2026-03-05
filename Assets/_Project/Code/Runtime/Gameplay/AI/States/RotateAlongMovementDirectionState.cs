using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

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
            _rotatable.Rotate(_movable.MoveDirection.Value, deltaTime);
        }
    }
}
