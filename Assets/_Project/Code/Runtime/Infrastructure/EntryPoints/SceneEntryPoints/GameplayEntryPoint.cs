using _Project.Code.MainHero;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Infrastructure.Registrations;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;
using _Project.Code.Runtime.Utility.Update;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Code.Runtime.Infrastructure.EntryPoints.SceneEntryPoints
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        private IUpdatableService _updatableService;
        private BrainsContext _brainsContext;

        public override IEnumerator Initialize(DIContainer container, IInputSceneArgs inputSceneArgs)
        {
            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"Input args must be of type {nameof(GameplayInputArgs)}");
            
            GameplayRegistrations.Register(container, gameplayInputArgs);

            _updatableService = container.Resolve<IUpdatableService>();
            _brainsContext = container.Resolve<BrainsContext>();
            
            _updatableService.AddRequest(_brainsContext);
            
            StageService stageService = container.Resolve<StageService>();
            MainHeroFactory mainHeroFactory = container.Resolve<MainHeroFactory>();
            
            mainHeroFactory.CreateTower(Vector3.zero);
            
            stageService.SwitchToNext();
            stageService.Start();
            
            stageService.Stage.Compleated.Subscribe(() => 
            {
                stageService.SwitchToNext(); 
                stageService.Start();
                Debug.Log("Compleated");
            });
            
            yield break;
        }

        public override void Run()
        {
        }


        private void OnDestroy()
        {
            _updatableService.RemoveRequest(_brainsContext);
        }
    }
}
