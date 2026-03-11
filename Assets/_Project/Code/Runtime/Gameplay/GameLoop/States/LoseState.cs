using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class LoseState : EndGameState
    {
        private readonly WinLoseCounter _winLoseCounter;

        public LoseState(
            LoadSceneService loadSceneService,
            ICoroutinePerformer coroutinePerformer,
            GameplayInputArgs gameplayInputArgs) : base(loadSceneService, coroutinePerformer, gameplayInputArgs)
        {
        }

        public override void Enter()
        {
            _winLoseCounter.AddLose();
        }
    }
}
