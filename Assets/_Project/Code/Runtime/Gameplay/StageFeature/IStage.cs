using _Project.Code.Runtime.Utility.Reactive.Event;
using System;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public interface IStage : IDisposable
    {
        IReadOnlyReactiveEvent Compleated { get; }
        void Enter();
        void Exit();
        void Update(float deltaTime);
    }
}
