using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.AI.States
{
    public class AIParallelState : ParallelState<IUpdatableState>, IUpdatableState
    {
        public AIParallelState(params IUpdatableState[] states) : base(states)
        {
        }
        
        public void Update(float deltaTime)
        {
            foreach (IUpdatableState state in States)
                state.Update(deltaTime);
        }
    }
}
