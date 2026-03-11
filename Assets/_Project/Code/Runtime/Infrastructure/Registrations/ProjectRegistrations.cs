using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.Factories;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.ConfigManagment.Loaders;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.InputService;
using _Project.Code.Runtime.Utility.InputService.Keyboard;
using _Project.Code.Runtime.Utility.LoadScreen;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.Update;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class ProjectRegistrations
    {
        public static void Register(DIContainer projectContainer)
        {
            projectContainer.Register(CreateCoroutinePerformer).AsSingle();
            projectContainer.Register(CreateLoadingScreen).AsSingle();
            projectContainer.Register(CreateResurcesAssetsLoader).AsSingle();
            projectContainer.Register(CreateLoadSceneService).AsSingle();
            projectContainer.Register(CreateConfigsProvider).AsSingle();
            projectContainer.Register(CreateUpdatableService).AsSingle();
            projectContainer.Register(CreateViewsFactory).AsSingle();
            projectContainer.Register(CreateProjectPresentersFactory).AsSingle();
            projectContainer.Register(CreateInputService).AsSingle();
            projectContainer.Register(CreateWallet).AsSingle();
            projectContainer.Register(CreateWinLoseCounter).AsSingle();

            projectContainer.Initialize();
        }

        private static WinLoseCounter CreateWinLoseCounter(DIContainer c)
        {
            return new WinLoseCounter();
        }
        
        private static Wallet CreateWallet(DIContainer c)
        {
            return new Wallet();
        }
        
        private static IInputService CreateInputService(DIContainer c)
        {
            return new KeyboardInput();
        }
        
        private static ProjectPresentersFactory CreateProjectPresentersFactory(DIContainer c)
        {
            return new ProjectPresentersFactory(c);
        }

        private static ViewsFactory CreateViewsFactory(DIContainer c)
        {
            return new ViewsFactory(c);
        }

        private static IUpdatableService CreateUpdatableService(DIContainer c)
        {
            ResourcesAssetsLoader resourceLoader = c.Resolve<ResourcesAssetsLoader>();
            UpdatableService updatableServicePrefab 
                = resourceLoader.Load<UpdatableService>("Utility/UpdatableService");

            return Object.Instantiate(updatableServicePrefab);
        }

        private static ConfigsProvider CreateConfigsProvider(DIContainer c)
        {
            return new ConfigsProvider(
                new ResourcesConfigLoader(c.Resolve<ResourcesAssetsLoader>()));
        }

        private static LoadSceneService CreateLoadSceneService(DIContainer c)
        {
            return new LoadSceneService(
                c.Resolve<ILoadScreen>(),
                c);
        }

        private static ResourcesAssetsLoader CreateResurcesAssetsLoader(DIContainer c)
        {
            return new ResourcesAssetsLoader();
        }

        private static ILoadScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourceLoader = c.Resolve<ResourcesAssetsLoader>();

            LoadScreen loadScreenPrefab = resourceLoader.Load<LoadScreen>("Utility/LoadScreen");
            LoadScreen loadScreen = Object.Instantiate(loadScreenPrefab);

            return loadScreen;
        }

        private static ICoroutinePerformer CreateCoroutinePerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourceLoader = c.Resolve<ResourcesAssetsLoader>();
            CoroutinePerformer coroutinePerformerPrefab 
                = resourceLoader.Load<CoroutinePerformer>("Utility/CoroutinePerformer");

            return Object.Instantiate(coroutinePerformerPrefab);
        }
    }
}
