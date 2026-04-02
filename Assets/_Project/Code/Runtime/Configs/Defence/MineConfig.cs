using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Configs.Shop;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Defence
{
    [CreateAssetMenu(fileName = "MineConfig", menuName = "Configs/DefenceObjects/MineConfig")]
    public class MineConfig : ShopItem
    {
        [field: SerializeField] public ExplosionConfig Explosion { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
    }
}
