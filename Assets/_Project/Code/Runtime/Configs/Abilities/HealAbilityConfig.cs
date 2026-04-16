using UnityEngine;

namespace _Project.Code.Runtime.Configs.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Ability/HealAbilityConfig", fileName = "HealAbilityConfig")]
    public class HealAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public ValueModifier Modifier { get; private set; }
    }
}
