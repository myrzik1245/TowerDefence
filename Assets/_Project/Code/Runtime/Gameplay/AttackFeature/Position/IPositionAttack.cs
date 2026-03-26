using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Utility.Reactive.Event;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Position
{
    public interface IPositionAttack : IAttack
    {
        IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked { get; }
    }
}
