using _Project.Code.Runtime.Gameplay.ViewCore;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public class AnimatorMovableView : CharacterViewBase
    {
        [SerializeField] private  Animator _animator;
        
        private readonly int _isWalkingKey = Animator.StringToHash("IsWalking");

        private IDisposable _movableSubscription;

        protected override void Initialize()
        {
            IMovable movable = GetComponent<IMovable>();
            
            _movableSubscription = movable.MoveDirection.Subscribe(velocity =>
            {
                if (velocity != Vector3.zero)
                    _animator.SetBool(_isWalkingKey, true);
                else
                    _animator.SetBool(_isWalkingKey, false);
            });
        }

        private void OnDestroy()
        {
            _movableSubscription?.Dispose();
        }
    }
}
