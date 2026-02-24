namespace _Project.Code.Runtime.Utility.Update
{
    public interface IFixedUpdate : IUpdatable
    {
        void FixedUpdate(float deltaTime);
    }
}
