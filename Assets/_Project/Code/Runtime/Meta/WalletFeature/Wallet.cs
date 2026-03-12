using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Meta.WalletFeature
{
    public class Wallet : IReadOnlyWallet, IDataWriter<PlayerData>, IDataReader<PlayerData>
    {
        private readonly ReactiveVariable<int> _balance = new();

        public Wallet(PlayerDataProvider playerData)
        {
            playerData.RegisterWriter(this);
            playerData.RegisterReader(this);
        }
        
        public IReadOnlyReactiveVariable<int> Balance => _balance;

        public bool Enough(int amount)
        {
            return _balance.Value - amount >= 0;
        }
        
        public void Spend(int amount)
        {
            if (Enough(amount) == false)
                throw new InvalidOperationException();
            
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            _balance.Value -= amount;
        }

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            _balance.Value += amount;
        }
        
        public void WriteTo(PlayerData data)
        {
            data.Balance = _balance.Value;
        }
        
        public void ReadFrom(PlayerData data)
        {
            _balance.Value = data.Balance;
        }
    }
}
