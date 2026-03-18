using _Project.Code.Runtime.UI.CommonPopups.ConfirmPopup;
using _Project.Code.Runtime.Utility.DI;
using System;

namespace _Project.Code.Runtime.UI.Factories.Presenters
{
    public class ProjectPresentersFactory
    {
        public ProjectPresentersFactory(DIContainer projectContainer)
        {
        }

        public ConfirmPopupPresenter CreateConfirmPopupPresenter(
            ConfirmPopupView view,
            string title,
            string message,
            Action onConfirm,
            Action onCancel = null)
        {
            return new ConfirmPopupPresenter(
                view,
                title,
                message,
                onConfirm,
                onCancel);
        }
    }
}
