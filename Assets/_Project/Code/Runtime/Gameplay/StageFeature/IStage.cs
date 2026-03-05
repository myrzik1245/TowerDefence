using _Project.Code.Runtime.Utility.Reactive.Event;
using System;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public interface IStage : IDisposable
    {
        IReadOnlyReactiveEvent Compleated { get; }
        void Start();
        void CleanUp();
        void Update(float deltaTime);
    }
}
