using _Project.Code.Runtime.Configs.Characters;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Stages
{
    [CreateAssetMenu(fileName = "KillAllEnemyStageConfig", menuName = "Configs/Stages/KillAllEnemyStageConfig")]
    public class KillAllEnemyStageConfig : StageConfig
    {
        [field: SerializeField] public CharacterConfig[] Enemies { get; private set; }
        [field: SerializeField, Min(0.1f)] public float SpawnRange { get; private set; } 
    }
}
