using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public abstract class EndGameState : State, IUpdatableState
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly GameplayInputArgs _gameplayInputArgs;
        
        protected EndGameState(
            LoadSceneService loadSceneService,
            ICoroutinePerformer coroutinePerformer,
            GameplayInputArgs gameplayInputArgs)
        {
            _loadSceneService = loadSceneService;
            _coroutinePerformer = coroutinePerformer;
            _gameplayInputArgs = gameplayInputArgs;
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.R))
                _coroutinePerformer.StartPerform(_loadSceneService.LoadAsync(Scenes.Gameplay, _gameplayInputArgs));
            
            else if (Input.GetKeyDown(KeyCode.Q))
                _coroutinePerformer.StartPerform(_loadSceneService.LoadAsync(Scenes.MainMenu));
        }
    }
}
