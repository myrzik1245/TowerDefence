using _Project.Code.Runtime.Gameplay.GameLoop;
using _Project.Code.Runtime.Gameplay.StatsFeature;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities
{
    public interface IAbility
    {
        GameEvents GameEvent { get; }
        void Execute();
    }
}
