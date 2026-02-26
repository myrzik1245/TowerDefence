using _Project.Code.Runtime.Configs.Explosion;
using _Project.Code.Runtime.Configs.Health;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Characters
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Configs/Characters/TowerConfig")]
    public class TowerConfig : CharacterConfig
    {
        [field: SerializeField] public HealthConfigData HealthConfigData { get; private set; }
        [field: SerializeField] public ExplosionConfig ExplosionConfig { get; private set; }

        private void OnValidate()
        {
            HealthConfigData.OnValidate();
        }
    }
}
