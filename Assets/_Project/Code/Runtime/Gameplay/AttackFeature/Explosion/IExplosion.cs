using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Explosion
{
    public interface IExplosion
    {
        IReadOnlyReactiveEvent<Vector3> AttackExecuted { get; }
    }
}
