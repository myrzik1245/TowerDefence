using UnityEngine;

namespace _Project.Code.Runtime.Configs.Explosion
{
    public abstract class ExplosionConfig : ScriptableObject
    {
        [field: SerializeField] public float Radius { get; private set; }
    }
}
