using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public interface IStage : IDisposable
    {
        IReadOnlyReactiveVariable<bool> IsCompleate { get; }
        void Start();
        void CleanUp();
        void Update(float deltaTime);
    }
}
