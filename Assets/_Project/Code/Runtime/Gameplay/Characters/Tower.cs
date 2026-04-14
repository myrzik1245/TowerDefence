using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.AttackFeature.Position;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.ProcessFeature;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class Tower : MonoBehaviour, ICharacter, IHelable, IPositionAttack, IStatsChangeable, IStatsProvider
    {
        [SerializeField] private Transform _shootPoint;
        
        private Health _health;
        private Process _spawnProcess;
        private PositionAttack _positionAttack;
        private IDisposable _isDeadSubscription;
        private ReactiveVariable<Vector3> _position;
        private Stats _stats;
        
        private readonly Blackboard _blackboard = new();
        private readonly ReactiveVariable<bool> _spawned = new();
        private readonly ReactiveVariable<bool> _initialized = new();

        public TeamsType TeamType { get; private set; }
        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public IReadOnlyReactiveVariable<Vector3> Position => _position;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _initialized;
        public IReadOnlyReactiveVariable<bool> IsSpawned => _spawned;
        public IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked => _positionAttack.PositionAttacked;
        
        public void Initialize(
            ICoroutinePerformer coroutinePerformer,
            Stats stats,
            IAttack attack,
            TeamsType teamType,
            float spawnTime,
            float attackTime)
        {
            TeamType = teamType;
            _stats = stats;
            
            _position = new ReactiveVariable<Vector3>(transform.position);
            
            _health = new Health(
                _stats.Get(StatTypes.Health), 
                _stats.Get(StatTypes.MaxHealth));

            _positionAttack = new PositionAttack(
                coroutinePerformer,
                attack,
                _shootPoint,
                attackTime);
            
            _spawnProcess = new Process(spawnTime, coroutinePerformer);
            
            _isDeadSubscription = _health.IsDead.Subscribe(isDead =>
            {
                if (isDead)
                    Destroy(gameObject);
            });
            
            _initialized.Value = true;

            _spawnProcess.StartProcess(() => _spawned.Value = true);
        }

        public bool CanTakeDamage(int damage)
        {
            return _health.CanTakeDamage(damage);
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
            
            _positionAttack.Attack(position);
        }
        
        public void WriteData(string key, object value)
        {
            _blackboard.WriteData(key, value);
        }
        
        public bool TryGetData<TData>(string key, out TData data)
        {
            return _blackboard.TryGetData<TData>(key, out data);
        }
        
        public void ChangeStat(StatTypes type, Func<int, int> changer)
        {
            _stats.Change(type, changer);
        }
        
        public Stat Get(StatTypes statType)
        {
            return _stats.Get(statType);
        }

        private void OnDestroy()
        {
            _isDeadSubscription?.Dispose();
        }
    }
}
