using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MovementFeature
{
    public interface IRotatable : IRotationProvider
    {
        void Rotate(Vector3 direction, float deltaTime);
    }
}
