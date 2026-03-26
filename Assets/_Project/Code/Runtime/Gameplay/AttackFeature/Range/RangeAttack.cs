using _Project.Code.Runtime.Gameplay.AttackFeature.Position;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Range
{
    public class RangeAttack
    {
        private readonly float _range;
        private readonly IPositionAttack _attack;
        private readonly IPositionProvider _sourcePosition;
        
        public RangeAttack(float range, IPositionAttack attack, IPositionProvider sourcePosition)
        {
            _range = range;
            _attack = attack;
            _sourcePosition = sourcePosition;
        }

        public IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked => _attack.PositionAttacked;
        
        public void Attack(Vector3 position)
        {
            _attack.Attack(position);
        }

        public bool InRange(Vector3 position)
        {
            return Vector3.Distance(_sourcePosition.Position.Value, position) < _range;
        }
    }
}
