using _Project.Code.Runtime.Utility.Reactive.Variable;

namespace _Project.Code.Runtime.Meta.WalletFeature.Slot
{
    public interface IReadOnlyWalletSlot
    {
        public CurrencyType CurrencyType { get; }
        public IReadOnlyReactiveVariable<int> Amount { get; }
    }
}
