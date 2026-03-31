using _Project.Code.Runtime.Configs.Common;
using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Configs.Health;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Characters
{
    [CreateAssetMenu(fileName = "ShooterConfig",  menuName = "Configs/Characters/ShooterConfig")]
    public class ShooterConfig : CharacterConfig
    {
        [field: SerializeField] public HealthConfigData HealthConfigData { get; private set; }
        [field: SerializeField] public ExplosionConfig ExplosionConfig { get; private set; }
        [field: SerializeField] public MovementConfigData MovementConfigData { get; private set; }
        [field: SerializeField] public RotatorConfigData RotatorConfigData { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }

        private void OnValidate()
        {
            HealthConfigData.OnValidate();
        }
    }
}
