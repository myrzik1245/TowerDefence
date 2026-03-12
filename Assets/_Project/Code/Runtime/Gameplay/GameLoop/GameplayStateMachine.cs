using _Project.Code.Runtime.Utility.StateMachineCore;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using _Project.Code.Runtime.Utility.Update;

namespace _Project.Code.Runtime.Gameplay.GameLoop
{
    public class GameplayStateMachine : StateMachine<IUpdatableState>, IUpdatableState, IUpdate
    {
        public override void Update(float deltaTime)
        {
            CurrentState?.Update(deltaTime);
            
            base.Update(deltaTime);
        }
    }
}
