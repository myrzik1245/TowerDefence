using _Project.Code.Runtime.Utility.Reactive.Event;
using System;

namespace _Project.Code.Runtime.UI.Core.Popup
{
    public abstract class PopupPresenterBase : IPresenter
    {
        private readonly PopupViewBase _view;
        private readonly ReactiveEvent<PopupPresenterBase> _closeRequest = new();
        private IDisposable _closeRequestSubscription;
        
        protected PopupPresenterBase(PopupViewBase view)
        {
            _view = view;
        }
        
        public IReadOnlyReactiveEvent<PopupPresenterBase> CloseRequest => _closeRequest;

        public void Show()
        {
            OnPreShow();
            
            _view.Show();

            OnPostShow();
        }

        public void Hide(Action callback = null)
        {
            OnPreHide();
            
            _view.Hide();

            OnPostHide();
            
            callback?.Invoke();
        }

        public virtual void Initialize()
        {
        }
        
        public virtual void Dispose()
        {
            _closeRequestSubscription?.Dispose();
        }

        protected void OnCLoseRequest()
        {
            _closeRequest.Invoke(this);
        }
        
        protected virtual void OnPostShow()
        {
            _closeRequestSubscription = _view.CloseRequest.Subscribe(OnCLoseRequest);
        }
        
        protected virtual void OnPreShow()
        {
        }
        
        protected virtual void OnPostHide()
        {
        }
        
        protected virtual void OnPreHide()
        {
            _closeRequestSubscription?.Dispose();
        }
    }
}
