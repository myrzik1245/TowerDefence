using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Gameplay.HealthFeature;
using _Project.Code.Runtime.Gameplay.SpawnerFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public class KillAllEnemyStage : IStage
    {
        private readonly KillAllEnemyStageConfig _config; 
        private readonly Spawner _spawner;
        private readonly ReactiveEvent _completed = new();
        private readonly Dictionary<GameObject, IDisposable> _enemyToSubscribe = new();

        private bool _isRun;

        public KillAllEnemyStage(KillAllEnemyStageConfig config, Spawner spawner)
        {
            _config = config;
            _spawner = spawner;
        }

        public IReadOnlyReactiveEvent Compleated => _completed;

        public void Enter()
        {
            List<GameObject> spawnedEnemies = _spawner.Spawn(_config.Enemies, TeamsType.Enemy);    
            
            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy.TryGetComponent(out IAlive alive))
                {
                    IDisposable isDeathSubscribe = alive.IsDead.Subscribe(isDead => {
                        if (isDead)
                        {
                            _enemyToSubscribe[enemy].Dispose();
                            _enemyToSubscribe.Remove(enemy);
                        }
                    });

                    _enemyToSubscribe.Add(enemy, isDeathSubscribe);
                }
            }

            _isRun = true;
        }

        public void Exit()
        {
            foreach (KeyValuePair<GameObject, IDisposable> enemy in _enemyToSubscribe)
            {
                Object.Destroy(enemy.Key);
                enemy.Value.Dispose();
            }

            _enemyToSubscribe.Clear();

            _isRun = false;
        }

        public void Update(float deltaTime)
        {
            if (_isRun == false)
                return;

            if (_enemyToSubscribe.Count <= 0)
            {
                _isRun = false;
                _completed.Invoke();
            }
        }

        public void Dispose()
        {
            foreach (KeyValuePair<GameObject, IDisposable> enemy in _enemyToSubscribe)
                enemy.Value.Dispose();

            _isRun = false;
        }
    }
}
