using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.Utility.Reactive.Event;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Runtime.UI.CommonPopups.ConfirmPopup
{
    public class ConfirmPopupView : PopupViewBase
    {
        [SerializeField] private TextView _titleView;
        [SerializeField] private TextView _messageView;
        [SerializeField] private Transform _confirmButton;
        [SerializeField] private Transform _cancelButton;

        private readonly ReactiveEvent _confirmButtonClicked = new();
        private readonly ReactiveEvent _cancelButtonClicked = new();
        
        public IReadOnlyReactiveEvent ConfirmButtonClicked => _confirmButtonClicked;
        public IReadOnlyReactiveEvent CancelButtonClicked => _cancelButtonClicked;

        public void SetTitle(string title)
        {
            _titleView.SetText(title);
        }

        public void SetMessage(string message)
        {
            _messageView.SetText(message);
        }
        
        public void OnConfirmButtonClicked()
        {
            _confirmButtonClicked.Invoke();
        }

        public void OnCancelButtonClicked()
        {
            _cancelButtonClicked.Invoke();
        }

        protected override void ModifiedShowAnimation(Sequence showAnimation)
        {
            base.ModifiedShowAnimation(showAnimation);

            showAnimation
                .Append(_cancelButton.DOScale(1, 0.2f).From(0))
                .Join(_confirmButton.DOScale(1, 0.2f).From(0));
        }

        protected override void ModifiedHideAnimation(Sequence hideAnimation)
        {
            base.ModifiedHideAnimation(hideAnimation);

            hideAnimation
                .Prepend(_cancelButton.DOScale(0, 0.2f).From(1).SetEase(Ease.OutQuint))
                .Join(_confirmButton.DOScale(0, 0.2f).From(1).SetEase(Ease.OutQuint));;
        }
    }
}
