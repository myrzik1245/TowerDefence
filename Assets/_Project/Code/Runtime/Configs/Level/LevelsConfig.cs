using UnityEngine;

namespace _Project.Code.Runtime.Configs.Level
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Level/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [field: SerializeField] public LevelConfig[] Levels { get; private set; }
    }
}
