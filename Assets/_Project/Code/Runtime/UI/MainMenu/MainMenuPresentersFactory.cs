using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Meta.ShopFeature;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.MainMenu.Popups.Shop;
using _Project.Code.Runtime.UI.MainMenu.Popups.Shop.Slot;
using _Project.Code.Runtime.UI.WinLose;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment;
using ShopItem = _Project.Code.Runtime.Meta.ShopFeature.ShopItem;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly WinLoseCounter _winLoseCounter;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly ConfigsProvider _configsProvider;
        private readonly ShopService _shopService;
        private readonly ViewsFactory _viewsFactory;

        private readonly DIContainer _mainMenuContainer;

        public MainMenuPresentersFactory(DIContainer mainMenuContainer)
        {
            _mainMenuContainer = mainMenuContainer;

            _loadSceneService = mainMenuContainer.Resolve<LoadSceneService>();
            _coroutinePerformer = mainMenuContainer.Resolve<ICoroutinePerformer>();
            _winLoseCounter = mainMenuContainer.Resolve<WinLoseCounter>();
            _projectPresentersFactory = mainMenuContainer.Resolve<ProjectPresentersFactory>();
            _configsProvider = mainMenuContainer.Resolve<ConfigsProvider>();
            _shopService = mainMenuContainer.Resolve<ShopService>();
            _viewsFactory = mainMenuContainer.Resolve<ViewsFactory>();
        }
        
        public ShopSlotPresenter CreateShopSlotPresenter(ShopSlotView view, ShopItemConfig config, ShopItem shopItem)
        {
            return new ShopSlotPresenter(
                view,
                config,
                shopItem);
        }

        public ShopPopupPresenter CreateShopPopupPresenter(ShopPopupView view)
        {
            return new ShopPopupPresenter(
                view,
                _coroutinePerformer,
                _configsProvider.GetConfig<ShopConfig>(),
                _shopService,
                _viewsFactory,
                this);
        }

        public MainMenuPresenter CreateMainMenuPresenter(MainMenuView view)
        {
            return new MainMenuPresenter(
                _loadSceneService,
                _coroutinePerformer,
                view,
                this,
                _projectPresentersFactory,
                _mainMenuContainer.Resolve<MainMenuPopupService>());
        }

        public WinLosePresenter CreateWinLoseCounter(WinLoseView view)
        {
            return new WinLosePresenter(
                view,
                _winLoseCounter);
        }
    }
}
