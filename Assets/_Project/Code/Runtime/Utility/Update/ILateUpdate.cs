namespace _Project.Code.Runtime.Utility.Update
{
    public interface ILateUpdate : IUpdatable
    {
        void LateUpdate(float deltaTime);
    }
}
