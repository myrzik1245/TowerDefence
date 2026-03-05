namespace _Project.Code.Runtime.Gameplay.AI.Brains
{
    public class StateMachineBrain : IBrain
    {
        private readonly AIStateMachine _stateMachine;
        private bool _isActive;
        
        public StateMachineBrain(AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enable()
        {
            _stateMachine.Enter();
            _isActive =  true;
        }
        
        public void Disable()
        {
            _stateMachine.Exit();
            _isActive = false;
        }
        
        public void Update(float deltaTime)
        {
            if (_isActive)
                _stateMachine.Update(deltaTime);
        }
        
        public void Dispose()
        {
            _isActive = false;
            _stateMachine.Dispose();
        }
    }
}
