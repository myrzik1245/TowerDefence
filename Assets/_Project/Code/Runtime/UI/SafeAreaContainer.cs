using UnityEngine;

namespace _Project.Code.Runtime.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaContainer : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _lastSafeArea;
        private Vector2Int _lastScreenSize;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void Update()
        {
            if (CanApply())
                ApplySafeArea();
        }

        private bool CanApply()
        {
            if (_lastSafeArea != Screen.safeArea)
                return true;
            
            if (Vector2.Distance(_lastSafeArea.size, Screen.safeArea.size) < float.Epsilon)
                return true;
            
            return false;
        }

        private void ApplySafeArea()
        {
            _lastSafeArea = Screen.safeArea;;

            Vector2 anchorMin = _lastSafeArea.position;
            Vector2 anchorMax = _lastSafeArea.position + _lastSafeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}
