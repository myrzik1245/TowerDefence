using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Meta.WinLoseFeature
{
    public interface IReadOnlyWinLoseCounter
    {
        IReadOnlyReactiveVariable<int> LoseCount { get; }
        IReadOnlyReactiveVariable<int> WinLoseCount { get; }
    }
}
