using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Factories;
using _Project.Code.Runtime.UI.Factories.Presenters;
using _Project.Code.Runtime.UI.MainMenu;
using _Project.Code.Runtime.UI.Walet;
using _Project.Code.Runtime.UI.WinLose;
using _Project.Code.Runtime.Utility.AssetsManagment;
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
            mainMenuContainer.Register(CreateWalletPresenter).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateWinLosePresenter).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateMainMenuPopupService).AsSingle().NonLazy();
            
            mainMenuContainer.Initialize();
        }
        
        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            UIRoot uiRoot = c.Resolve<UIRoot>();
            
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                uiRoot.PopupsLayer);
        }

        private static WinLosePresenter CreateWinLosePresenter(DIContainer c)
        {
            MainMenuView mainMenuView = c.Resolve<MainMenuView>();
            MainMenuPresentersFactory presentersFactory = c.Resolve<MainMenuPresentersFactory>();
            WinLoseView view = mainMenuView.WinLoseView;
            
            return presentersFactory.CreateWinLoseCounter(view);
        }
        
        private static WalletPresenter CreateWalletPresenter(DIContainer c)
        {
            MainMenuView mainMenuView = c.Resolve<MainMenuView>();
            ProjectPresentersFactory presentersFactory = c.Resolve<ProjectPresentersFactory>();
            WalletView view = mainMenuView.WalletView;
            
            return presentersFactory.CreateWalletPresenter(view);
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
