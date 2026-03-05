using _Project.Code.Runtime.Utility.StateMachineCore;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.GameLoop
{
    public class GameLoopStateMachine : StateMachine<IUpdatableState>
    {
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            CurrentState?.Update(deltaTime);
        }
    }
}
