using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Gameplay.HealthFeature
{
    public interface IReadOnlyHealth : IAlive
    {
        IReadOnlyReactiveVariable<int> CurrentHealth { get; }
        IReadOnlyReactiveVariable<int> MaxHealth { get; }
    }

}
