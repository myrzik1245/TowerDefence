using _Project.Code.Runtime.Gameplay.StatsFeature;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Ability/ChangeStatAbilityConfig", fileName = "ChangeStatAbilityConfig")]
    public class ChangeStatAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public StatTypes StatType { get; private set; }
        [field: SerializeField] public ValueModifier Modifier { get; private set; }
    }

}
