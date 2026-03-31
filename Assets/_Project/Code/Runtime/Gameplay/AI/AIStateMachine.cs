using System;
using _Project.Code.Runtime.Utility.StateMachineCore;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.AI
{
    public class AIStateMachine : StateMachine<IUpdatableState>, IUpdatableState
    {
        public AIStateMachine(params IDisposable[] disposables) : base(disposables)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            CurrentState?.Update(deltaTime);
        }
    }
}
