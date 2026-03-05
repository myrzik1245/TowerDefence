using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.MovementFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public interface ICharacter : IDamageble, IReadOnlyHealth, ITeam, IBlackboard, IPositionProvider
    {
    }
}
