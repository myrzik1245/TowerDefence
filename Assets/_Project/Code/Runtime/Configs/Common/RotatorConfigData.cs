using System;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Common
{
    [Serializable]
    public class RotatorConfigData
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}
