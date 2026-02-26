using UnityEngine;

namespace _Project.Code.Runtime.Configs.Explosion
{
    [CreateAssetMenu(fileName = "DamageExplosionConfig", menuName = "Configs/Explosions/DamageExplosionConfig")]
    public class DamageExplosionConfig : ExplosionConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}
