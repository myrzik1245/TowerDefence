using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Core.Popup;
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
