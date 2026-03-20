using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.Utility.Reactive.Event;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Runtime.UI.CommonPopups.ContinuePopup
{
    public class ContinuePopupView : PopupViewBase
    {
        [SerializeField] private Button _continueButton;
        private readonly ReactiveEvent _continueButtonClicked = new();

        public IReadOnlyReactiveEvent ContinueButtonClicked => _continueButtonClicked;

        public void OnContinueButtonClicked()
        {
            _continueButtonClicked.Invoke();
        }

        protected override void ModifiedShowAnimation(Sequence showAnimation)
        {
            base.ModifiedShowAnimation(showAnimation);
            
            showAnimation.Append(_continueButton.transform.DOScale(1, 0.2f).From(0));
        }

        protected override void ModifiedHideAnimation(Sequence hideAnimation)
        {
            base.ModifiedHideAnimation(hideAnimation);
            
            hideAnimation.Prepend(_continueButton.transform.DOScale(0, 0.2f).From(1));
        }
    }
}
