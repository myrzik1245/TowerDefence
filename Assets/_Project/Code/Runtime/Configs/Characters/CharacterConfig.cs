using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Code.Runtime.Configs.Characters
{
    public abstract class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public string PrefabPath { get; private set; }
    }
}
