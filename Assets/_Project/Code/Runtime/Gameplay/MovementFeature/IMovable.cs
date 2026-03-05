using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public interface IMovable : IPositionProvider
    {
        IReadOnlyReactiveVariable<Vector3> MoveDirection { get; }  
        void Move(Vector3 direction, float deltaTime);
    }
}
