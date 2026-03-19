using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.Gameplay.Popups.EndGamePopup
{
    public class EndGamePopupPresenter : PopupPresenterBase
    {
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly EndGamePopupView _view;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly LoadSceneService _loadSceneService;
        private readonly string _title;
        
        private readonly List<IDisposable> _subscriptions = new();

        public EndGamePopupPresenter(
            GameplayInputArgs gameplayInputArgs,
            EndGamePopupView view,
            ICoroutinePerformer coroutinePerformer,
            LoadSceneService loadSceneService,
            string title) : base(view, coroutinePerformer)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _view = view;
            _coroutinePerformer = coroutinePerformer;
            _loadSceneService = loadSceneService;
            _title = title;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(_title);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();
        }

        protected override void OnPostShow()
        {
            base.OnPostShow();
            
            _subscriptions.Add(_view.RestartButtonClicked.Subscribe(OnRestart));
            _subscriptions.Add(_view.ExitButtonClicked.Subscribe(OnExit));
        }

        protected override void OnPreShow()
        {
            base.OnPreShow();
            
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();
        }

        private void OnRestart()
        {
            _coroutinePerformer.StartPerform(
                _loadSceneService.LoadAsync(Scenes.Gameplay, _gameplayInputArgs));
        }

        private void OnExit()
        {
            _coroutinePerformer.StartPerform(_loadSceneService.LoadAsync(Scenes.MainMenu));
        }
    }
}
