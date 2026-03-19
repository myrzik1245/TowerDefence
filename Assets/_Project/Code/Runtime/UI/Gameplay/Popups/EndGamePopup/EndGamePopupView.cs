using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.Utility.Reactive.Event;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Gameplay.Popups.EndGamePopup
{
    public class EndGamePopupView : PopupViewBase
    {
        [SerializeField] private TextView _title;
        [SerializeField] private Transform[] _buttons;
 
        private readonly ReactiveEvent _restartButtonClicked = new();
        private readonly ReactiveEvent _exitButtonClicked = new();

        public IReadOnlyReactiveEvent RestartButtonClicked => _restartButtonClicked;
        public IReadOnlyReactiveEvent ExitButtonClicked => _exitButtonClicked;
        
        public void SetTitle(string title)
        {
            _title.SetText(title);
        }

        public void OnRestartButtonClicked()
        {
            _restartButtonClicked.Invoke();
        }

        public void OnExitButtonClicked()
        {
            _exitButtonClicked.Invoke();
        }

        protected override void ModifiedShowAnimation(Sequence showAnimation)
        {
            base.ModifiedShowAnimation(showAnimation);

            showAnimation.Append(_title.transform.DOScale(1, 0.2f).From(0));
            
            foreach (Transform button in _buttons)
                showAnimation.Append(button.DOScale(1, 0.2f).From(0));
        }

        protected override void ModifiedHideAnimation(Sequence hideAnimation)
        {
            base.ModifiedHideAnimation(hideAnimation);
            
            foreach (Transform button in _buttons)
                hideAnimation.Prepend(button.DOScale(1, 0.2f).From(0));
        }
    }
}
