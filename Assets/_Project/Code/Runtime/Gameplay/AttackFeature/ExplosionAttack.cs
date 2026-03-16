using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public class ExplosionAttack : IAttack
    {
        private readonly Explosion _explosion;
        private readonly ITeam _sourceTeam;
        
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
