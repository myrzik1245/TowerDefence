using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Meta.WalletFeature.Slot
{
    public class WalletSlot : IReadOnlyWalletSlot, IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private ReactiveVariable<int> _amount = new();
        
        public CurrencyType CurrencyType { get; }
        public IReadOnlyReactiveVariable<int> Amount => _amount;

        public WalletSlot(CurrencyType currencyType, PlayerDataProvider playerDataProvider)
        {
            CurrencyType = currencyType;
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public bool Enough(int amount)
        {
            return _amount.Value - amount >= 0;
        }

        public void Spend(int amount)
        {
            if (Enough(amount) == false)
                throw new InvalidOperationException();
            
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            _amount.Value -= amount;
        }

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            _amount.Value += amount;
        }
        
        public void ReadFrom(PlayerData data)
        {
            _amount.Value = data.WalletData[CurrencyType];
        }
        
        public void WriteTo(PlayerData data)
        {
            data.WalletData[CurrencyType] = _amount.Value;
        }
    }
}
