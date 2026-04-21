using _Project.Code.Runtime.Configs.Abilities;
using _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities;
using _Project.Code.Runtime.Gameplay.Enemy;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using System;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature
{
    public class AbilitiesFactory
    {
        private readonly AbilitiesContext _context;
        private readonly EnemiesFactory _enemiesFactory;
        
        public AbilitiesFactory(AbilitiesContext context, EnemiesFactory enemiesFactory)
        {
            _context = context;
            _enemiesFactory = enemiesFactory;
        }

        public IAbility CreateAbility(AbilityConfig config, IStatsChangeable statsChangeable, IHealable healable, IReadOnlyHealth health)
        {
            switch (config)
            {
                case ChangeStatAbilityConfig changeStatAbilityConfig:
                    return CreateChangeStatsAbility(statsChangeable, changeStatAbilityConfig);

                case DamageEnemyAbilityConfig damageEnemyAbilityConfig:
                    return CreateDamageEnemyAbility(damageEnemyAbilityConfig);
                    
                case HealAbilityConfig healAbilityConfig:
                    return CreateHealAbility(healable, health, healAbilityConfig);
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(config));

            }
        }
        
        public ChangeStatAbility CreateChangeStatsAbility(IStatsChangeable statsChangeable, ChangeStatAbilityConfig config)
        {
            ChangeStatAbility ability = new(
                statsChangeable,
                config.StatType,
                config.Modifier,
                config.Event);

            _context.AddAbility(ability);
            
            return ability;
        }

        public HealAbility CreateHealAbility(IHealable healable, IReadOnlyHealth health, HealAbilityConfig config)
        {
            HealAbility ability = new(
                healable,
                health,
                config.Modifier,
                config.Event);
            
            _context.AddAbility(ability);
            
            return ability;
        }

        public DamageEnemyAbility CreateDamageEnemyAbility(DamageEnemyAbilityConfig config)
        {
            DamageEnemyAbility ability = new(
                _enemiesFactory,
                config.Modifier,
                config.Count,
                config.Event);
            
            _context.AddAbility(ability);
            
            return ability;
        }
    }
}
