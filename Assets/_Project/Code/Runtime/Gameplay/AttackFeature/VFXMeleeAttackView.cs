using _Project.Code.Runtime.Gameplay.ViewCore;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public class VFXMeleeAttackView : CharacterViewBase
    {
        [SerializeField] private ParticleSystem _vfxPrefab;
        
        private IMeleeAttack _meleeAttack;
        
        private IDisposable _meleeAttackSubscription;

        protected override void Initialize()
        {
            _meleeAttack = GetComponent<IMeleeAttack>();

            _meleeAttackSubscription = _meleeAttack.MeleeAttacked.Subscribe(position =>
            {
                Instantiate(_vfxPrefab, position, Quaternion.identity);
            });
        }

        private void OnDestroy()
        {
            _meleeAttackSubscription?.Dispose();
        }
    }
}
