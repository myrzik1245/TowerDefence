using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.Gameplay.Popups.EndGamePopup;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Gameplay
{
    public class GameplayPopupService : PopupService
    {
        private readonly ViewsFactory _viewsFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly Transform _popupLayer;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;

        public GameplayPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory projectPresentersFactory,
            Transform popupLayer, GameplayPresentersFactory gameplayPresentersFactory) : base(viewsFactory, projectPresentersFactory, popupLayer)
        {
            _viewsFactory = viewsFactory;
            _projectPresentersFactory = projectPresentersFactory;
            _popupLayer = popupLayer;
            _gameplayPresentersFactory = gameplayPresentersFactory;
        }
        public EndGamePopupPresenter OpenEndGamePopup(string title)
        {
            EndGamePopupView view = _viewsFactory.Create<EndGamePopupView>(ViewIDs.EndGamePopup, _popupLayer);
            
            EndGamePopupPresenter presenter = _gameplayPresentersFactory.CreateEndGamePopupPresenter(view, title);
            
            OnPopupCreated(presenter, view);
            
            return presenter;
        }
    }
}
