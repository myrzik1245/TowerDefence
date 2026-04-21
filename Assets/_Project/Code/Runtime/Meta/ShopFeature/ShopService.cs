using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;
using _Project.Code.Runtime.Utility.Reactive.List;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Meta.ShopFeature
{
    public class ShopService : IDisposable, IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Wallet _wallet;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ShopConfig _config;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ReactiveList<ShopItem> _availableItems = new();
        private readonly List<string> _purchasedItemIds = new();
        private readonly List<IDisposable> _itemBuyedSubscriptions = new();
        
        public ShopService(
            Wallet wallet,
            PlayerDataProvider playerDataProvider,
            ShopConfig config,
            ICoroutinePerformer coroutinePerformer)
        {
            _wallet = wallet;
            _playerDataProvider = playerDataProvider;
            _config = config;
            _coroutinePerformer = coroutinePerformer;

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }
        
        public IReadOnlyReactiveList<ShopItem> AvailableItems => _availableItems;
        
        public void Dispose()
        {
            foreach (IDisposable disposable in _itemBuyedSubscriptions)
                disposable.Dispose();
            
            _itemBuyedSubscriptions.Clear();
        }
        
        public void ReadFrom(PlayerData data)
        {
            _purchasedItemIds.Clear();
            
            foreach (string id in data.PurchasedItemsIds)
                _purchasedItemIds.Add(id);
            
            _availableItems.Clear();

            foreach (var itemConfig in _config.Items)
            {
                if (_purchasedItemIds.Contains(itemConfig.ID) == false)
                {
                    ShopItem shopItem = new(itemConfig.CurrencyType, itemConfig.Price, _wallet, itemConfig.ID);
                    _availableItems.Add(shopItem);
                    
                    _itemBuyedSubscriptions.Add(shopItem.Buyed.Subscribe(OnItemBuyed));
                }
            }
        }
        
        public void WriteTo(PlayerData data)
        {
            data.PurchasedItemsIds.Clear();
            data.PurchasedItemsIds.AddRange(_purchasedItemIds);
        }

        private void OnItemBuyed(ShopItem item)
        {
            _purchasedItemIds.Add(item.ID);
            _coroutinePerformer.StartPerform(_playerDataProvider.Save());
            
            _availableItems.Remove(item);
        }
    }
}
