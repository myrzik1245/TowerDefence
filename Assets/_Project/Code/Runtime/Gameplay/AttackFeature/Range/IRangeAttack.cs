using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Range
{
    public interface IRangeAttack : IAttack
    {
        bool InRange(Vector3 position);
    }
}
