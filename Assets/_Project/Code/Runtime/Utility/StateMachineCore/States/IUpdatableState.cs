namespace _Project.Code.Runtime.Utility.StateMachineCore.States
{
    public interface IUpdatableState : IState
    {
        void Update(float deltaTime);
    }
}
