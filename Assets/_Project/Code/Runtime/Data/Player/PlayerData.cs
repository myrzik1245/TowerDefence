using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Utility.DataManagment;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Data.Player
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyType, int> WalletData;
        public int WinCount;
        public int LoseCount;
    }
}
