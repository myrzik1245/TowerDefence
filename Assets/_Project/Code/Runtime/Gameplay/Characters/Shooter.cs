using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.AttackFeature.Position;
using _Project.Code.Runtime.Gameplay.AttackFeature.Range;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Gameplay.ProcessFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class Shooter : MonoBehaviour, ICharacter, IMovable, IRotatable, IPositionAttack, IRangeAttack
    {
        [SerializeField] private Transform _shootPoint;
        
        private ICoroutinePerformer _coroutinePerformer;
        private Health _health;
        private RigidbodyMover _rigidbodyMover;
        private RigidbodyRotator _rigidbodyRotator;
        private Process _spawnProcess;
        private RangeAttack _rangeAttack;
        private IDisposable _isDeathSubscription;
        
        private readonly Blackboard _blackboard = new();
        private readonly ReactiveVariable<bool> _isInitialized = new();
        private readonly ReactiveVariable<bool> _isSpawned = new();
        private readonly ReactiveEvent<PositionAttackProcess> _positionAttacked = new();

        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public IReadOnlyReactiveVariable<Vector3> Position => _rigidbodyMover.Position;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _isInitialized;
        public IReadOnlyReactiveVariable<bool> IsSpawned => _isSpawned;
        public IReadOnlyReactiveVariable<Vector3> MoveDirection => _rigidbodyMover.Direction;
        public IReadOnlyReactiveVariable<Quaternion> Rotation => _rigidbodyRotator.Rotation;
        public IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked => _positionAttacked;
        public TeamsType TeamType { get; private set; }

        public void Initialize(
            ICoroutinePerformer coroutinePerformer,
            TeamsType teamType,
            IAttack attack,
            int startHealth,
            int maxHealth,
            float moveSpeed,
            float moveSmooth,
            float rotationSpeed,
            float spawnTime,
            float attackTime)
        {
            _coroutinePerformer = coroutinePerformer;
            TeamType = teamType;

            _health = new Health(
                startHealth,
                maxHealth);

            _rigidbodyMover = new RigidbodyMover(
                GetComponent<Rigidbody>(),
                moveSpeed,
                moveSmooth);

            _rigidbodyRotator = new RigidbodyRotator(
                GetComponent<Rigidbody>(),
                rotationSpeed);

            _isDeathSubscription = _health.IsDead.Subscribe(isDead =>
            {
                if (isDead)
                    Destroy(gameObject);
            });
            
            _isInitialized.Value = true;
            
            _spawnProcess = new Process(spawnTime, _coroutinePerformer);

            _spawnProcess.StartProcess(() => _isSpawned.Value = true);
        }

        public bool CanTakeDamage(int damage)
        {
            return _health.CanTakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public void WriteData(string key, object value)
        {
            _blackboard.WriteData(key, value);
        }

        public bool TryGetData<TData>(string key, out TData data)
        {
            return _blackboard.TryGetData(key, out data);
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            _rigidbodyMover.Move(direction, deltaTime);
        }

        public void Rotate(Vector3 direction, float deltaTime)
        {
            _rigidbodyRotator.Rotate(direction, deltaTime);
        }
        
        public void Attack(Vector3 position)
        {
            if (_isSpawned.Value == false)
                return;
            
            _rangeAttack.Attack(position);
        }
        
        public bool InRange(Vector3 position)
        {
            return _rangeAttack.InRange(position);
        }

        private void OnDestroy()
        {
            _isDeathSubscription.Dispose();
        }
    }
}
