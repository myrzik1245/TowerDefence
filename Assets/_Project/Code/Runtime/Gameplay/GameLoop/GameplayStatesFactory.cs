using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.Gameplay.GameLoop.States;
using _Project.Code.Runtime.Gameplay.MainHero;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.Meta.WinLoseFeature;
using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.Utility.Conditions;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.InputService;

namespace _Project.Code.Runtime.Gameplay.GameLoop
{
    public class GameplayStatesFactory
    {
        private readonly StageService _stageService;
        private readonly IInputService _inputService;
        private readonly DefenceObjectsFactory _defenceObjectsFactory;
        private readonly ConfigsProvider _configsProvider;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly MainHeroService _mainHeroService;
        private readonly Wallet _wallet;
        private readonly WinLoseCounter _winLoseCounter;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly BrainsContext _brainsContext;
        private readonly GameplayPopupService _gameplayPopupService;
        private readonly DefenceObjectsSelector _defenceObjectsSelector;
        
        public GameplayStatesFactory(DIContainer container)
        {
            _stageService = container.Resolve<StageService>();
            _inputService = container.Resolve<IInputService>();
            _defenceObjectsFactory = container.Resolve<DefenceObjectsFactory>();
            _configsProvider = container.Resolve<ConfigsProvider>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
            _mainHeroService = container.Resolve<MainHeroService>();
            _wallet = container.Resolve<Wallet>();
            _winLoseCounter = container.Resolve<WinLoseCounter>();
            _playerDataProvider = container.Resolve<PlayerDataProvider>();
            _brainsContext = container.Resolve<BrainsContext>();
            _gameplayPopupService = container.Resolve<GameplayPopupService>();
            _defenceObjectsSelector = container.Resolve<DefenceObjectsSelector>();
        }

        public GameplayStateMachine CreateGameplayStateMachine()
        {
            GameplayStateMachine gameLoopStateMachine = CreateGameLoop();

            LoseState loseState = new LoseState(
                _gameplayPopupService,
                "Помер!",
                _coroutinePerformer,
                _playerDataProvider,
                _winLoseCounter);

            WinState winState = new WinState(
                _gameplayPopupService,
                "Молодец",
                _wallet,
                _configsProvider,
                _winLoseCounter,
                _playerDataProvider,
                _coroutinePerformer);
            
            ICondition gameLoopToLose = new CompositeCondition(
                new FuncCondition(() => _mainHeroService.MainHero.IsDead.Value));
            
            ICondition gameLoopToWin = new CompositeCondition(
                new FuncCondition(() => _stageService.HasNextStage() == false),
                new FuncCondition(() => _stageService.Stage.IsCompleate.Value));
            
            GameplayStateMachine root = new GameplayStateMachine();

            root
                .AddState(gameLoopStateMachine)
                .AddState(loseState)
                .AddState(winState);

            root
                .AddTransition(gameLoopStateMachine, loseState, gameLoopToLose)
                .AddTransition(gameLoopStateMachine, winState, gameLoopToWin);
            
            return root;
        }

        private GameplayStateMachine CreateGameLoop()
        {
            StageState stageState = new StageState(_stageService, _brainsContext);
            
            PreparationState preparationState = new PreparationState(
                _inputService,
                _defenceObjectsFactory,
                _configsProvider,
                _wallet,
                _gameplayPopupService,
                _defenceObjectsSelector);
            
            ICondition stageToPreparation = new CompositeCondition(
                new FuncCondition(() => _stageService.IsCompleate.Value),
                new FuncCondition(() => _stageService.HasNextStage()),
                new FuncCondition(() => _mainHeroService.MainHero.IsDead.Value == false));
            
            ICondition preparationToStage  = new CompositeCondition(
                new FuncCondition(() => preparationState.Continue));
            
            GameplayStateMachine gameLoopStateMachine = new GameplayStateMachine();
            
            gameLoopStateMachine
                .AddState(stageState)
                .AddState(preparationState);

            gameLoopStateMachine
                .AddTransition(stageState, preparationState, stageToPreparation)
                .AddTransition(preparationState, stageState, preparationToStage);
            
            return gameLoopStateMachine;
        }
    }
}
