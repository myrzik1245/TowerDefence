using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.Reactive.Event;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Core.Popup
{
    public abstract class PopupPresenterBase : IPresenter
    {
        private readonly PopupViewBase _view;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ReactiveEvent<PopupPresenterBase> _closeRequest = new();
        private IDisposable _closeRequestSubscription;
        private Coroutine _process;
        
        protected PopupPresenterBase(PopupViewBase view, ICoroutinePerformer coroutinePerformer)
        {
            _view = view;
            _coroutinePerformer = coroutinePerformer;
        }
        
        public IReadOnlyReactiveEvent<PopupPresenterBase> CloseRequest => _closeRequest;

        public void Show()
        {
            if (_process != null)
                _coroutinePerformer.StopPerform(_process);
            
            _process = _coroutinePerformer.StartPerform(AnimationProcess(OnPreShow, _view.Show, OnPostShow));
        }

        public void Hide(Action callback = null)
        {
            if (_process != null)
                _coroutinePerformer.StopPerform(_process);
            
            _process = _coroutinePerformer.StartPerform(AnimationProcess(OnPreHide, _view.Hide, OnPostHide, callback));
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

        private IEnumerator AnimationProcess(Action pre, Func<Tween> action, Action post, Action callback = null)
        {
            pre?.Invoke();
            yield return action.Invoke()?.WaitForCompletion();
            post?.Invoke();
            
            callback?.Invoke();
        }
    }
}
