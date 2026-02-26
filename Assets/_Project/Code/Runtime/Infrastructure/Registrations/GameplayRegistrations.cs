using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.SpawnerFeature;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Utility.DI;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class GameplayRegistrations
    {
        public static void Register(DIContainer gameplayContainer)
        {
            gameplayContainer.Register(CreateCharactersFactory).AsSingle();
            gameplayContainer.Register(CreateExplosionsFactory).AsSingle();
            gameplayContainer.Register(CreateAttackFactory).AsSingle();
            gameplayContainer.Register(CreateStageFactory).AsSingle();
            gameplayContainer.Register(CreateSpawner).AsSingle();

            gameplayContainer.Initialize();
        }

        private static SpawnersFactory CreateSpawner(DIContainer c)
        {
            return new SpawnersFactory(c);
        }
        
        private static StageFactory CreateStageFactory(DIContainer c)
        {
            return new StageFactory(c);
        }
        
        private static AttackFactory CreateAttackFactory(DIContainer c)
        {
            return new AttackFactory(c);
        }

        private static ExplosionsFactory CreateExplosionsFactory(DIContainer c)
        {
            return new ExplosionsFactory(c);
        }

        private static CharactersFactory CreateCharactersFactory(DIContainer c)
        {
            return new CharactersFactory(c);
        }
    }
}
