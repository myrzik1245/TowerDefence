using System;

namespace _Project.Code.Runtime.Gameplay.AI.Brains
{
    public interface IBrain : IDisposable
    {
        void Enable();
        void Disable();
        void Update(float deltaTime);
    }
}
