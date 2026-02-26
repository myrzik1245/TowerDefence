using NaughtyAttributes;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Health
{
    [Serializable]
    public class HealthConfigData
    {
        [field: SerializeField] public int StartHealth { get; private set; }
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }
        [field: SerializeField] public bool ClampHealth { get;  private set; }

        public void OnValidate()
        {
            if (ClampHealth)
                StartHealth = Mathf.Clamp(StartHealth, 0, MaxHealth);
            else
                MaxHealth = int.MaxValue;
        }
    }
}
