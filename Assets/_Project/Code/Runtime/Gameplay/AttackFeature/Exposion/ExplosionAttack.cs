using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Exposion
{
    public class ExplosionAttack : IAttack, IExplosion
    {
        private readonly Explosion _explosion;
        private readonly ITeam _sourceTeam;
        
        public IReadOnlyReactiveEvent<Vector3> AttackExecuted => _explosion.AttackExecuted;
        
        public ExplosionAttack(Explosion explosion, ITeam sourceTeam)
        {
            _explosion = explosion;
            _sourceTeam = sourceTeam;
        }

        
        public void Attack(Vector3 position)
        {
            _explosion.Execute(position, _sourceTeam);
        }
    }
}
