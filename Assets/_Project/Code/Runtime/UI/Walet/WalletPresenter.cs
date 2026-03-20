using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WalletFeature.Slot;
using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Walet.Slot;
using System.Collections.Generic;

namespace _Project.Code.Runtime.UI.Walet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletView _view;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly IReadOnlyWallet _wallet;
        
        private Dictionary<WalletSlotPresenter, WalletSlotView> _slotsMap =  new();
        
        public WalletPresenter(WalletView view, ProjectPresentersFactory presentersFactory, ViewsFactory viewsFactory, IReadOnlyWallet wallet)
        {
            _view = view;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _wallet = wallet;

        }

        public void Initialize()
        {
            foreach (IReadOnlyWalletSlot slot in _wallet.Slots)
            {
                WalletSlotView view = _viewsFactory.Create<WalletSlotView>(ViewIDs.WalletSlot);
                _view.Add(view);
                
                WalletSlotPresenter presenter = _presentersFactory.CreateWalletSlotPresenter(slot, view);
                
                _slotsMap.Add(presenter, view);
                
                presenter.Initialize();
            }
        }
        
        public void Dispose()
        {
            foreach (KeyValuePair<WalletSlotPresenter, WalletSlotView> slots in _slotsMap)
            {
                slots.Key.Dispose();
                _viewsFactory.Release(slots.Value);
            }
        }
    }
}
