using _Project.Code.Runtime.UI.CommonPopups.ConfirmPopup;
using _Project.Code.Runtime.UI.Factories;
using _Project.Code.Runtime.UI.Factories.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Core.Popup
{
    public abstract class PopupService : IDisposable
    {
        private readonly ViewsFactory _viewsFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly Transform _popupLayer;

        private readonly List<PopupData> _popupsData = new();

        protected PopupService(ViewsFactory viewsFactory, ProjectPresentersFactory projectPresentersFactory, Transform popupLayer)
        {
            _viewsFactory = viewsFactory;
            _projectPresentersFactory = projectPresentersFactory;
            _popupLayer = popupLayer;
        }

        public ConfirmPopupPresenter OpenConfirmPopup(
            string title,
            string message,
            Action onConfirm,
            Action onCancel = null,
            Action closeCallback = null)
        {
            ConfirmPopupView view = _viewsFactory.Create<ConfirmPopupView>(ViewIDs.ConfirmPopup, _popupLayer);

            ConfirmPopupPresenter popup = _projectPresentersFactory.CreateConfirmPopupPresenter(view,
                title,
                message,
                onConfirm,
                onCancel);

            OnPopupCreated(popup, view, closeCallback);
            
            return popup;
        }

        public void Close(PopupPresenterBase popup)
        {
            PopupData data = _popupsData.First(popupData => popupData.Presenter == popup);
            
            data.Presenter.Hide(() => 
            {
                DisposePopup(data);
                data.CloseCallback?.Invoke();
                
                _popupsData.Remove(data);
            });
            
            data.CloseRequestSubscription.Dispose();
            
        }

        public void Dispose()
        {
            foreach (PopupData data in _popupsData)
            {
                data.CloseRequestSubscription.Dispose();
                DisposePopup(data);
            }
            
            _popupsData.Clear();
        }

        protected void OnPopupCreated(PopupPresenterBase presenter, PopupViewBase view, Action closeCallback = null)
        {
            presenter.Initialize();
            presenter.Show();

            IDisposable subsctiption = presenter.CloseRequest.Subscribe(Close);
            
            _popupsData.Add(new PopupData(presenter, view, subsctiption, closeCallback));
        }

        private void DisposePopup(PopupData data)
        {
            data.Presenter.Dispose();
            _viewsFactory.Release(data.View);
        }

        private class PopupData
        {
            public PopupData(PopupPresenterBase presenter, PopupViewBase view, IDisposable closeRequestSubscription, Action closeCallback)
            {
                Presenter = presenter;
                View = view;
                CloseRequestSubscription = closeRequestSubscription;
                CloseCallback = closeCallback;
            }
            
            public PopupPresenterBase Presenter { get; }
            public PopupViewBase View { get; }
            public IDisposable CloseRequestSubscription { get; }
            public Action CloseCallback { get; }
        }
    }
}
