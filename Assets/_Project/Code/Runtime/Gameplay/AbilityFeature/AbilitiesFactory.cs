using _Project.Code.Runtime.Configs.Abilities;
using _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities;
using _Project.Code.Runtime.Gameplay.Enemy;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.StatsFeature;

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
