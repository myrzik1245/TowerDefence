namespace _Project.Code.Runtime.Utility.DI
{
    public interface IDIOptions
    {
        IDIOptions NonLazy();
        IDIOptions AsSingle();
    }
}
