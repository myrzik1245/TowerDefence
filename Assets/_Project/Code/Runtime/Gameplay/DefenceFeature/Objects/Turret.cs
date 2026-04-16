using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.AttackFeature.Position;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature.Objects
{
    public class Turret : MonoBehaviour, IRotatable, IPositionAttack, ITeam, IInitializableCharacter, IBlackboard, IPositionProvider
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Rigidbody _headRigidbody;

        private RigidbodyRotator _rotator;
        private PositionAttack _positionAttack;

        private readonly ReactiveVariable<bool> _isInitialized = new();
        private readonly Blackboard _blackboard = new();
        private readonly ReactiveVariable<Vector3> _position = new();
        
        public IReadOnlyReactiveVariable<Quaternion> Rotation => _rotator.Rotation;
        public IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked => _positionAttack.PositionAttacked;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _isInitialized;
        public IReadOnlyReactiveVariable<Vector3> Position => _position;
        public TeamsType TeamType { get; private set; }
        public bool IsDestroyed { get; private set; }
        public IReadOnlyReactiveEvent Attacked => _positionAttack.Attacked;

        public void Initialize(
            float rotationSpeed,
            ICoroutinePerformer coroutinePerformer,
            IAttack attack,
            float attackTime,
            TeamsType teamType)
        {
            TeamType = teamType;
            
            _position.Value = transform.position;

            _rotator = new RigidbodyRotator(
                _headRigidbody,
                rotationSpeed);

            _positionAttack = new PositionAttack(
                coroutinePerformer,
                attack,
                _shootPoint,
                attackTime);

            _isInitialized.Value = true;
        }


        public void Rotate(Vector3 direction, float deltaTime)
        {
            _rotator.Rotate(direction, deltaTime);
        }

        public void Attack(Vector3 position)
        {
            _positionAttack.Attack(position);
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
            IsDestroyed = true;
        }
    }
}