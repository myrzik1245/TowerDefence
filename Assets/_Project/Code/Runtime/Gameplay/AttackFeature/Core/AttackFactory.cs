using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Gameplay.AttackFeature.Explosion;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Core
{
    public class AttackFactory
    {
        private readonly ExplosionsFactory _explosionsFactory;

        public AttackFactory(DIContainer container)
        {
            _explosionsFactory = container.Resolve<ExplosionsFactory>();
        }

        public ExplosionAttack CreateExplosionAttack(ExplosionConfig config, ITeam sourceTeam, Stats stats = null)
        {
            ExplosionFeature.Explosion explosion = _explosionsFactory.Create(config, stats);
            return new ExplosionAttack(explosion, sourceTeam);
        }
    }
}
