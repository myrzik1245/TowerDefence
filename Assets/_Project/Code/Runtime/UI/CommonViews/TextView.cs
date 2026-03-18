using _Project.Code.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace _Project.Code.Runtime.UI.CommonViews
{
    public class TextView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
