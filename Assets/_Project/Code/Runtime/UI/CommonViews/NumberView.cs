using _Project.Code.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace _Project.Code.Runtime.UI.CommonViews
{
    public class NumberView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;

        public void SetNumber(int number)
        {
            _text.text = number.ToString();
        }
    }
}
