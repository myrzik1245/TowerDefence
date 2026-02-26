using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bomber : MonoBehaviour, IDamageble, IReadOnlyHealth, IMovable, IRotatable, ITeam
    {
        private Health _health;
        private IPositionAttack _attack;
        private RigidbodyMover _rigidbodyMover;
        private RigidbodyRotator _rigidbodyRotator;

        public IReadOnlyReactiveVariable<bool> IsDead => _health.IsDead;
        public IReadOnlyReactiveVariable<int> CurrentHealth => _health.CurrentHealth;
        public IReadOnlyReactiveVariable<int> MaxHealth => _health.MaxHealth;
        public IReadOnlyReactiveVariable<Vector3> Position => _rigidbodyMover.Position;
        public IReadOnlyReactiveVariable<Quaternion> Rotation => _rigidbodyRotator.Rotation;
        public TeamsType TeamType { get; private set; }

        private IDisposable _isDeathSubscription;

        public void Initialize(
            int startHealth,
            int maxHealth,
            IPositionAttack attack,
            float moveSpeed,
            float moveSmooth,
            float rotateSpeed,
            TeamsType teamType)
        {
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

            _isDeathSubscription = _health.IsDead.Subscribe(isDead => {
                if (isDead)
                    Destroy(gameObject);
            });
        }

        public bool CanTakeDamage(int healAmount)
        {
            return _health.CanTakeDamage(healAmount);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public void Attack()
        {
            _attack.Attack(transform.position);
            _health.Kill();
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            _rigidbodyMover.Move(direction, deltaTime);
        }

        public void Rotate(Vector3 direction, float deltaTime)
        {
            _rigidbodyRotator.Rotate(direction, deltaTime);
        }

        private void OnDestroy()
        {
            _isDeathSubscription.Dispose();
        }
    }
}
