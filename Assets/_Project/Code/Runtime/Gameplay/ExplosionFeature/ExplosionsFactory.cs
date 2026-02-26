using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Gameplay.ExplosionFeature.ExplosionEffects;
using _Project.Code.Runtime.Utility.DI;
using System;

namespace _Project.Code.Runtime.Gameplay.ExplosionFeature
{
    public class ExplosionsFactory
    {
        public ExplosionsFactory(DIContainer container)
        {
        }

        public Explosion Create(ExplosionConfig config)
        {
            switch (config)
            {
                case DamageExplosionConfig damageConfig:
                    return CreateDamageExplosion(damageConfig);

                default:
                    throw new ArgumentException($"Unsupported explosion config: {nameof(config)}");
            }
        }

        public Explosion CreateDamageExplosion(DamageExplosionConfig config)
        {
            return new Explosion(new DamageEffect(config.Damage), config.Radius);
        }
    }
}
