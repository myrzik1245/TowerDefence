using _Project.Code.Runtime.Configs;
using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.SceneManagment;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;

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
            LoadSceneService loadSceneService,
            ICoroutinePerformer coroutinePerformer,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            Wallet wallet,
            ConfigsProvider configsProvider,
            WinLoseCounter winLoseCounter) : base(loadSceneService, coroutinePerformer, gameplayInputArgs)
        {
            _wallet = wallet;
            _configsProvider = configsProvider;
            _winLoseCounter = winLoseCounter;
            _playerDataProvider =  playerDataProvider;
            _coroutinePerformer = coroutinePerformer;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _wallet.Add(_configsProvider.GetConfig<BonusConfig>().WinBonus);
            _winLoseCounter.AddWin();
            
            _coroutinePerformer.StartPerform(_playerDataProvider.Save());
        }
    }
}
