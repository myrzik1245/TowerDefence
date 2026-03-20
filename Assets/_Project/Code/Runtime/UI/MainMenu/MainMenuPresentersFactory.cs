using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.WinLose;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly WinLoseCounter _winLoseCounter;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        
        public MainMenuPresentersFactory(DIContainer mainMenuContainer)
        {
            _loadSceneService = mainMenuContainer.Resolve<LoadSceneService>();
            _coroutinePerformer = mainMenuContainer.Resolve<ICoroutinePerformer>();
            _winLoseCounter = mainMenuContainer.Resolve<WinLoseCounter>();
            _projectPresentersFactory = mainMenuContainer.Resolve<ProjectPresentersFactory>();
        }

        public MainMenuPresenter CreateMainMenuPresenter(MainMenuView view)
        {
            return new MainMenuPresenter(
                _loadSceneService,
                _coroutinePerformer,
                view,
                this,
                _projectPresentersFactory);
        }
        
        public WinLosePresenter CreateWinLoseCounter(WinLoseView view)
        {
            return new WinLosePresenter(
                view,
                _winLoseCounter);
        }
    }
}
