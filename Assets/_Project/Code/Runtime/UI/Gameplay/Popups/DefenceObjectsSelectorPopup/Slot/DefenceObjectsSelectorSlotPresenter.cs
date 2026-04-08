using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.Utility.Reactive.Event;
using System;

namespace _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup.Slot
{
    public class DefenceObjectsSelectorSlotPresenter : IPresenter
    {
        private readonly DefenceObjectsSelectorSlotView _view;
        private readonly DefenceObjectShopConfig _shopConfig;
        private readonly DefenceObjectTypes _type;
        private readonly ReactiveEvent _buttonClicked = new();
        private IDisposable _buttonClickedSubscription;
        
        public DefenceObjectsSelectorSlotPresenter(DefenceObjectsSelectorSlotView view, DefenceObjectShopConfig shopConfig, DefenceObjectTypes type)
        {
            _view = view;
            _shopConfig = shopConfig;
            _type = type;
        }

        public IReadOnlyReactiveEvent ButtonClicked => _buttonClicked;
        
        public void Initialize()
        {
            _buttonClickedSubscription = _view.ButtonClicked.Subscribe(() => _buttonClicked.Invoke());
            
            _view.SetIcon(_shopConfig.GetIcon(_type));
            _view.SetPrice(_shopConfig.GetPrice(_type));
        }
        
        public void Dispose()
        {
            _buttonClickedSubscription?.Dispose();
        }
    }
}
