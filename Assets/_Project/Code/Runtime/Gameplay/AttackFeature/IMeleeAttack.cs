using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature
{
    public interface IMeleeAttack : IAttack
    {
        public IReadOnlyReactiveEvent<Vector3> MeleeAttacked { get; }
    }
}
