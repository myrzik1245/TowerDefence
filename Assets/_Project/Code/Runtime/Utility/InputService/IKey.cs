namespace _Project.Code.Runtime.Utility.InputService
{
    public interface IKey
    {
        bool Down { get; }
        bool Up { get; }
        bool Pressing { get; }
    }
}
