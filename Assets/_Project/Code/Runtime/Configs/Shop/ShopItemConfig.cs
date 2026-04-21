using _Project.Code.Runtime.Meta.WalletFeature;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Shop
{
    [CreateAssetMenu(menuName = "Configs/ShopItemConfig", fileName = "ShopItemConfig", order = 0)]
    public class ShopItemConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public CurrencyType CurrencyType { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
