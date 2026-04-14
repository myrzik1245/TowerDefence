using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Configs.Defence;
using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Gameplay.StatsFeature
{
    public class StatsFactory
    {
        public Stats CreateTowerStats(TowerConfig towerConfig)
        {
            return new Stats(
                new Stat(new ReactiveVariable<int>(towerConfig.HealthConfigData.StartHealth), StatTypes.Health),
                new Stat(new ReactiveVariable<int>(towerConfig.HealthConfigData.MaxHealth), StatTypes.MaxHealth),
                CreateExplosionStat(towerConfig.ExplosionConfig));
        }

        public Stats CreateBomberStats(BomberConfig bomberConfig)
        {
            return new Stats(
                new Stat(new ReactiveVariable<int>(bomberConfig.HealthConfigData.StartHealth), StatTypes.Health),
                new Stat(new ReactiveVariable<int>(bomberConfig.HealthConfigData.MaxHealth), StatTypes.MaxHealth),
                CreateExplosionStat(bomberConfig.ExplosionConfig));
        }

        public Stats CreateShooterStats(ShooterConfig shooterConfig)
        {
            return new Stats(
                new Stat(new ReactiveVariable<int>(shooterConfig.HealthConfigData.StartHealth), StatTypes.Health),
                new Stat(new ReactiveVariable<int>(shooterConfig.HealthConfigData.MaxHealth), StatTypes.MaxHealth),
                CreateExplosionStat(shooterConfig.ExplosionConfig));
        }

        public Stats CreatePuddleStats(PuddleConfig puddleConfig)
        {
            return new Stats(
                CreateExplosionStat(puddleConfig.Explosion));
        }

        public Stats CreateMineStats(MineConfig mineConfig)
        {
            return new Stats(
                CreateExplosionStat(mineConfig.Explosion));
        }

        public Stats CreateTurretStats(TurretConfig turretConfig)
        {
            return new Stats();
        }

        private Stat CreateExplosionStat(ExplosionConfig config)
        {
            switch (config)
            {
                case DamageExplosionConfig damageExplosionConfig:
                    return new Stat(new ReactiveVariable<int>(damageExplosionConfig.Damage), StatTypes.Damage);

                default:
                    throw new ArgumentException($"Unsupported explosion config: {nameof(config)}");
            }
        }
    }
}
