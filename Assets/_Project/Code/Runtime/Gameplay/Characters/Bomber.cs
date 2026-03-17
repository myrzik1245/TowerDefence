using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.AttackFeature.Exposion;
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
    [RequireComponent(typeof(Rigidbody))]
    public class Bomber : MonoBehaviour, ICharacter, IMovable, IRotatable, IExplosion, IAttack
    {
        private Health _health;
        private ExplosionAttack _attack;
        private RigidbodyMover _rigidbodyMover;
        private RigidbodyRotator _rigidbodyRotator;
        private Process _spawnProcess;
        private ICoroutinePerformer _coroutinePerformer;
        
        private readonly Blackboard _blackboard = new();
        private readonly ReactiveVariable<bool> _initialized = new();
        private readonly ReactiveVariable<bool> _spawned = new();

        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public IReadOnlyReactiveVariable<Vector3> Position => _rigidbodyMover.Position;
        public IReadOnlyReactiveVariable<Vector3>  MoveDirection => _rigidbodyMover.Direction;
        public IReadOnlyReactiveVariable<Quaternion> Rotation => _rigidbodyRotator.Rotation;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _initialized;
        public IReadOnlyReactiveVariable<bool> IsSpawned => _spawned;
        public IReadOnlyReactiveEvent<Vector3> AttackExecuted => _attack.AttackExecuted;
        
        public TeamsType TeamType { get; private set; }

        private IDisposable _isDeathSubscription;

        public void Initialize(
            ICoroutinePerformer coroutinePerformer,
            int startHealth,
            int maxHealth,
            ExplosionAttack attack,
            float moveSpeed,
            float moveSmooth,
            float rotateSpeed,
            TeamsType teamType,
            float spawnTime)
        {
            _coroutinePerformer = coroutinePerformer;
            _attack = attack;
            TeamType = teamType;

            _health = new Health(startHealth, maxHealth);
            
            _rigidbodyMover = new RigidbodyMover(
                GetComponent<Rigidbody>(),
                moveSpeed,
                moveSmooth);

            _rigidbodyRotator = new RigidbodyRotator(
                GetComponent<Rigidbody>(),
                rotateSpeed);

            _spawnProcess = new Process(spawnTime, _coroutinePerformer);
            
            _isDeathSubscription = _health.IsDead.Subscribe(isDead => {
                if (isDead)
                    Destroy(gameObject);
            });

            _initialized.Value = true;
            
            _spawnProcess.StartProcess(() => _spawned.Value = true);
        }

        public bool CanTakeDamage(int healAmount)
        {
            return _health.CanTakeDamage(healAmount);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public void Attack(Vector3 position)
        {
            if (_spawned.Value == false)
                return;
            
            _attack.Attack(position);
            _health.Kill();
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            if (_spawned.Value == false)
                return;
            
            _rigidbodyMover.Move(direction, deltaTime);
        }

        public void Rotate(Vector3 direction, float deltaTime)
        {
            if (_spawned.Value == false)
                return;
            
            _rigidbodyRotator.Rotate(direction, deltaTime);
        }
        
        public void WriteData(string key, object value)
        {
            _blackboard.WriteData(key, value);
        }
        
        public bool TryGetData<TData>(string key, out TData data)
        {
            return _blackboard.TryGetData(key, out data);
        }
        
        private void OnDestroy()
        {
            _isDeathSubscription.Dispose();
        }
    }
}
