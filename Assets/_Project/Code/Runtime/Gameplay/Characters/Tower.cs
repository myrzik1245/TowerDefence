using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class Tower : MonoBehaviour, ICharacter, IHelable, IPositionAttack
    {
        private Health _health;
        private IPositionAttack _attack;
        private ReactiveVariable<Vector3> _position;
        private readonly Blackboard _blackboard = new Blackboard();

        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public TeamsType TeamType { get; private set; }

        public IReadOnlyReactiveVariable<Vector3> Position => _position;

        public void Initialize(
            int startHealth,
            int maxHealth,
            IPositionAttack attack,
            TeamsType teamType)
        {
            _attack = attack;
            TeamType = teamType;
            _health = new Health(startHealth, maxHealth);
            _position = new ReactiveVariable<Vector3>(transform.position);
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
        
        public void WriteData(string key, object value)
        {
            _blackboard.WriteData(key, value);
        }
        
        public bool TryGetData<TData>(string key, out TData data)
        {
            return _blackboard.TryGetData<TData>(key, out data);
        }
    }
}
