using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Gameplay.HealthFeature
{
    public interface IAlive
    {
        IReadOnlyReactiveVariable<bool> IsDead { get; }
    }
}
