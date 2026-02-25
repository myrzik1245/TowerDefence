using _Project.Code.Runtime.UI.MainMenu;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment;

namespace _Project.Code.Runtime.UI.Factories
{
    public class MainMenuPresentersFactory
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;

        public MainMenuPresentersFactory(DIContainer mainMenuContainer)
        {
            _loadSceneService = mainMenuContainer.Resolve<LoadSceneService>();
            _coroutinePerformer = mainMenuContainer.Resolve<ICoroutinePerformer>();
        }

        public MainMenuPresenter CreateMainMenuPresenter(MainMenuView view)
        {
            return new MainMenuPresenter(
                _loadSceneService,
                _coroutinePerformer,
                view);
        }
    }
}
