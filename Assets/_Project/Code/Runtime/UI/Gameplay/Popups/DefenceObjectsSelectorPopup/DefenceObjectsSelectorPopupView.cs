using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup.Slot;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup
{
    public class DefenceObjectsSelectorPopupView : PopupViewBase
    {
        [SerializeField] private ListView<DefenceObjectsSelectorSlotView> _listView;
        
        public void Add(DefenceObjectsSelectorSlotView element)
        {
            _listView.Add(element);
        }

        public void Remove(DefenceObjectsSelectorSlotView element)
        {
            _listView.Remove(element);
        }
    }
}
