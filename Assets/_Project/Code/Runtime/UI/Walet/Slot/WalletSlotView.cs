using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Walet.Slot
{
    public class WalletSlotView : MonoBehaviour, IView
    {
        [SerializeField] private NumberView _numberView;
        [SerializeField] private IconView _iconView;

        public void SetAmount(int amount)
        {
            _numberView.SetNumber(amount);
        }

        public void SetIcon(Sprite icon)
        {
            _iconView.SetIcon(icon);
        }
    }
}
