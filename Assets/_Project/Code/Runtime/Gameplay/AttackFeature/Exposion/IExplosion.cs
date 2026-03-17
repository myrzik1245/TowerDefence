using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Exposion
{
    public interface IExplosion
    {
        IReadOnlyReactiveEvent<Vector3> AttackExecuted { get; }
    }
}
