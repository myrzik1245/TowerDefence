using _Project.Code.Runtime.Gameplay.AttackFeature.PositionAttack;
using _Project.Code.Runtime.Utility.Reactive.Event;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Core
{
    public interface IPositionAttack : IAttack
    {
        IReadOnlyReactiveEvent<PositionAttackProcess> PositionAttacked { get; }
    }
}
