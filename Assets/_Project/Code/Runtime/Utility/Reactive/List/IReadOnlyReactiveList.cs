using _Project.Code.Runtime.Utility.Reactive.Event;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.Reactive.List
{
    public interface IReadOnlyReactiveList<T> : IEnumerable<T>
    {
        IReadOnlyReactiveEvent<T> Added { get; }
        IReadOnlyReactiveEvent<T> Removed { get; }
        IReadOnlyList<T> List { get; }
    }
}
