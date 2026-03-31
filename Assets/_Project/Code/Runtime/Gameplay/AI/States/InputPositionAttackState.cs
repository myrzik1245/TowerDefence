using _Project.Code.Runtime.Gameplay.AttackFeature.Position;
using _Project.Code.Runtime.Utility.Extensions;
using _Project.Code.Runtime.Utility.InputService;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class InputPositionAttackState : State, IUpdatableState
    {
        private readonly IPositionAttack _positionAttack;
        private readonly IInputService _inputService;
        
        public InputPositionAttackState(IPositionAttack positionAttack, IInputService inputService)
        {
            _positionAttack = positionAttack;
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();
            
            Vector3 position = VectorExtensions.CameraToWorldPoint(_inputService.MousePosition);
            _positionAttack.Attack(position);
        }
        
        public void Update(float deltaTime)
        {
        }
    }
}
