using UnityEngine;

namespace _Project.Code.Runtime.Configs.Characters
{
    public abstract class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public string PrefabPath { get; private set; }
        [field: SerializeField] public float SpawnTime { get; private set; }
        [field: SerializeField] public float AttackTime { get; private set; }
    }
}
