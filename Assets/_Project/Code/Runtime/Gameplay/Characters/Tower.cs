using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.ProcessFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class Tower : MonoBehaviour, ICharacter, IHelable, IPositionAttack
    {
        [SerializeField] private Transform _shootPoint;
        
        private Health _health;
        private IAttack _attack;
        private ReactiveVariable<Vector3> _position;
        private Process _spawnProcess;
        private float _attackTime;

        private readonly Blackboard _blackboard = new();
        private readonly ReactiveVariable<bool> _initialized = new();
        private readonly ReactiveVariable<bool> _spawned = new();
        private readonly ReactiveEvent<PositionAttackProcess> _attacked = new();

        public TeamsType TeamType { get; private set; }
        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public IReadOnlyReactiveVariable<Vector3> Position => _position;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _initialized;
        public IReadOnlyReactiveVariable<bool> IsSpawned => _spawned;
        public IReadOnlyReactiveEvent<PositionAttackProcess> Attacked => _attacked;

        private IDisposable _disposable;
        
        public void Initialize(
            int startHealth,
            int maxHealth,
            IAttack attack,
            TeamsType teamType,
            float spawnTime,
            float attackTime)
        {
            _attack = attack;
            TeamType = teamType;
            _attackTime = attackTime;
            _health = new Health(startHealth, maxHealth);
            _position = new ReactiveVariable<Vector3>(transform.position);

            _spawnProcess = new Process(spawnTime);
            
            _disposable = _health.IsDead.Subscribe(isDead =>
            {
                if (isDead)
                    Destroy(gameObject);
            });
            
            _initialized.Value = true;
            
            StartCoroutine(_spawnProcess.StartProcess(() => _spawned.Value = true));
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
            if (_spawned.Value == false)
                return false;
            
            return _health.CanHeal(healAmount);
        }

        public void Heal(int healAmount)
        {
            _health.Heal(healAmount);
        }

        public void Attack(Vector3 position)
        {
            if (_spawned.Value == false)
                return;

            PositionAttackProcess attackProcess = new(_shootPoint.position, position, _attackTime);
            _attacked.Invoke(attackProcess);
            StartCoroutine(attackProcess.StartProcess(() => _attack.Attack(position)));
        }
        
        public void WriteData(string key, object value)
        {
            _blackboard.WriteData(key, value);
        }
        
        public bool TryGetData<TData>(string key, out TData data)
        {
            return _blackboard.TryGetData<TData>(key, out data);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}
