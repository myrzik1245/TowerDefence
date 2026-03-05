using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public class RigidbodyRotator
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        private readonly ReactiveVariable<Quaternion> _rotation;

        public IReadOnlyReactiveVariable<Quaternion> Rotation => _rotation;

        public RigidbodyRotator(Rigidbody rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            
            _rotation = new ReactiveVariable<Quaternion>(_rigidbody.rotation);
        }

        public void Rotate(Vector3 direction, float deltaTime)
        {
            if (direction == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, lookRotation, deltaTime * _speed);
            _rotation.Value = lookRotation;
        }
    }
}
