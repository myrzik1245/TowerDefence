using _Project.Code.Runtime.Configs.Wallet;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WalletFeature.Slot;
using _Project.Code.Runtime.UI.CommonPopups.ConfirmPopup;
using _Project.Code.Runtime.UI.MainMenu;
using _Project.Code.Runtime.UI.Walet;
using _Project.Code.Runtime.UI.Walet.Slot;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using System;

namespace _Project.Code.Runtime.UI.Factories.Presenters
{
    public class ProjectPresentersFactory
    {
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly Wallet _wallet;
        private readonly ConfigsProvider _configsProvider;
        private readonly ViewsFactory _viewsFactory;

        public ProjectPresentersFactory(DIContainer projectContainer)
        {
            _coroutinePerformer = projectContainer.Resolve<ICoroutinePerformer>();
            _wallet = projectContainer.Resolve<Wallet>();
            _configsProvider = projectContainer.Resolve<ConfigsProvider>();
            _viewsFactory = projectContainer.Resolve<ViewsFactory>();
        }

        public ConfirmPopupPresenter CreateConfirmPopupPresenter(
            ConfirmPopupView view,
            string title,
            string message,
            Action onConfirm,
            Action onCancel = null)
        {
            return new ConfirmPopupPresenter(
                _coroutinePerformer,
                view,
                title,
                message,
                onConfirm,
                onCancel);
        }

        public WalletSlotPresenter CreateWalletSlotPresenter(
            IReadOnlyWalletSlot slot,
            WalletSlotView view)
        {
            return new WalletSlotPresenter(
                slot,
                view,
                _configsProvider.GetConfig<WalletConfig>());
        }

        public WalletPresenter CreateWalletPresenter(WalletView view)
        {
            return new WalletPresenter(
                view,
                this,
                _viewsFactory,
                _wallet);
        }
    }
}
