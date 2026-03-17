using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.PositionAttack
{
    public class PositionAttackProcessView : IDisposable
    {
        private readonly PositionAttackProcess _attackProcess;
        private readonly GameObject _projectilePrefab;
        private readonly ParticleSystem _vfxPrefab;

        private readonly List<IDisposable> _subscriptions = new();
        
        private GameObject _projectile;
        private ParticleSystem _vfx;

        public PositionAttackProcessView(PositionAttackProcess attackProcess, GameObject projectilePrefab, ParticleSystem vfxPrefab)
        {
            _attackProcess = attackProcess;
            _projectilePrefab = projectilePrefab;
            _vfxPrefab = vfxPrefab;
        }

        public void Initialize()
        {
            _subscriptions.Add(_attackProcess.Progress.Subscribe(OnProgressChanged));
            _subscriptions.Add(_attackProcess.Ended.Subscribe(OnEnded));
            
            Quaternion lockRotation = Quaternion.LookRotation(_attackProcess.EndPosition - _attackProcess.StartPosition);
            
            _projectile = Object.Instantiate(_projectilePrefab, _attackProcess.EndPosition, lockRotation);
        }

        public void Dispose()
        {
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();
            
            Object.Destroy(_projectile.gameObject);
            
            if (_vfx != null)
                Object.Destroy(_vfx.gameObject);
        }

        private void OnProgressChanged(float progress)
        { 
            _projectile.transform.position = Vector3.Lerp(_attackProcess.StartPosition, _attackProcess.EndPosition, progress);
        }

        private void OnEnded()
        {
            Object.Destroy(_projectile.gameObject);
            _vfx = Object.Instantiate(_vfxPrefab, _attackProcess.EndPosition, Quaternion.identity);
        }
    }
}
