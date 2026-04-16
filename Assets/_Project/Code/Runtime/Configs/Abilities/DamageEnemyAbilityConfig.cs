using UnityEngine;

namespace _Project.Code.Runtime.Configs.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Ability/DamageEnemyAbilityConfig", fileName = "DamageEnemyAbilityConfig")]
    public class DamageEnemyAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public ValueModifier Modifier { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
