using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.MainMenu.Popups.Shop;
using UnityEngine;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly ViewsFactory _viewsFactory;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
        private readonly Transform _popupLayer;

        public MainMenuPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPresentersFactory mainMenuPresentersFactory,
            Transform popupLayer) : base(viewsFactory, projectPresentersFactory, popupLayer)
        {
            _viewsFactory = viewsFactory;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _popupLayer = popupLayer;
        }

        public ShopPopupPresenter OpenShopPopup(string title)
        {
            ShopPopupView view = _viewsFactory.Create<ShopPopupView>(ViewIDs.ShopPopup, _popupLayer);
            ShopPopupPresenter presenter = _mainMenuPresentersFactory.CreateShopPopupPresenter(view);
            
            OnPopupCreated(presenter, view);
            
            return presenter;
        }
    }
}
