using _Project.Code.Runtime.Configs.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private StageConfig[] _stages;
        
        [field: SerializeField] public TowerConfig TowerConfig { get; private set; }
        
        public IReadOnlyList<StageConfig> Stages => _stages;
        
        public StageConfig GetStageByIndex(int index) => Stages[index];
        
        public bool HasStages(int index) => index < _stages.Length;
    }
}
