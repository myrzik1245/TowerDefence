using System;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Common
{
    [Serializable]
    public class MovementConfigData
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Smooth { get; private set; } 
    }
}
