using DG.Tweening;

namespace _Project.Code.Runtime.UI.Core.Popup
{
    public interface IPopupView : IView
    {
        Tween Show();
        Tween Hide();
    }
}
