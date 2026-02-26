using _Project.Code.Runtime.Configs.Common;
using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Configs.Health;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Characters
{
    [CreateAssetMenu(fileName = "BomberConfig", menuName = "Configs/Characters/BomberConfig", order = 0)]
    public class BomberConfig : CharacterConfig
    {
        [field: SerializeField] public HealthConfigData HealthConfigData { get; private set; }
        [field: SerializeField] public MovementConfigData MovementConfigData { get; private set; }
        [field: SerializeField] public RotatorConfigData RotatorConfigData { get; private set; }
        [field: SerializeField] public ExplosionConfig ExplosionConfig { get; private set; }

        private void OnValidate()
        {
            HealthConfigData.OnValidate();
        }
    }
}
