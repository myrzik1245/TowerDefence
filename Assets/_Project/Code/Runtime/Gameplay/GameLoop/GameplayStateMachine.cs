using _Project.Code.Runtime.Utility.StateMachineCore;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using _Project.Code.Runtime.Utility.Update;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.GameLoop
{
    public class GameplayStateMachine : StateMachine<IUpdatableState>, IUpdatableState, IUpdate
    {
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
           
            CurrentState?.Update(deltaTime);
        }
    }
}
