using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public interface IPositionAttack : IAttack
    {
        IReadOnlyReactiveEvent<PositionAttackProcess> Attacked { get; }
    }
}
