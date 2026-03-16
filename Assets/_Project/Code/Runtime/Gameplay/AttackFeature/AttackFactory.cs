using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public class AttackFactory
    {
        private readonly ExplosionsFactory _explosionsFactory;

        public AttackFactory(DIContainer container)
        {
            _explosionsFactory = container.Resolve<ExplosionsFactory>();
        }

        public IAttack CreateExplosionAttack(ExplosionConfig config, ITeam sourceTeam)
        {
            Explosion explosion = _explosionsFactory.Create(config);
            return new ExplosionAttack(explosion, sourceTeam);
        }
    }
}
