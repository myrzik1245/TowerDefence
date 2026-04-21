using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.UI.Core;
using System;
using ShopItem = _Project.Code.Runtime.Meta.ShopFeature.ShopItem;

namespace _Project.Code.Runtime.UI.MainMenu.Popups.Shop.Slot
{
    public class ShopSlotPresenter : IPresenter
    {
        private readonly ShopItem _shopItem;
        private readonly ShopSlotView _view;
        private readonly ShopItemConfig _config;
        
        private IDisposable _buyButtonClickedSubscription;

        public ShopSlotPresenter(ShopSlotView view, ShopItemConfig config, ShopItem shopItem)
        {
            _view = view;
            _config = config;
            _shopItem = shopItem;
        }

        public void Initialize()
        {
            _view.SetPrice(_config.Price);
            _view.SetIcon(_config.Icon);

            _buyButtonClickedSubscription = _view.BuyButtonCLicked.Subscribe(OnBuyButtonCLicked);
        }

        public void Dispose()
        {
            _buyButtonClickedSubscription?.Dispose();
        }

        private void OnBuyButtonCLicked()
        {
            if (_shopItem.CanBuy())
                _shopItem.Buy();
        }
    }
}
