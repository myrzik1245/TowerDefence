using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ExplosionFeature.ExplosionEffects
{
    public interface IExplosionEffect
    {
        public void Apply(IEnumerable<Collider> colliders);
    }
}
