using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Configs.Shop;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Defence
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "Configs/DefenceObjects/TurretConfig")]
    public class TurretConfig : ShopItem
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public ExplosionConfig ExplosionAttackConfig { get; private set; }
        [field: SerializeField] public float AttackTime { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}