using _Project.Code.Runtime.UI.Core;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.Gameplay
{
    public class GameplayPresenter : IPresenter
    {
        private readonly GameplayView _view;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;

        private List<IPresenter> _presenters = new();
        
        public GameplayPresenter(GameplayView view, ProjectPresentersFactory projectPresentersFactory, GameplayPresentersFactory gameplayPresentersFactory)
        {
            _view = view;
            _projectPresentersFactory = projectPresentersFactory;
            _gameplayPresentersFactory = gameplayPresentersFactory;
        }

        public void Initialize()
        {
            _presenters.Add(_projectPresentersFactory.CreateWalletPresenter(_view.WalletView));
            
            foreach(IPresenter presenter in _presenters)
                presenter.Initialize();
        }
        
        public void Dispose()
        {
            foreach(IPresenter presenter in _presenters)
                presenter.Dispose();
        }
    }
}
