using _Project.Code.Runtime.Meta.WalletFeature;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Wallet
{
    [CreateAssetMenu(fileName = "WalletConfig", menuName = "Configs/Wallet")]
    public class WalletConfig : ScriptableObject
    {
        [field: SerializeField] public CurrencyType Currency { get; private set; }
        [field: SerializeField] public int StartAmount { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
