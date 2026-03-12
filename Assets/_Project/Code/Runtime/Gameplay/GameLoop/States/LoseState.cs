using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class LoseState : EndGameState
    {
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly WinLoseCounter _winLoseCounter;

        public LoseState(
            LoadSceneService loadSceneService,
            ICoroutinePerformer coroutinePerformer,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            WinLoseCounter winLoseCounter) : base(loadSceneService, coroutinePerformer, gameplayInputArgs)
        {
            _coroutinePerformer = coroutinePerformer;
            _playerDataProvider = playerDataProvider;
            _winLoseCounter = winLoseCounter;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _winLoseCounter.AddLose();
            
            _coroutinePerformer.StartPerform(_playerDataProvider.Save());
        }
    }
}
