using _Project.Code.Runtime.Gameplay.Enemy;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.PositionRandomizer;

namespace _Project.Code.Runtime.Gameplay.SpawnerFeature
{
    public class SpawnersFactory
    {
        private readonly EnemiesFactory _enemiesFactory;
        
        public SpawnersFactory(DIContainer container)
        {
            _enemiesFactory = container.Resolve<EnemiesFactory>();
        }
        
        public RadiusSpawner CreateRadiusSpawner(float radius)
        {
            return new RadiusSpawner(new RadiusPositionRandomizer(radius), _enemiesFactory);
        }
    }
}
