using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Utility.DataManagment;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Data.Player
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        public PlayerDataProvider(ISaveLoadSerivce saveLoadService) : base(saveLoadService)
        {
        }
        
        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = new Dictionary<CurrencyType, int>()
                {
                    {CurrencyType.Soft, 100},
                    {CurrencyType.Hard, 50},
                },
                
                WinCount = 0,
                LoseCount = 0,
            };
        }
    }
}
