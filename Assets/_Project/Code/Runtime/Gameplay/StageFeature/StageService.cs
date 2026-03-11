using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using System;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public class StageService : IDisposable
    {
        private readonly StagesFactory _stagesFactory;
        private readonly LevelConfig _levelConfig;
        private ReactiveVariable<int> _stageIndex;
        
        public StageService(StagesFactory stagesFactory, LevelConfig levelConfig)
        {
            _stagesFactory = stagesFactory;
            _levelConfig = levelConfig;
            
            _stageIndex = new ReactiveVariable<int>();
        }

        public IStage Stage { get; private set; }
        public IReadOnlyReactiveVariable<bool> IsCompleate => Stage.IsCompleate;

        public bool HasNextStage()
        {
            return _stageIndex.Value + 1 <= _levelConfig.Stages.Count;
        }

        public void Update(float deltaTime)
        {
            Stage.Update(deltaTime);
        }
        
        public void SwitchToNext()
        {
            if (HasNextStage() == false)
                throw new InvalidOperationException("Cannot switch to next stage.");
            
            if (Stage != null)
                CleanUp();
            
            Stage = CreateStage();
            Stage.Start();
            
            _stageIndex.Value++;
        }

        public void CleanUp()
        {
            Stage.CleanUp();
        }
        
        public void Dispose()
        {
            CleanUp();
            Stage?.Dispose();
        }
        
        private IStage CreateStage()
        {
            return _stagesFactory.Create(_levelConfig.Stages[_stageIndex.Value]);
        }
    }
}
