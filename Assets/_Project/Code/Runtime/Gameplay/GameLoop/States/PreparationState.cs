using _Project.Code.Runtime.Configs.Mine;
using _Project.Code.Runtime.Configs.Shop;
using _Project.Code.Runtime.Gameplay.DefenceFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Meta.WalletFeature;
using _Project.Code.Runtime.UI.Gameplay;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.Extensions;
using _Project.Code.Runtime.Utility.InputService;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Code.Runtime.Gameplay.GameLoop.States
{
    public class PreparationState : State, IUpdatableState
    {
        private readonly IInputService _inputService;
        private readonly DefenceObjectsFactory _defenceObjectsFactory;
        private readonly ConfigsProvider _configs;
        private readonly Wallet _wallet;
        private readonly GameplayPopupService _popupService;

        public PreparationState(IInputService inputService, DefenceObjectsFactory defenceObjectsFactory, ConfigsProvider configs, Wallet wallet, GameplayPopupService popupService)
        {
            _inputService = inputService;
            _defenceObjectsFactory = defenceObjectsFactory;
            _configs = configs;
            _wallet = wallet;
            _popupService = popupService;
        }

        public bool Continue { get; private set; }

        public void Update(float deltaTime)
        {
            if (_inputService.Attack.Down)
            {
                MineConfig mineConfig = _configs.GetConfig<MineConfig>();
                int price = _configs.GetConfig<ShopConfig>().GetPrice(mineConfig);
                
                if (_wallet.Enough(CurrencyType.Soft, price) && _inputService.IsCursorOverUI == false)
                {
                    _defenceObjectsFactory.CreateMine( 
                        VectorExtensions.CameraToWorldPoint(_inputService.MousePosition),
                        mineConfig,
                        TeamsType.Player);
                    
                    _wallet.Spend(CurrencyType.Soft, price);
                }
            }
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
    }
}
