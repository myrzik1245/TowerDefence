using _Project.Code.Runtime.Configs.Abilities;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.Enemy;
using _Project.Code.Runtime.Gameplay.GameLoop;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities
{
    public class DamageEnemyAbility : IAbility
    {
        private readonly EnemiesFactory _enemiesFactory;
        private readonly ValueModifier _valueModifier;
        private readonly int _targetsCount;
        
        public DamageEnemyAbility(
            EnemiesFactory enemiesFactory,
            ValueModifier valueModifier,
            int targetsCount,
            GameEvents gameEvent)
        {
            _enemiesFactory = enemiesFactory;
            _valueModifier = valueModifier;
            _targetsCount = targetsCount;
            GameEvent = gameEvent;
        }

        public GameEvents GameEvent { get; }
        
        public void Execute()
        {
            foreach (ICharacter enemy in _enemiesFactory.Enemies.List.Take(_targetsCount))
            {
                int damage = _valueModifier.GetDelta(enemy.MaxHealth.Value);
                
                if (enemy.CanTakeDamage(damage))
                    enemy.TakeDamage(damage);
            }
        }
    }
}
