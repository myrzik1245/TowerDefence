using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.Enemy;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.PositionRandomizer;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.SpawnerFeature
{
    public class RadiusSpawner
    {
        private readonly IPositionRandomizer _positionRandomizer;
        private readonly EnemiesFactory _enemiesFactory;
        
        public RadiusSpawner(IPositionRandomizer positionRandomizer, EnemiesFactory enemiesFactory)
        {
            _positionRandomizer = positionRandomizer;
            _enemiesFactory = enemiesFactory;
        }

        public List<ICharacter> Spawn(IEnumerable<CharacterConfig> configs, TeamsType teamType)
        {
            List<ICharacter> spawnedEnemies = new();
            
            foreach (CharacterConfig config in configs)
            {
                Vector3 spawnPosition = _positionRandomizer.GetRandomPosition(Vector3.up);
                
                ICharacter instance = _enemiesFactory.Create(config, spawnPosition);
                
                spawnedEnemies.Add(instance);
            }
            
            return spawnedEnemies;
        }
    }
}
