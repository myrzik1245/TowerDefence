using _Project.Code.Runtime.Configs.Abilities;
using _Project.Code.Runtime.Gameplay.GameLoop;
using _Project.Code.Runtime.Gameplay.HealthFeature;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities
{
    public class HealAbility : IAbility
    {
        private readonly IHealable _healable;
        private readonly IReadOnlyHealth _health;
        private readonly ValueModifier _modifier;
        
        public HealAbility(IHealable healable, IReadOnlyHealth health, ValueModifier modifier, GameEvents gameEvent)
        {
            _healable = healable;
            _health = health;
            _modifier = modifier;
            GameEvent = gameEvent;
        }

        public GameEvents GameEvent { get; }
        
        public void Execute()
        {
            int healAmount = _modifier.GetDelta(_health.CurrentHealth.Value);
            
            if (_healable.CanHeal(healAmount))
                _healable.Heal(healAmount);
        }
    }
}
