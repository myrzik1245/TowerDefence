using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using UnityEngine;

namespace _Project.Code.Runtime.UI.WinLose
{
    public class WinLoseView : MonoBehaviour, IView
    {
        [SerializeField] private NumberView _winView;
        [SerializeField] private NumberView _loseView;

        public void SetWin(int win)
        {
            _winView.SetNumber(win);
        }

        public void SetLose(int lose)
        {
            _loseView.SetNumber(lose);
        }
    }
}
