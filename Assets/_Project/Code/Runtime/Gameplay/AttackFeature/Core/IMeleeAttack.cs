using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Core
{
    public interface IMeleeAttack : IAttack
    {
        IReadOnlyReactiveEvent<Vector3> MeleeAttacked { get; }
    }
}
