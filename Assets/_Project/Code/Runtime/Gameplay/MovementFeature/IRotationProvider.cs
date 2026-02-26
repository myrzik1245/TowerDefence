using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public interface IRotationProvider
    {
        IReadOnlyReactiveVariable<Quaternion> Rotation { get; }
    }
}
