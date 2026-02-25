using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly LoadSceneService _loadSceneService;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly MainMenuView _view;

        private List<IDisposable> _disposables = new();

        public MainMenuPresenter(LoadSceneService loadSceneService, ICoroutinePerformer coroutinePerformer, MainMenuView view)
        {
            _loadSceneService = loadSceneService;
            _coroutinePerformer = coroutinePerformer;
            _view = view;
        }

        public void Initialize()
        {
            _disposables.Add(_view.PlayButtonClicked.Subscribe(OnPlayButtonClicked));
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private void OnPlayButtonClicked()
        {
            _coroutinePerformer.StartPerform(_loadSceneService.LoadAsync(Scenes.Gameplay));
        }
    }
}
