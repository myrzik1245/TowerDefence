using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.Gameplay.Enemy;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.GameLoop;
using _Project.Code.Runtime.Gameplay.MainHero;
using _Project.Code.Runtime.Gameplay.SpawnerFeature;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class GameplayRegistrations
    {
        private static GameplayInputArgs _gameplayInputArgs;
        
        public static void Register(DIContainer gameplayContainer, GameplayInputArgs gameplayInputArgs)
        {
            _gameplayInputArgs = gameplayInputArgs;
            
            gameplayContainer.Register(CreateCharactersFactory).AsSingle();
            gameplayContainer.Register(CreateExplosionsFactory).AsSingle();
            gameplayContainer.Register(CreateAttackFactory).AsSingle();
            gameplayContainer.Register(CreateStageFactory).AsSingle();
            gameplayContainer.Register(CreateSpawner).AsSingle();
            gameplayContainer.Register(CreateBrainsFactory).AsSingle();
            gameplayContainer.Register(CreateBrainsContext).AsSingle();
            gameplayContainer.Register(CreateMainHeroFactory).AsSingle();
            gameplayContainer.Register(CreateEnemiesFactory).AsSingle();
            gameplayContainer.Register(CreateStageService).AsSingle();
            gameplayContainer.Register(CreateGameplayStatesFactory).AsSingle();
            gameplayContainer.Register(CreateDefenceObjectsFactory).AsSingle();
            gameplayContainer.Register(CreateMainHeroService).AsSingle();
            gameplayContainer.Register(CreateGameplayPresentersFactory).AsSingle();
            gameplayContainer.Register(CreateGameplayPopupService).AsSingle();
            gameplayContainer.Register(CreateUIRoot).AsSingle();
            gameplayContainer.Register(CreateGameplayPresenter).AsSingle().NonLazy();
            gameplayContainer.Register(CreateDefenceObjectSelector).AsSingle();
        }

        private static DefenceObjectsSelector CreateDefenceObjectSelector(DIContainer c)
        {
            return new DefenceObjectsSelector(
                Enum.GetValues(typeof(DefenceObjectTypes)).Cast<DefenceObjectTypes>().ToArray());
        }
        
        private static GameplayPresenter CreateGameplayPresenter(DIContainer c)
        {
            GameplayPresentersFactory gameplayPresentersFactory = c.Resolve<GameplayPresentersFactory>();
            ViewsFactory viewsFactory = c.Resolve<ViewsFactory>();
            UIRoot uiRoot = c.Resolve<UIRoot>();
            
            GameplayView view = viewsFactory.Create<GameplayView>(ViewIDs.Gameplay, uiRoot.HUDLayer); 
            
            return gameplayPresentersFactory.CreateGameplayPresenter(view);
        }
        
        private static UIRoot CreateUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourceLoader = c.Resolve<ResourcesAssetsLoader>();
            UIRoot uiRootPrefab = resourceLoader.Load<UIRoot>("UI/UIRoot");

            return Object.Instantiate(uiRootPrefab);            
        }
        
        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            UIRoot uiRoot = c.Resolve<UIRoot>();
            
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                uiRoot.PopupsLayer,
                c.Resolve<GameplayPresentersFactory>());
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
        {
            return new GameplayPresentersFactory(
                c,
                _gameplayInputArgs);
        }
        
        private static MainHeroService CreateMainHeroService(DIContainer c)
        {
            return new MainHeroService(
                c.Resolve<MainHeroFactory>());
        }
        
        private static DefenceObjectsFactory CreateDefenceObjectsFactory(DIContainer c)
        {
            return new DefenceObjectsFactory(c);
        }
        
        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c)
        {
            return new GameplayStatesFactory(c);
        }
        
        private static StageService CreateStageService(DIContainer c)
        {
            return new StageService(
                c.Resolve<StagesFactory>(),
                c.Resolve<ConfigsProvider>().GetConfig<LevelsConfig>().Levels[_gameplayInputArgs.Level]);
        }
        
        private static EnemiesFactory CreateEnemiesFactory(DIContainer c)
        {
            return new EnemiesFactory(c);
        }
        
        private static MainHeroFactory CreateMainHeroFactory(DIContainer c)
        {
            return new MainHeroFactory(c);
        }
        
        private static BrainsContext CreateBrainsContext(DIContainer c)
        {
            return new BrainsContext();
        }
        
        private static BrainsFactory CreateBrainsFactory(DIContainer c)
        {
            return new BrainsFactory(c);
        }
        
        private static SpawnersFactory CreateSpawner(DIContainer c)
        {
            return new SpawnersFactory(c);
        }
        
        private static StagesFactory CreateStageFactory(DIContainer c)
        {
            return new StagesFactory(c);
        }
        
        private static AttackFactory CreateAttackFactory(DIContainer c)
        {
            return new AttackFactory(c);
        }

        private static ExplosionsFactory CreateExplosionsFactory(DIContainer c)
        {
            return new ExplosionsFactory(c);
        }

        private static CharactersFactory CreateCharactersFactory(DIContainer c)
        {
            return new CharactersFactory(c);
        }
    }
}
