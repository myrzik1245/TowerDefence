using System;
using _Project.Code.Runtime.Configs.Defence;
using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.Extensions;
using _Project.Code.Runtime.Utility.InputService;
using _Project.Code.Runtime.Utility.Selector;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class PreparationState : State, IUpdatableState
    {
        private readonly IInputService _inputService;
        private readonly DefenceObjectsFactory _defenceObjectsFactory;
        private readonly ConfigsProvider _configs;
        private readonly Wallet _wallet;
        private readonly GameplayPopupService _popupService;

        private readonly SelectorService<Action> _selector;
        
        public PreparationState(IInputService inputService, DefenceObjectsFactory defenceObjectsFactory, ConfigsProvider configs, Wallet wallet, GameplayPopupService popupService)
        {
            _inputService = inputService;
            _defenceObjectsFactory = defenceObjectsFactory;
            _configs = configs;
            _wallet = wallet;
            _popupService = popupService;
            
            _selector = new SelectorService<Action>(
                BuyAndPlaceTurret);
        }

        public bool Continue { get; private set; }
        private ShopConfig ShopConfig => _configs.GetConfig<ShopConfig>();

        public void Update(float deltaTime)
        {
            if (_inputService.Attack.Down && _inputService.IsCursorOverUI == false)
                _selector.Get().Invoke();
        }

        public override void Enter()
        {
            base.Enter();
            
            _popupService.OpenContinuePopup(() => Continue = true);
        }

        public override void Exit()
        {
            base.Exit();
            
            Continue = false;
        }

        private bool CanBuy(ShopItem shopItem)
        {
            return _wallet.Enough(CurrencyType.Soft, _configs.GetConfig<ShopConfig>().GetPrice(shopItem));
        }

        private void BuyAndPlaceTurret()
        {
            TurretConfig turretConfig = _configs.GetConfig<TurretConfig>();
            int price = ShopConfig.GetPrice(turretConfig);

            if (CanBuy(turretConfig))
            {
                _defenceObjectsFactory.CreateTurret(
                    VectorExtensions.CameraToWorldPoint(_inputService.MousePosition),
                    turretConfig,
                    TeamsType.Player);
            }
        }
        
        private void BuyAndPlaceMine()
        {
            MineConfig mineConfig = _configs.GetConfig<MineConfig>();
            int price = ShopConfig.GetPrice(mineConfig);
                
            if (CanBuy(mineConfig))
            {
                _defenceObjectsFactory.CreateMine( 
                    VectorExtensions.CameraToWorldPoint(_inputService.MousePosition),
                    mineConfig,
                    TeamsType.Player);
                    
                _wallet.Spend(CurrencyType.Soft, price);
            }
        }
    }
}
