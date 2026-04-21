using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using System;

namespace _Project.Code.Runtime.Meta.ShopFeature
{
    public class ShopItem
    {
        private readonly CurrencyType _currencyType;
        private readonly int _price;
        private readonly Wallet _wallet;
        private readonly ReactiveEvent<ShopItem> _buyed = new();

        public string ID { get; }
        public IReadOnlyReactiveEvent<ShopItem> Buyed => _buyed;

        public ShopItem(CurrencyType currencyType, int price, Wallet wallet, string id)
        {
            _currencyType = currencyType;
            _price = price;
            _wallet = wallet;
            ID = id;
        }

        public bool CanBuy()
        {
            return _wallet.Enough(_currencyType, _price);
        }
        
        public void Buy()
        {
            if (CanBuy() == false)
                throw new InvalidOperationException();
            
            _wallet.Spend(_currencyType, _price);
            _buyed.Invoke(this);
        }
    }
}
