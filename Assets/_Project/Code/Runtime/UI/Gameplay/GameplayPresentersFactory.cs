using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup;
using _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup.Slot;
using _Project.Code.Runtime.UI.Gameplay.Popups.EndGamePopup;
using _Project.Code.Runtime.Utility.ConfigManagment;
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
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly ConfigsProvider _configsProvider;
        private readonly DefenceObjectsSelector _defenceObjectsSelector;
        private readonly ViewsFactory _viewsFactory;

        public GameplayPresentersFactory(
            DIContainer gameplayContainer,
            GameplayInputArgs gameplayInputArgs)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _coroutinePerformer = gameplayContainer.Resolve<ICoroutinePerformer>();
            _loadSceneService = gameplayContainer.Resolve<LoadSceneService>();
            _projectPresentersFactory = gameplayContainer.Resolve<ProjectPresentersFactory>();
            _configsProvider = gameplayContainer.Resolve<ConfigsProvider>();
            _defenceObjectsSelector = gameplayContainer.Resolve<DefenceObjectsSelector>();
            _viewsFactory = gameplayContainer.Resolve<ViewsFactory>();
        }

        public DefenceObjectsSelectorPopupPresenter CreateDefenceObjectsSelectorPopupPresenter(DefenceObjectsSelectorPopupView view)
        {
            return new DefenceObjectsSelectorPopupPresenter(
                view,
                _coroutinePerformer,
                _defenceObjectsSelector,
                this,
                _viewsFactory);
        }

        public DefenceObjectsSelectorSlotPresenter CreateDefenceObjectsSelectorSlotPresenter(
            DefenceObjectsSelectorSlotView view,
            DefenceObjectTypes type)
        {
            return new DefenceObjectsSelectorSlotPresenter(
                view,
                _configsProvider.GetConfig<DefenceObjectShopConfig>(),
                type);
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
