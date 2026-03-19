using _Project.Code.Runtime.Configs;
using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class WinState : EndGameState
    {
        private readonly Wallet _wallet;
        private readonly ConfigsProvider _configsProvider;
        private readonly WinLoseCounter _winLoseCounter;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinePerformer _coroutinePerformer;


        public WinState(
            GameplayPopupService gameplayPopupService,
            string popupTitle,
            Wallet wallet,
            ConfigsProvider configsProvider,
            WinLoseCounter winLoseCounter,
            PlayerDataProvider playerDataProvider,
            ICoroutinePerformer coroutinePerformer) : base(gameplayPopupService, popupTitle)
        {
            _wallet = wallet;
            _configsProvider = configsProvider;
            _winLoseCounter = winLoseCounter;
            _playerDataProvider = playerDataProvider;
            _coroutinePerformer = coroutinePerformer;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _wallet.Add(CurrencyType.Soft, _configsProvider.GetConfig<BonusConfig>().WinBonus);
            _winLoseCounter.AddWin();
            
            _coroutinePerformer.StartPerform(_playerDataProvider.Save());
        }
    }
}
