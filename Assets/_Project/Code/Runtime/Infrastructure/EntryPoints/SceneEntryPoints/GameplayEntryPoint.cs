using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Infrastructure.Registrations;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.EntryPoints.SceneEntryPoints
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        private IStage _stage;
        private GameplayInputArgs _inputArgs;
        private Tower _player;
        
        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs inputSceneArgs)
        {
            GameplayRegistrations.Register(container);
            
            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"Input args must be of type {nameof(GameplayInputArgs)}");

            _inputArgs = gameplayInputArgs;
            
            StageFactory stageFactory = container.Resolve<StageFactory>();
            ConfigsProvider configsProvider = container.Resolve<ConfigsProvider>();
            LevelsConfig levelsConfig = configsProvider.GetConfig<LevelsConfig>();
            LevelConfig levelConfig = levelsConfig.Levels[_inputArgs.Level];
            CharactersFactory charactersFactory = container.Resolve<CharactersFactory>();
            
            _player = charactersFactory.CreateTower(levelConfig.TowerConfig, Vector3.zero, TeamsType.Player);
            _stage = stageFactory.Create(levelConfig.Stages[0]);
            _stage.Compleated.Subscribe(() => Debug.Log("Completed"));
                
            yield break;
        }

        public override void Run()
        {
            _stage.Enter();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                
                if (groundPlane.Raycast(ray, out float rayDistance))
                {
                    Vector3 worldPoint = ray.GetPoint(rayDistance);
                    _player.Attack(worldPoint);
                }
            }
        }
    }
}
