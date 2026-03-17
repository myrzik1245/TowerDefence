using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public interface IInitializableCharacter
    {
        IReadOnlyReactiveVariable<bool> IsInitialized { get; }
    }
}
