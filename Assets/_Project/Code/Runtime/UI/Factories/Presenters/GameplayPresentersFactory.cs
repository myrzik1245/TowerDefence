using _Project.Code.Runtime.UI.Gameplay.Popups.EndGamePopup;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;

namespace _Project.Code.Runtime.UI.Factories.Presenters
{
    public class GameplayPresentersFactory
    {
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly LoadSceneService _loadSceneService;
        
        public GameplayPresentersFactory(
            DIContainer gameplayContainer,
            GameplayInputArgs gameplayInputArgs)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _coroutinePerformer = gameplayContainer.Resolve<ICoroutinePerformer>();
            _loadSceneService = gameplayContainer.Resolve<LoadSceneService>();
        }

        public EndGamePopupPresenter CreateEndGamePopupPresenter(EndGamePopupView view, string title)
        {
            return new EndGamePopupPresenter(
                _gameplayInputArgs,
                view,
                _coroutinePerformer,
                _loadSceneService,
                title);
        }
    }
}
