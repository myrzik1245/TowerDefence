using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public interface IMovable : IPositionProvider
    {
        void Move(Vector3 direction, float deltaTime);
    }
}
