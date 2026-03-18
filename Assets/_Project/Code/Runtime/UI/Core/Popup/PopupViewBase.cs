using _Project.Code.Runtime.Utility.Reactive.Event;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Core.Popup
{
    public class PopupViewBase : MonoBehaviour, IPopupView
    {
        [SerializeField] private Transform _anticlicker;
        [SerializeField] private Transform _body;

        private readonly ReactiveEvent _closeRequest = new();
        
        public IReadOnlyReactiveEvent CloseRequest => _closeRequest;
        
        public void Show()
        {
            OnPreShow();
            
            Sequence animation = DOTween.Sequence();
            ModifieShowAnimation(animation);
            
            animation.OnComplete(OnPostShow);
        }
        
        public void Hide()
        {
            OnPreHide();
            
            Sequence animation = DOTween.Sequence();
            ModifieHideAnimation(animation);
            
            animation.OnComplete(OnPostHide);
        }

        public void OnCloseButtonCLicked()
        {
            _closeRequest.Invoke();
        }
        
        protected virtual void ModifieShowAnimation(Sequence animation)
        {
        }
        
        protected virtual void ModifieHideAnimation(Sequence animation)
        {
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
