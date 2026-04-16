using _Project.Code.Runtime.Gameplay.GameLoop;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public GameEvents Event { get; private set; }
    }
}
