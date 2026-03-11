using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Utility.Extensions;
using _Project.Code.Runtime.Utility.InputService;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class PositionAttackState : State, IUpdatableState
    {
        private readonly IPositionAttack _positionAttack;
        private readonly IInputService _inputService;
        
        public PositionAttackState(IPositionAttack positionAttack, IInputService inputService)
        {
            _positionAttack = positionAttack;
            _inputService = inputService;
        }

        public void Update(float deltaTime)
        {
            if (_inputService.Attack.Down)
                _positionAttack.Attack(VectorExtensions.CameraToWorldPoint(_inputService.MousePosition));
        }
    }
}
