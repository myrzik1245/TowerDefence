using _Project.Code.Runtime.Gameplay.DefenceFeature;
using System;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Shop
{
    [CreateAssetMenu(fileName = "DefenceObjectShopConfig", menuName = "Configs/DefenceObjectShopConfig")]
    public class DefenceObjectShopConfig : ScriptableObject
    {
        [SerializeField] private ItemData[] _items;

        public int GetPrice(DefenceObjectTypes type)
        {
            return _items.First(item => item.Type == type).Price;
        }

        public Sprite GetIcon(DefenceObjectTypes type)
        {
            return _items.First(item => item.Type == type).Icon;
        }
        
        [Serializable]
        private class ItemData
        {
            [field: SerializeField] public DefenceObjectTypes Type { get; private set; }
            [field: SerializeField] public int Price { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }
        }
    }
}
