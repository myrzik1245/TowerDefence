using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.MainMenu;
using _Project.Code.Runtime.UI.WinLose;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment;

namespace _Project.Code.Runtime.UI.Factories.Presenters
{
    public class MainMenuPresentersFactory
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly Wallet _wallet;
        private readonly WinLoseCounter _winLoseCounter;

        public MainMenuPresentersFactory(DIContainer mainMenuContainer)
        {
            _loadSceneService = mainMenuContainer.Resolve<LoadSceneService>();
            _coroutinePerformer = mainMenuContainer.Resolve<ICoroutinePerformer>();
            _wallet = mainMenuContainer.Resolve<Wallet>();
            _winLoseCounter = mainMenuContainer.Resolve<WinLoseCounter>();
        }

        public MainMenuPresenter CreateMainMenuPresenter(MainMenuView view)
        {
            return new MainMenuPresenter(
                _loadSceneService,
                _coroutinePerformer,
                view);
        }
        
        public WinLosePresenter CreateWinLoseCounter(WinLoseView view)
        {
            return new WinLosePresenter(
                view,
                _winLoseCounter);
        }
    }
}
