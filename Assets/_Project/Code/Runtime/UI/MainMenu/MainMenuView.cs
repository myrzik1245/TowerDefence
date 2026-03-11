using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.WinLose;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour, IView
    {
        public IReadOnlyReactiveEvent PlayButtonClicked => _playButtonClicked;

        [SerializeField] private Button _playButton;
        [field: SerializeField] public NumberView WalletView { get; private set; }
        [field: SerializeField] public WinLoseView WinLoseView { get; private set; }

        private ReactiveEvent _playButtonClicked = new();

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _playButtonClicked.Invoke();
        }
    }
}
