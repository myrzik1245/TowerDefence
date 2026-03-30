using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Position
{
    public class PositionAttack : IPositionAttack
    {
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly IAttack _attack;
        private readonly Transform _shootPoint;
        private readonly float _attackTime;
        
        private readonly ReactiveEvent<PositionAttackProcess> _positionAttacked = new();
        
        public PositionAttack(ICoroutinePerformer coroutinePerformer, IAttack attack, Transform shootPoint, float attackTime)
        {
            _coroutinePerformer = coroutinePerformer;
            _attack = attack;
            _shootPoint = shootPoint;
            _attackTime = attackTime;
        }

        public IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked => _positionAttacked;
        
        public void Attack(Vector3 position)
        {
            PositionAttackProcess attackProcess = new(_coroutinePerformer, _shootPoint.position, position, _attackTime);
            _positionAttacked.Invoke(attackProcess);
            attackProcess.StartProcess(() => _attack.Attack(position));
        }
    }
}
