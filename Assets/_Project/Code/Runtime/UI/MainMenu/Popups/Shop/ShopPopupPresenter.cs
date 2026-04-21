using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Meta.ShopFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.MainMenu.Popups.Shop.Slot;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using ShopItem = _Project.Code.Runtime.Meta.ShopFeature.ShopItem;

namespace _Project.Code.Runtime.UI.MainMenu.Popups.Shop
{
    public class ShopPopupPresenter : PopupPresenterBase
    {
        private readonly ShopPopupView _view;
        private readonly ShopConfig _config;
        private readonly ShopService _shopService;
        private readonly ViewsFactory _viewsFactory;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;

        private readonly Dictionary<ShopItem, (IPresenter presenter, ShopSlotView view)> _presentersMap = new();
        private readonly List<IDisposable> _subscriptions = new();

        public ShopPopupPresenter(
            ShopPopupView view,
            ICoroutinePerformer coroutinePerformer,
            ShopConfig config,
            ShopService shopService,
            ViewsFactory viewsFactory,
            MainMenuPresentersFactory mainMenuPresentersFactory) : base(view, coroutinePerformer)
        {
            _view = view;
            _config = config;
            _shopService = shopService;
            _viewsFactory = viewsFactory;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
        }

        public override void Initialize()
        {
            foreach (ShopItem item in _shopService.AvailableItems)
            {
                ShopSlotView view = _viewsFactory.Create<ShopSlotView>(ViewIDs.ShopSlotView);

                ShopSlotPresenter presenter = _mainMenuPresentersFactory.CreateShopSlotPresenter(
                    view,
                    _config.Items.First(itemConfig => itemConfig.ID == item.ID),
                    item);

                _presentersMap.Add(item, (presenter, view));

                _subscriptions.Add(item.Buyed.Subscribe(OnBuyet));

                _view.Add(view);
            }

            foreach (KeyValuePair<ShopItem, (IPresenter presenter, ShopSlotView view)> keyValuePair in _presentersMap)
                keyValuePair.Value.presenter.Initialize();
        }

        public override void Dispose()
        {
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();

            foreach (KeyValuePair<ShopItem, (IPresenter presenter, ShopSlotView view)> keyValuePair in _presentersMap)
            {
                keyValuePair.Value.presenter.Dispose();
                _viewsFactory.Release(keyValuePair.Value.view);
            }

            _presentersMap.Clear();
        }

        private void OnBuyet(ShopItem item)
        {
            if (_presentersMap.TryGetValue(item, out var data))
            {
                _view.Remove(data.view);

                data.presenter.Dispose();
                _viewsFactory.Release(data.view);

                _presentersMap.Remove(item);
            }
        }
    }
}
