using _Project.Code.Runtime.Gameplay.AttackFeature;
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
            {
                Ray ray = Camera.main.ScreenPointToRay(_inputService.MousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                
                if (groundPlane.Raycast(ray, out float rayDistance))
                {
                    Vector3 worldPoint = ray.GetPoint(rayDistance);
                    _positionAttack.Attack(worldPoint);
                }
            }
        }
    }
}
