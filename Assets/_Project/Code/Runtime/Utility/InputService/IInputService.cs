using UnityEngine;

namespace _Project.Code.Runtime.Utility.InputService
{
    public interface IInputService
    {
        Vector2 MousePosition { get; }
        IKey Attack { get; }
        IKey Continue { get; }
        bool IsCursorOverUI { get; }
    }
}
