using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.Factories;
using _Project.Code.Runtime.UI.Factories.Presenters;
using UnityEngine;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        public MainMenuPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory projectPresentersFactory,
            Transform popupLayer) : base(viewsFactory, projectPresentersFactory, popupLayer)
        {
        }
    }
}
