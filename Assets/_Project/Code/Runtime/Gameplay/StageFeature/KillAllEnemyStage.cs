using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.SpawnerFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public class KillAllEnemyStage : IStage
    {
        private readonly KillAllEnemyStageConfig _config;
        private readonly RadiusSpawner _radiusSpawner;
        private readonly ReactiveEvent _completed = new();
        private readonly Dictionary<ICharacter, IDisposable> _enemyToSubscribe = new();

        public KillAllEnemyStage(KillAllEnemyStageConfig config, RadiusSpawner radiusSpawner)
        {
            _config = config;
            _radiusSpawner = radiusSpawner;
        }

        public IReadOnlyReactiveEvent Compleated => _completed;

        public void Start()
        {
            List<ICharacter> spawnedEnemies = _radiusSpawner.Spawn(_config.Enemies, TeamsType.Enemy);

            foreach (ICharacter enemy in spawnedEnemies)
            {
                IDisposable isDeathSubscribe = enemy.IsDead.Subscribe(isDead => {
                    if (isDead)
                    {
                        _enemyToSubscribe[enemy].Dispose();
                        _enemyToSubscribe.Remove(enemy);

                        if (_enemyToSubscribe.Count <= 0)
                            _completed.Invoke();
                    }
                });

                _enemyToSubscribe.Add(enemy, isDeathSubscribe);
            }
        }

        public void CleanUp()
        {
            foreach (KeyValuePair<ICharacter, IDisposable> enemy in _enemyToSubscribe)
                enemy.Value.Dispose();

            _enemyToSubscribe.Clear();
        }

        public void Update(float deltaTime)
        {
        }

        public void Dispose()
        {
            foreach (KeyValuePair<ICharacter, IDisposable> enemy in _enemyToSubscribe)
                enemy.Value.Dispose();
        }
    }
}
