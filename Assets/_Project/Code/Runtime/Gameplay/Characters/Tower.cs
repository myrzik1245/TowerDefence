using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class Tower : MonoBehaviour, IDamageble, IHelable, IReadOnlyHealth, IPositionAttack, ITeam
    {
        private Health _health;
        private IPositionAttack _attack;

        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public TeamsType TeamType { get; private set; }

        public void Initialize(
            int startHealth,
            int maxHealth,
            IPositionAttack attack,
            TeamsType teamType)
        {
            _health = new Health(startHealth, maxHealth);
            _attack = attack;
            TeamType = teamType;
        }

        public bool CanTakeDamage(int healAmount)
        {
            return _health.CanTakeDamage(healAmount);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public bool CanHeal(int healAmount)
        {
            return _health.CanHeal(healAmount);
        }

        public void Heal(int healAmount)
        {
            _health.Heal(healAmount);
        }

        public void Attack(Vector3 position)
        {
            _attack.Attack(position);
        }
    }
}
