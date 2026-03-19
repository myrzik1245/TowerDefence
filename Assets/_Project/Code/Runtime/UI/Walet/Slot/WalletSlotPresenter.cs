using _Project.Code.Runtime.Configs.Wallet;
using _Project.Code.Runtime.Meta.WalletFeature.Slot;
using _Project.Code.Runtime.UI.Core;
using System;

namespace _Project.Code.Runtime.UI.Walet.Slot
{
    public class WalletSlotPresenter : IPresenter
    {
        private readonly IReadOnlyWalletSlot _slot;
        private readonly WalletSlotView _view;
        private readonly WalletConfig _config;
            
        private IDisposable _subscription;
        
        public WalletSlotPresenter(IReadOnlyWalletSlot slot, WalletSlotView view, WalletConfig config)
        {
            _slot = slot;
            _view = view;
            _config = config;
        }

        public void Initialize()
        {
            _view.SetAmount(_slot.Amount.Value);
            _view.SetIcon(_config.Icon);
            _subscription = _slot.Amount.Subscribe(_view.SetAmount);
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
