using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Ability/AbilityContainer", fileName = "AbilityContainer")]
    public class AbilityContainer : ScriptableObject
    {
        [SerializeField] private AbilityConfig[] _abilities;
        
        public IReadOnlyCollection<AbilityConfig> Abilities => _abilities;
        
        public TConfig GetConfigById<TConfig>(string id) where TConfig : AbilityConfig
        {
            return (TConfig)_abilities.First(ability => ability.ID == id);
        }
    }
}
