using _Project.Code.Runtime.UI.Core;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.UI.CommonViews
{
    public class ListView<TView> : MonoBehaviour where TView : MonoBehaviour, IView
    {
        [SerializeField] private Transform _parent;

        private List<TView> _elements = new();
        
        public IReadOnlyList<TView> Elements => _elements;

        public void Add(TView viewElement)
        {
            viewElement.transform.SetParent(_parent);
            _elements.Add(viewElement);
        }

        public void Remove(TView viewElement)
        {
            viewElement.transform.SetParent(null);
            _elements.Remove(viewElement);
        }
    }
}
