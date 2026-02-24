namespace _Project.Code.Runtime.Utility.Update
{
    public interface IUpdate : IUpdatable
    {
        void Update(float deltaTime);
    }
}
