using _Project.Code.Runtime.Gameplay.ExplosionFeature.ExplosionEffects;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.ExplosionFeature
{
    public class Explosion
    {
        private readonly IExplosionEffect _effect;
        private readonly float _radius;
        private readonly ReactiveEvent<Vector3> _executed = new();

        public IReadOnlyReactiveEvent<Vector3> Executed => _executed;

        public Explosion(IExplosionEffect effect, float radius)
        {
            _effect = effect;
            _radius = radius;
        }

        public void Execute(Vector3 position, ITeam sourceTeam)
        {
            Collider[] colliders = Physics.OverlapSphere(position, _radius);
            _effect.Apply(FilterColliders(colliders, sourceTeam));
            _executed.Invoke(position);
        }

        private List<Collider> FilterColliders(Collider[] colliders, ITeam sourceTeam)
        {
            List<Collider> result = new();
            
            foreach (Collider collider in colliders)
                if (collider.TryGetComponent(out ITeam team))
                    if (team.TeamType != sourceTeam.TeamType)
                        result.Add(collider);

            return result;
        }
    }
}
