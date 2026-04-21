using _Project.Code.Runtime.Configs.Abilities;
using _Project.Code.Runtime.Data.Player;
using _Project.Code.Runtime.Gameplay.MainHero;
using _Project.Code.Runtime.Utility.DataManagment.DataProviders;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature
{
    public class AddAbilitiesToPlayerService : IDataReader<PlayerData>, IDisposable
    {
        private readonly AbilityContainer _abilityContainer;
        private readonly AbilitiesFactory _abilitiesFactory;
        private readonly IDisposable _mainHeroSpawnedSubscription;
        private List<string> _buyedAbilitiesIDs;
        
        public AddAbilitiesToPlayerService(
            PlayerDataProvider playerDataProvider,
            AbilityContainer abilityContainer,
            AbilitiesFactory abilitiesFactory,
            MainHeroService mainHeroService)
        {
            _abilityContainer = abilityContainer;
            _abilitiesFactory = abilitiesFactory;

            _mainHeroSpawnedSubscription = mainHeroService.MainHeroSpawned.Subscribe(OnMainHeroSpawned);
            
            playerDataProvider.RegisterReader(this);
        }

        public void ReadFrom(PlayerData data)
        {
            _buyedAbilitiesIDs = new(data.PurchasedItemsIds);
        }
        
        public void Dispose()
        {
            _mainHeroSpawnedSubscription?.Dispose();
        }

        private void OnMainHeroSpawned(IMainHero mainHero)
        {
            foreach (AbilityConfig config in _abilityContainer.Abilities)
                if (_buyedAbilitiesIDs.Contains(config.ShopItemConfig.ID))
                    _abilitiesFactory.CreateAbility(config, mainHero, mainHero, mainHero);
        }
    }
}
