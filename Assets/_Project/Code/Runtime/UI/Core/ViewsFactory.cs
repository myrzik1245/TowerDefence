using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.DI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.UI.Core
{
    public class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<string, string> _pathsMap = new Dictionary<string, string>()
        {
            { ViewIDs.MainMenu, "UI/MainMenu/MainMenuView" },
            { ViewIDs.Gameplay, "UI/Gameplay/GameplayView" },
            { ViewIDs.ConfirmPopup, "UI/Popups/ConfirmPopup" },
            { ViewIDs.ContinuePopup, "UI/Popups/ContinuePopup" },
            { ViewIDs.EndGamePopup, "UI/Popups/EndGamePopup" },
            { ViewIDs.WalletSlot, "UI/Wallet/WalletSlotView" },
            { ViewIDs.Wallet, "UI/Wallet/WalletView" },
        };

        public ViewsFactory(DIContainer container)
        {
            _resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
        }

        public TView Create<TView>(string viewId, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_pathsMap.TryGetValue(viewId, out var path))
            {
                TView prefab = _resourcesAssetsLoader.Load<TView>(path);
                TView instance = Object.Instantiate(prefab, parent);

                return instance;
            }

            throw new ArgumentException($"View with id {nameof(viewId)} not found");
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}
