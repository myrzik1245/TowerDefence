using _Project.Code.Runtime.Gameplay.StatsFeature;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Configs.Abilities
{
    [Serializable]
    public class ValueModifier
    {
        [SerializeField] private ChangeStatOperation _operation;
        [SerializeField] private int _value;
        
        public Func<int, int> GetChanger()
        {
            switch (_operation)
            {
                case ChangeStatOperation.Multiply:
                    return stat => stat * _value;

                case ChangeStatOperation.Add:
                    return stat => stat + _value;

                case ChangeStatOperation.Subtract:
                    return stat => stat - _value;

                case ChangeStatOperation.AddPercentage:
                    return stat => stat + Mathf.RoundToInt(stat * (_value / 100f));
                        
                default:
                    throw new InvalidOperationException();
            }
        }

        public int GetDelta(int baseValue)
        {
            switch (_operation)
            {
                case ChangeStatOperation.Multiply:
                    return baseValue * _value;
                
                case ChangeStatOperation.Add:
                    return _value;

                case ChangeStatOperation.Subtract:
                    return -_value;

                case ChangeStatOperation.AddPercentage:
                    return Mathf.RoundToInt(baseValue * (_value / 100f));
                        
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
