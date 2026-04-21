using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Shop
{
    [CreateAssetMenu(menuName = "Configs/ShopConfig", fileName = "ShopConfig", order = 0)]
    
    public class ShopConfig : ScriptableObject
    {
        [SerializeField] private ShopItemConfig[] _items;
        
        public IReadOnlyList<ShopItemConfig> Items => _items;
    }
}
