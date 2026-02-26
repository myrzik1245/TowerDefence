using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public interface IPositionProvider
    {
        IReadOnlyReactiveVariable<Vector3> Position { get; }        
    }
}
