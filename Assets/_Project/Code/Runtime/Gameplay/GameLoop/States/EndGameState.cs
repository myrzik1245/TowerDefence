using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public abstract class EndGameState : State, IUpdatableState
    {
        private readonly GameplayPopupService _gameplayPopupService;
        private readonly string _popupTitle;

        protected EndGameState(GameplayPopupService gameplayPopupService, string popupTitle)
        {
            _gameplayPopupService = gameplayPopupService;
            _popupTitle = popupTitle;
        }

        public override void Enter()
        {
            base.Enter();

            _gameplayPopupService.OpenEndGamePopup(_popupTitle);
        }
        
        public void Update(float deltaTime)
        {
        }
    }
}
