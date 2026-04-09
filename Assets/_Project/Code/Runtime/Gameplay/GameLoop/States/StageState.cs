using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class StageState : State, IUpdatableState
    {
        private readonly StageService _stageService;
        private readonly BrainsContext _brainsContext;
        private readonly DefenceObjectsFactory _defenceObjectsFactory;
        
        public StageState(StageService stageService, BrainsContext brainsContext, DefenceObjectsFactory defenceObjectsFactory)
        {
            _stageService = stageService;
            _brainsContext = brainsContext;
            _defenceObjectsFactory = defenceObjectsFactory;
        }

        public override void Enter()
        {
            if (_stageService.HasNextStage())
                _stageService.SwitchToNext();
        }

        public override void Exit()
        {
            _stageService.CleanUp();
            _defenceObjectsFactory.Clear();
        }
        
        public void Update(float deltaTime)
        {
            _brainsContext.Update(deltaTime);
            _stageService.Update(deltaTime);
        }
    }
}
