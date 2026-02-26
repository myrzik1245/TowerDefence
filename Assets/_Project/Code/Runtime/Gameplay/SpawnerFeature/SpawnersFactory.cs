using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.PositionRandomizer;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.SpawnerFeature
{
    public class SpawnersFactory
    {
        private readonly CharactersFactory _charactersFactory;
        
        public SpawnersFactory(DIContainer container)
        {
            _charactersFactory = container.Resolve<CharactersFactory>();
        }
        
        public Spawner CreateRadiusSpawner(float radius)
        {
            return new Spawner(new RadiusPositionRandomizer(radius), _charactersFactory);
        }
    }
}
