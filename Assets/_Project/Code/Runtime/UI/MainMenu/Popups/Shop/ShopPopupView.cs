using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core.Popup;
using _Project.Code.Runtime.UI.MainMenu.Popups.Shop.Slot;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Runtime.UI.MainMenu.Popups.Shop
{
    public class ShopPopupView : PopupViewBase
    {
        [SerializeField] private ListView<ShopSlotView> _slotViews;

        protected override void ModifiedShowAnimation(Sequence showAnimation)
        {
            base.ModifiedShowAnimation(showAnimation);

            foreach (ShopSlotView element in _slotViews.Elements)
                showAnimation.Append(element.transform.DOScale(1, 0.2f).From(0));
        }

        protected override void ModifiedHideAnimation(Sequence hideAnimation)
        {
            base.ModifiedHideAnimation(hideAnimation);
            
            foreach (ShopSlotView element in _slotViews.Elements)
                hideAnimation.Prepend(element.transform.DOScale(0, 0.2f));
        }

        public void Add(ShopSlotView view)
        {
            _slotViews.Add(view);
        }

        public void Remove(ShopSlotView view)
        {
            _slotViews.Remove(view);
        }
    }
}
