namespace _Project.Code.Runtime.Utility.Update
{
    public interface IUpdatableService
    {
        void AddRequest(IUpdatable updatable);
        void RemoveRequest(IUpdatable updatable);
    }
}
