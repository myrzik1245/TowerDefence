using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Meta.ShopFeature;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.MainMenu;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class MainMenuRegistrations
    {
        public static void Register(DIContainer mainMenuContainer)
        {
            mainMenuContainer.Register(CreateUIRoot).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateMainMenuView).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateMainMenuPresenter).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateMainMenuPresentersFactory).AsSingle();
            mainMenuContainer.Register(CreateMainMenuPopupService).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateShopService).AsSingle().NonLazy();
        }

        private static ShopService CreateShopService(DIContainer c)
        {
            return new ShopService(
                c.Resolve<Wallet>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ConfigsProvider>().GetConfig<ShopConfig>(),
                c.Resolve<ICoroutinePerformer>());
        }
        
        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            UIRoot uiRoot = c.Resolve<UIRoot>();
            
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuPresentersFactory>(),
                uiRoot.PopupsLayer);
        }
        
        private static MainMenuPresenter CreateMainMenuPresenter(DIContainer c)
        {
            MainMenuPresentersFactory presentersFactory = c.Resolve<MainMenuPresentersFactory>();
            MainMenuView view = c.Resolve<MainMenuView>();
            
            return presentersFactory.CreateMainMenuPresenter(view);
        }

        private static MainMenuView CreateMainMenuView(DIContainer c)
        {
            ViewsFactory viewsFactory = c.Resolve<ViewsFactory>();
            UIRoot uiRoot = c.Resolve<UIRoot>();

            MainMenuView view = viewsFactory.Create<MainMenuView>(ViewIDs.MainMenu, uiRoot.HUDLayer);
            
            return view;
        }
        
        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer c)
        {
            return new MainMenuPresentersFactory(c);
        }

        private static UIRoot CreateUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourceLoader = c.Resolve<ResourcesAssetsLoader>();
            UIRoot uiRootPrefab = resourceLoader.Load<UIRoot>("UI/UIRoot");

            return Object.Instantiate(uiRootPrefab);            
        }
    }
}
