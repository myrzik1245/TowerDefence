using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Runtime.UI.MainMenu.Popups.Shop.Slot
{
    public class ShopSlotView : MonoBehaviour, IView
    {
        [SerializeField] private IconView _iconView;
        [SerializeField] private NumberView _priceView;
        [SerializeField] private Button _buyButton;

        private readonly ReactiveEvent _buyButtonCLicked = new();
        public IReadOnlyReactiveEvent BuyButtonCLicked => _buyButtonCLicked;
        
        public void SetIcon(Sprite icon)
        {
            _iconView.SetIcon(icon);
        }

        public void SetPrice(int price)
        {
            _priceView.SetNumber(price);
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }
        
        private void OnBuyButtonClicked()
        {
            _buyButtonCLicked.Invoke();
        }
    }
}
