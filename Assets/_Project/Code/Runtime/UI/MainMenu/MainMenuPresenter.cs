using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly MainMenuView _view;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private IDisposable _playButtonSubscription;
        
        private List<IPresenter> _presenters = new();

        public MainMenuPresenter(LoadSceneService loadSceneService, ICoroutinePerformer coroutinePerformer, MainMenuView view, MainMenuPresentersFactory mainMenuPresentersFactory, ProjectPresentersFactory projectPresentersFactory)
        {
            _loadSceneService = loadSceneService;
            _coroutinePerformer = coroutinePerformer;
            _view = view;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            _playButtonSubscription = _view.PlayButtonClicked.Subscribe(OnPlayButtonClicked);
            
            _presenters.Add(_mainMenuPresentersFactory.CreateWinLoseCounter(_view.WinLoseView));
            _presenters.Add(_projectPresentersFactory.CreateWalletPresenter(_view.WalletView));
            
            foreach (IPresenter presenter in _presenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _playButtonSubscription?.Dispose();
            
            foreach (IPresenter presenter in _presenters)
                presenter.Dispose();
            
            _presenters.Clear();
        }

        private void OnPlayButtonClicked()
        {
            _coroutinePerformer.StartPerform(
                _loadSceneService.LoadAsync(Scenes.Gameplay, new GameplayInputArgs(0)));
        }
    }
}
