using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Gameplay.ExplosionFeature.ExplosionEffects;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using _Project.Code.Runtime.Utility.DI;
using System;

namespace _Project.Code.Runtime.Gameplay.ExplosionFeature
{
    public class ExplosionsFactory
    {
        public ExplosionsFactory(DIContainer container)
        {
        }

        public Explosion Create(ExplosionConfig config, Stats stats = null)
        {
            switch (config)
            {
                case DamageExplosionConfig damageConfig:
                    return CreateDamageExplosion(damageConfig, stats);

                default:
                    throw new ArgumentException($"Unsupported explosion config: {nameof(config)}");
            }
        }

        public Explosion CreateDamageExplosion(DamageExplosionConfig config, Stats stats)
        {
            if (stats != null)
                return new Explosion(new DamageEffect(stats.Get(StatTypes.Damage)), config.Radius);

            return new Explosion(new DamageEffect(config.Damage), config.Radius);
        }
    }
}
