using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AttackFeature.Core
{
    public interface IAttack
    {
        public IReadOnlyReactiveEvent Attacked { get; }
        void Attack(Vector3 position);
    }
}
