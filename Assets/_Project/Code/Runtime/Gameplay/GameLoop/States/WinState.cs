using _Project.Code.Runtime.Configs;
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
        private WinLoseCounter _winLoseCounter;

        public WinState(
            LoadSceneService loadSceneService,
            ICoroutinePerformer coroutinePerformer,
            GameplayInputArgs gameplayInputArgs, Wallet wallet, ConfigsProvider configsProvider, WinLoseCounter winLoseCounter) : base(loadSceneService, coroutinePerformer, gameplayInputArgs)
        {
            _wallet = wallet;
            _configsProvider = configsProvider;
            _winLoseCounter = winLoseCounter;
        }

        public override void Enter()
        {
            _wallet.Add(_configsProvider.GetConfig<BonusConfig>().WinBonus);
            _winLoseCounter.AddWin();
        }
    }
}
