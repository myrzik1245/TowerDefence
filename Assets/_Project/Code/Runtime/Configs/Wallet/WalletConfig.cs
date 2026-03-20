using _Project.Code.Runtime.Meta.WalletFeature;
using System;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Wallet
{
    [CreateAssetMenu(fileName = "WalletConfig", menuName = "Configs/Wallet")]
    public class WalletConfig : ScriptableObject
    {
        [SerializeField] private CurrencyData[] _currencyData;

        public int GetStartAmount(CurrencyType currencyType)
        {
            return _currencyData.First(data => data.Currency == currencyType).StartAmount;
        }

        public Sprite GetIcon(CurrencyType currencyType)
        {
            return _currencyData.First(data => data.Currency == currencyType).Icon;
        }
        
        [Serializable]
        private class CurrencyData
        {
            [field: SerializeField] public CurrencyType Currency { get; private set; }
            [field: SerializeField] public int StartAmount { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }
        }
    }
}
