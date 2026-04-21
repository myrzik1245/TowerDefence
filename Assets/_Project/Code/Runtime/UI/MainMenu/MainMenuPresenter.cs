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
        private readonly MainMenuPopupService _mainMenuPopupService;

        private List<IDisposable> _disposables = new();
        
        private List<IPresenter> _presenters = new();

        public MainMenuPresenter(LoadSceneService loadSceneService, ICoroutinePerformer coroutinePerformer, MainMenuView view, MainMenuPresentersFactory mainMenuPresentersFactory, ProjectPresentersFactory projectPresentersFactory, MainMenuPopupService mainMenuPopupService)
        {
            _loadSceneService = loadSceneService;
            _coroutinePerformer = coroutinePerformer;
            _view = view;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _projectPresentersFactory = projectPresentersFactory;
            _mainMenuPopupService = mainMenuPopupService;
        }

        public void Initialize()
        {
            _disposables.Add(_view.PlayButtonClicked.Subscribe(OnPlayButtonClicked));
            _disposables.Add(_view.ShopButtonClicked.Subscribe(OnShopButtonClicked));
            
            _presenters.Add(_mainMenuPresentersFactory.CreateWinLoseCounter(_view.WinLoseView));
            _presenters.Add(_projectPresentersFactory.CreateWalletPresenter(_view.WalletView));
            
            foreach (IPresenter presenter in _presenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
            
            foreach (IPresenter presenter in _presenters)
                presenter.Dispose();
            
            _presenters.Clear();
        }

        private void OnShopButtonClicked()
        {
            _mainMenuPopupService.OpenShopPopup("Abilities shop");
        }
        
        private void OnPlayButtonClicked()
        {
            _coroutinePerformer.StartPerform(
                _loadSceneService.LoadAsync(Scenes.Gameplay, new GameplayInputArgs(0)));
        }
    }
}
