using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Factories;
using _Project.Code.Runtime.UI.MainMenu;
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
            mainMenuContainer.Register(CreateMainMenuPresenter).AsSingle().NonLazy();
            mainMenuContainer.Register(CreateMainMenuPresentersFactory).AsSingle();

            mainMenuContainer.Initialize();
        }

        private static MainMenuPresenter CreateMainMenuPresenter(DIContainer c)
        {
            MainMenuPresentersFactory presentersFactory = c.Resolve<MainMenuPresentersFactory>();
            ViewsFactory viewsFactory = c.Resolve<ViewsFactory>();
            UIRoot uiRoot = c.Resolve<UIRoot>();

            MainMenuView view = viewsFactory.Create<MainMenuView>(ViewIDs.MainMenu, uiRoot.HUDLayer);

            return presentersFactory.CreateMainMenuPresenter(view);
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
