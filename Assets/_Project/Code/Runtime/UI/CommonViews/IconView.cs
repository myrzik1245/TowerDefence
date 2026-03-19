using _Project.Code.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Runtime.UI.CommonViews
{
    public class IconView :  MonoBehaviour, IView
    {
        [SerializeField] private Image _image;

        public void SetIcon(Sprite icon)
        {
            _image.sprite = icon;
        }
    }
}
