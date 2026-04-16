using _Project.Code.Runtime.Configs.Abilities;
using _Project.Code.Runtime.Gameplay.GameLoop;
using _Project.Code.Runtime.Gameplay.StatsFeature;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities
{
    public class ChangeStatAbility : IAbility
    {
        private readonly IStatsChangeable _statsChangeable;
        private readonly StatTypes _statType;
        private readonly ValueModifier _modifier;
        
        public ChangeStatAbility(
            IStatsChangeable statsChangeable,
            StatTypes statType,
            ValueModifier modifier,
            GameEvents gameGameEvent)
        {
            _statsChangeable = statsChangeable;
            _statType = statType;
            _modifier = modifier;
            GameEvent = gameGameEvent;
        }

        public GameEvents GameEvent { get; }
        
        public void Execute()
        {
            _statsChangeable.ChangeStat(_statType, _modifier.GetChanger());
        }
    }
}
