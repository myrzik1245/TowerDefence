using _Project.Code.Runtime.Meta.WalletFeature.Slot;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Meta.WalletFeature
{
    public interface IReadOnlyWallet
    {
        IReadOnlyList<IReadOnlyWalletSlot> Slots { get; }
    }
}
