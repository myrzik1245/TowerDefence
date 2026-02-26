using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public class RigidbodyMover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        private readonly float _smooth;
        private readonly ReactiveVariable<Vector3> _position = new ReactiveVariable<Vector3>();

        public RigidbodyMover(Rigidbody rigidbody, float speed, float smooth)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            _smooth = smooth;
        }

        public IReadOnlyReactiveVariable<Vector3> Position => _position;

        public void Move(Vector3 direction, float deltaTime)
        {
            Vector3 velocity = direction * _speed;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, velocity, deltaTime * _smooth);
            _position.Value = _rigidbody.position;
        }
    }
}
