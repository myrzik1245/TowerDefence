using _Project.Code.Runtime.Utility.Reactive.Event;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Core.Popup
{
    public class PopupViewBase : MonoBehaviour, IPopupView
    {
        [SerializeField] private CanvasGroup _anticlicker;
        [SerializeField] private CanvasGroup _body;

        private readonly ReactiveEvent _closeRequest = new();
        
        public IReadOnlyReactiveEvent CloseRequest => _closeRequest;
        
        public Tween Show()
        {
            OnPreShow();
            
            Sequence showAnimation = DOTween.Sequence();
            ModifiedShowAnimation(showAnimation);

            showAnimation.OnComplete(OnPostShow);
            
            showAnimation.Play();
            
            return showAnimation;
        }
        
        public Tween Hide()
        {
            OnPreHide();
            
            Sequence hideAnimation = DOTween.Sequence();
            ModifiedHideAnimation(hideAnimation);
            
            hideAnimation.OnComplete(OnPostHide);
            
            hideAnimation.Play();
            
            return hideAnimation;
        }

        public void OnCloseButtonCLicked()
        {
            _closeRequest.Invoke();
        }
        
        protected virtual void ModifiedShowAnimation(Sequence showAnimation)
        {
            showAnimation
                .Append(_anticlicker.DOFade(1, 0.2f).From(0))
                .Append(_body.transform.DOScale(1, 0.2f).From(0.2f).SetEase(Ease.InOutQuad))
                .Join(_body.DOFade(1, 0.1f).From(0));
        }
        
        protected virtual void ModifiedHideAnimation(Sequence hideAnimation)
        {
            hideAnimation
                .Append(_body.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InOutBack))
                .Append(_anticlicker.DOFade(0, 0.1f).From(1));
        }

        protected virtual void OnPreShow()
        {
        }

        protected virtual void OnPostShow()
        {
        }

        protected virtual void OnPreHide()
        {
        }

        protected virtual void OnPostHide()
        {
        }
    }
}
