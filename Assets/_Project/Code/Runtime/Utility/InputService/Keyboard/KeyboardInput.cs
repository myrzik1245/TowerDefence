using UnityEngine;

namespace _Project.Code.Runtime.Utility.InputService.Keyboard
{
    public class KeyboardInput : IInputService
    {
        public KeyboardInput()
        {
            Attack = new KeyboardKey(KeyCode.Mouse0);
            Continue = new KeyboardKey(KeyCode.F);
        }

        public Vector2 MousePosition => Input.mousePosition;
        public IKey Attack { get; private set; }
        public IKey Continue { get; private set; }
    }
}
