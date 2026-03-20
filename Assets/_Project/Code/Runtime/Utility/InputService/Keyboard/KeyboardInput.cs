using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        
        public bool IsCursorOverUI {
            get 
            {
                PointerEventData eventData = new(EventSystem.current)
                {
                    position = MousePosition,
                };

                List<RaycastResult> results = new();
    
                EventSystem.current.RaycastAll(eventData, results);
    
                return results.Count > 0;
            }
        }
    }
}
