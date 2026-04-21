using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Walet;
using _Project.Code.Runtime.UI.WinLose;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Runtime.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shopButton;
        [field: SerializeField] public WalletView WalletView { get; private set; }
        [field: SerializeField] public WinLoseView WinLoseView { get; private set; }

        private readonly ReactiveEvent _playButtonClicked = new();
        private readonly ReactiveEvent _shopButtonClicked = new();

        public IReadOnlyReactiveEvent PlayButtonClicked => _playButtonClicked;
        public IReadOnlyReactiveEvent ShopButtonClicked => _shopButtonClicked;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _playButtonClicked.Invoke();
        }
        
        private void OnShopButtonClicked()
        {
            _shopButtonClicked.Invoke();
        }
    }
}
