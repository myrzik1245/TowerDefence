using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup.Slot
{
    public class DefenceObjectsSelectorSlotView : MonoBehaviour, IView
    {
        [SerializeField] private IconView _iconView;
        [SerializeField] private NumberView _priceView;
        
        private readonly ReactiveEvent _buttonClicked = new();
        
        public IReadOnlyReactiveEvent ButtonClicked => _buttonClicked;
        
        public void OnButtonClicked()
        {
            _buttonClicked.Invoke();
        }
        
        public void SetIcon(Sprite icon)
        {
            _iconView.SetIcon(icon);
        }

        public void SetPrice(int price)
        {
            _priceView.SetNumber(price);
        }
    }
}
