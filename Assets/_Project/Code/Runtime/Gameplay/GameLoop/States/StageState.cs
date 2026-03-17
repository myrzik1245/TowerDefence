using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class StageState : State, IUpdatableState
    {
        private readonly StageService _stageService;
        private readonly BrainsContext _brainsContext;
        
        public StageState(StageService stageService, BrainsContext brainsContext)
        {
            _stageService = stageService;
            _brainsContext = brainsContext;
        }

        public override void Enter()
        {
            if (_stageService.HasNextStage())
                _stageService.SwitchToNext();
        }

        public override void Exit()
        {
            _stageService.CleanUp();
        }
        
        public void Update(float deltaTime)
        {
            _brainsContext.Update(deltaTime);
            _stageService.Update(deltaTime);
        }
    }
}
