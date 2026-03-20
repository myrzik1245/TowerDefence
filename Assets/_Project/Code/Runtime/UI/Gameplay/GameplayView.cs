using _Project.Code.Runtime.UI.Core;
using _Project.Code.Runtime.UI.Walet;
using UnityEngine;

namespace _Project.Code.Runtime.UI.Gameplay
{
    public class GameplayView : MonoBehaviour, IView
    {
        [field: SerializeField] public WalletView WalletView { get; private set; }
    }
}
