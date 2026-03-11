using System;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Shop
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "Configs/ShopConfig")]
    public class ShopConfig : ScriptableObject
    {
        [SerializeField] private ItemToPrice[] _itemsPrices;

        public int GetPrice(ShopItem shopItem)
        {
            return _itemsPrices.First(item => item.ShopItem == shopItem).Price;
        }
        
        [Serializable]
        private class ItemToPrice
        {
            [field: SerializeField] public ShopItem ShopItem { get; private set; }
            [field: SerializeField] public int Price { get; private set; }
        }
    }
}
