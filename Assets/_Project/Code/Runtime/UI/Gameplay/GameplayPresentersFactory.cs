using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Gameplay.Popups.EndGamePopup;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;

namespace _Project.Code.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly LoadSceneService _loadSceneService;
        private ProjectPresentersFactory _projectPresentersFactory;

        public GameplayPresentersFactory(
            DIContainer gameplayContainer,
            GameplayInputArgs gameplayInputArgs)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _coroutinePerformer = gameplayContainer.Resolve<ICoroutinePerformer>();
            _loadSceneService = gameplayContainer.Resolve<LoadSceneService>();
            _projectPresentersFactory = gameplayContainer.Resolve<ProjectPresentersFactory>();
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

        public GameplayPresenter CreateGameplayPresenter(GameplayView view)
        {
            return new GameplayPresenter(
                view,
                _projectPresentersFactory,
                this);
        }
    }
}
