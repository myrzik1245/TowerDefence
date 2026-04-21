using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.GameLoop;
using _Project.Code.Runtime.Gameplay.MainHero;
using _Project.Code.Runtime.Infrastructure.Registrations;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using _Project.Code.Runtime.Utility.Update;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.EntryPoints.SceneEntryPoints
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        private IUpdatableService _updatableService;
        private BrainsContext _brainsContext;
        private GameplayStateMachine _gameLoop;

        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs inputSceneArgs)
        {
            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"Input args must be of type {nameof(GameplayInputArgs)}");

            GameplayRegistrations.Register(container, gameplayInputArgs);
            container.Initialize();

            _updatableService = container.Resolve<IUpdatableService>();

            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();
            GameplayStatesFactory gameplayStatesFactory = container.Resolve<GameplayStatesFactory>();
            MainHeroFactory mainHeroFactory = container.Resolve<MainHeroFactory>();

            yield return playerDataProvider.Load();
            
            mainHeroFactory.CreateTower(Vector3.zero);

            _gameLoop = gameplayStatesFactory.CreateGameplayStateMachine();

            _updatableService.AddRequest(_gameLoop);
        }

        public override void Run()
        {
            _gameLoop.Enter();
        }


        private void OnDestroy()
        {
            _updatableService.RemoveRequest(_brainsContext);
            _updatableService.RemoveRequest(_gameLoop);
            _gameLoop.Dispose();
        }
    }
}
