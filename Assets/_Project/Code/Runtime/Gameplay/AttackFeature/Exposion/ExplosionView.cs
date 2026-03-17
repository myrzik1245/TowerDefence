using _Project.Code.Runtime.Gameplay.ViewCore;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Exposion
{
    public class ExplosionView : CharacterViewBase
    {
        [SerializeField] private ParticleSystem _vfxPrefab;
        
        private IExplosion _explosion;
        
        private IDisposable _explosionSubscription;

        protected override void Initialize()
        {
            _explosion = GetComponent<IExplosion>();

            _explosionSubscription = _explosion.AttackExecuted.Subscribe(position =>
            {
                Instantiate(_vfxPrefab, position, Quaternion.identity);
            });
        }

        private void OnDestroy()
        {
            _explosionSubscription?.Dispose();
        }
    }
}
