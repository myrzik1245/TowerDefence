using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using System;

namespace _Project.Code.Runtime.UI.CommonPopups.ContinuePopup
{
    public class ContinuePopupPresenter : PopupPresenterBase
    {
        private readonly ContinuePopupView _view;
        private readonly Action _onContinue;
        private IDisposable _continueButtonClickedSubscription;
        
        public ContinuePopupPresenter(ContinuePopupView view, ICoroutinePerformer coroutinePerformer, Action onContinue) : base(view, coroutinePerformer)
        {
            _view = view;
            _onContinue = onContinue;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _continueButtonClickedSubscription = _view.ContinueButtonClicked.Subscribe( () =>
            {
                _onContinue?.Invoke();
                OnCLoseRequest();
            });
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _continueButtonClickedSubscription?.Dispose();
        }
    }
}
