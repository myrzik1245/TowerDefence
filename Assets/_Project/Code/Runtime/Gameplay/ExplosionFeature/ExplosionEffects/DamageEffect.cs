using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ExplosionFeature.ExplosionEffects
{
    public class DamageEffect : IExplosionEffect
    {
        private readonly IReadOnlyReactiveVariable<int> _damage;

        public DamageEffect(int damage)
        {
            _damage = new ReactiveVariable<int>(damage);
        }

        public DamageEffect(Stat damageStat)
        {
            _damage = damageStat.ModifiedStat;
        }

        public void Apply(IEnumerable<Collider> colliders)
        {
            foreach (Collider collider in colliders)
                if (collider.TryGetComponent(out IDamageble damageable))
                    if (damageable.CanTakeDamage(_damage.Value))
                        damageable.TakeDamage(_damage.Value);
        }
    }
}
