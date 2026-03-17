using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class ExplosionState : State, IUpdatableState
    {
        private readonly IBlackboard _blackboard;
        private readonly IAttack _positionAttack;
        private readonly IMovable _movable;
        
        public ExplosionState(IBlackboard blackboard, IAttack positionAttack, IMovable movable)
        {
            _blackboard = blackboard;
            _positionAttack = positionAttack;
            _movable = movable;
        }

        public override void Enter()
        {
            base.Enter();
            
            _positionAttack.Attack(_movable.Position.Value);
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}
