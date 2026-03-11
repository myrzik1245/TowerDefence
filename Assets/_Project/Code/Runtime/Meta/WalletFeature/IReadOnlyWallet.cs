using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Meta.WalletFeature
{
    public interface IReadOnlyWallet
    {
        IReadOnlyReactiveVariable<int> Balance { get; }
    }
}
