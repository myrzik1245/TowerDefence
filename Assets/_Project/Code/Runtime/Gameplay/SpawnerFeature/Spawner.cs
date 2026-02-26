using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.PositionRandomizer;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.SpawnerFeature
{
    public class Spawner
    {
        private readonly IPositionRandomizer _positionRandomizer;
        private readonly CharactersFactory _charactersFactory;
        
        public Spawner(IPositionRandomizer positionRandomizer, CharactersFactory charactersFactory)
        {
            _positionRandomizer = positionRandomizer;
            _charactersFactory = charactersFactory;
        }

        public List<GameObject> Spawn(IEnumerable<CharacterConfig> configs, TeamsType teamType)
        {
            List<GameObject> spawnedEnemies = new();
            
            foreach (CharacterConfig config in configs)
            {
                Vector3 spawnPosition = _positionRandomizer.GetRandomPosition(Vector3.up);
                GameObject instance = _charactersFactory.Create(config, spawnPosition, teamType);
                spawnedEnemies.Add(instance);
            }
            
            return spawnedEnemies;
        }
    }
}
