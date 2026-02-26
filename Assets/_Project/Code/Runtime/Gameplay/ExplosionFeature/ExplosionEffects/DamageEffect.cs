using _Project.Code.Runtime.Gameplay.HealthFeature;
using System;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ExplosionFeature.ExplosionEffects
{
    public class DamageEffect : IExplosionEffect
    {
        private readonly int _damage;

        public DamageEffect(int damage, params Func<Collider, bool>[] filters)
        {
            _damage = damage;
        }

        public void Apply(IEnumerable<Collider> colliders)
        {
            foreach (Collider collider in colliders)
                if (collider.TryGetComponent(out IDamageble damageable))
                    if (damageable.CanTakeDamage(_damage))
                        damageable.TakeDamage(_damage);
        }
    }
}
