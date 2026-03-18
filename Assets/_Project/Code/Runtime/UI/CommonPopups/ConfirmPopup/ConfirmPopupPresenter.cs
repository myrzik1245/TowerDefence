using _Project.Code.Runtime.UI.Core.Popup;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.CommonPopups.ConfirmPopup
{
    public class ConfirmPopupPresenter : PopupPresenterBase
    {
        private readonly ConfirmPopupView _view;
        private readonly string _title;
        private readonly string _message;
        private readonly Action _onConfirm;
        private readonly Action _onCancel;

        private readonly List<IDisposable> _subscriptions = new();
        
        public ConfirmPopupPresenter(
            ConfirmPopupView view,
            string title,
            string message, Action onConfirm, Action onCancel = null) : base(view)
        {
            _view = view;
            _title = title;
            _message = message;
            _onConfirm = onConfirm;
            _onCancel = onCancel;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(_title);
            _view.SetMessage(_message);
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
            
            _subscriptions.Add(_view.ConfirmButtonClicked.Subscribe(() =>
            {
                _onConfirm?.Invoke();
                OnCLoseRequest();
            }));
            
            _subscriptions.Add(_view.CancelButtonClicked.Subscribe(() => 
            {
                _onCancel?.Invoke();
                OnCLoseRequest();
            }));
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();
        }
    }
}
