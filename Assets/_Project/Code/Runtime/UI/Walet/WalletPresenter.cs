using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.UI.CommonViews;
using _Project.Code.Runtime.UI.Core;
using System;

namespace _Project.Code.Runtime.UI.Walet
{
    public class WalletPresenter : IPresenter
    {
        private readonly Wallet _wallet;
        private readonly NumberView _view;
        
        private IDisposable _disposable;
        
        public WalletPresenter(Wallet wallet, NumberView view)
        {
            _wallet = wallet;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetNumber(_wallet.Balance.Value);
            _disposable = _wallet.Balance.Subscribe(_view.SetNumber);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
