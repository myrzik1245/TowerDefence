using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.AttackFeature.Position;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class TargetPositionAttackState : State, IUpdatableState 
    {
        private readonly IBlackboard _blackboard;
        private readonly IPositionAttack _positionAttack;
        
        public TargetPositionAttackState(IBlackboard blackboard, IPositionAttack positionAttack)
        {
            _blackboard = blackboard;
            _positionAttack = positionAttack;
        }

        public override void Enter()
        {
            base.Enter();
            
            if (_blackboard.TryGetData(BlackboardKeys.Target, out Transform target))
            {
                Vector3 position = target.position;
                _positionAttack.Attack(position);
            }
        }
        
        public void Update(float deltaTime)
        {
        }
    }
}
