using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Configs.Shop;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Defence
{
    [CreateAssetMenu(fileName = "PuddleConfig", menuName = "Configs/DefenceObjects/PuddleConfig")]
    
    public class PuddleConfig : ShopItem
    {
        [field: SerializeField] public ExplosionConfig Explosion { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}