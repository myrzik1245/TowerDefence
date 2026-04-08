using System;
using _Project.Code.Runtime.Configs.Defence;
using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.UI.Gameplay.Popups.DefenceObjectsSelectorPopup;
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
        private readonly DefenceObjectsSelector _selector;
        private DefenceObjectsSelectorPopupPresenter _defenceObjectsSelectorPopupPresenter;

        
        public PreparationState(
            IInputService inputService,
            DefenceObjectsFactory defenceObjectsFactory,
            ConfigsProvider configs,
            Wallet wallet,
            GameplayPopupService popupService,
            DefenceObjectsSelector defenceObjectsSelector)
        {
            _inputService = inputService;
            _defenceObjectsFactory = defenceObjectsFactory;
            _configs = configs;
            _wallet = wallet;
            _popupService = popupService;
            _selector = defenceObjectsSelector;
        }

        public bool Continue { get; private set; }
        private DefenceObjectShopConfig DefenceObjectShopConfig => _configs.GetConfig<DefenceObjectShopConfig>();

        public void Update(float deltaTime)
        {
            if (_inputService.Attack.Down && _inputService.IsCursorOverUI == false)
                TryCreate(_selector.Get());
        }

        public override void Enter()
        {
            base.Enter();
            
            _popupService.OpenContinuePopup(() => Continue = true);
            _defenceObjectsSelectorPopupPresenter = _popupService.OpenDefenceObjectsSelectorPopup();
        }

        public override void Exit()
        {
            base.Exit();
            
            _popupService.Close(_defenceObjectsSelectorPopupPresenter);
            
            Continue = false;
        }

        private void TryCreate(DefenceObjectTypes type)
        {
            switch (type)
            {
                case DefenceObjectTypes.Mine:
                    TryBuyAndPlaceMine(type);
                    break;
                
                case DefenceObjectTypes.Puddle:
                    TryBuyAndPlacePuddle(type);
                    break;
                
                case DefenceObjectTypes.Turret:
                    TryBuyAndPlaceTurret(type);
                    break;
                
                default:
                    throw new NotSupportedException($"Defence Object type {type.GetType()} not supported");
            }
        }
        
        private bool CanBuy(DefenceObjectTypes type)
        {
            return _wallet.Enough(CurrencyType.Soft, _configs.GetConfig<DefenceObjectShopConfig>().GetPrice(type));
        }

        private void TryBuyAndPlacePuddle(DefenceObjectTypes type)
        {
            PuddleConfig puddleConfig = _configs.GetConfig<PuddleConfig>();
            int price = DefenceObjectShopConfig.GetPrice(type);

            if (CanBuy(type))
            {
                _defenceObjectsFactory.CreatePuddle(
                    VectorExtensions.CameraToWorldPoint(_inputService.MousePosition),
                    puddleConfig,
                    TeamsType.Player);
                
                _wallet.Spend(CurrencyType.Soft, price);
            }
        }
        
        private void TryBuyAndPlaceTurret(DefenceObjectTypes type)
        {
            TurretConfig turretConfig = _configs.GetConfig<TurretConfig>();
            int price = DefenceObjectShopConfig.GetPrice(type);

            if (CanBuy(type))
            {
                _defenceObjectsFactory.CreateTurret(
                    VectorExtensions.CameraToWorldPoint(_inputService.MousePosition),
                    turretConfig,
                    TeamsType.Player);
                
                _wallet.Spend(CurrencyType.Soft, price);
            }
        }
        
        private void TryBuyAndPlaceMine(DefenceObjectTypes type)
        {
            MineConfig mineConfig = _configs.GetConfig<MineConfig>();
            int price = DefenceObjectShopConfig.GetPrice(type);
                
            if (CanBuy(type))
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
