using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public interface ICharacter : IDamageble, IReadOnlyHealth, ITeam, IBlackboard, IPositionProvider, IInitializableCharacter
    {
        IReadOnlyReactiveVariable<bool> IsSpawned { get; }
    }
}
