using _Project.Code.Runtime.Configs.Defence;
using _Project.Code.Runtime.Gameplay.AttackFeature.Explosion;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature.Objects
{
    [RequireComponent(typeof(SphereCollider))]
    public class Mine : MonoBehaviour, ITeam, IExplosion, IInitializableCharacter
    {
        private Explosion _explosion;
        private readonly ReactiveVariable<bool> _isInitialized = new();
        
        public IReadOnlyReactiveEvent<Vector3> AttackExecuted => _explosion.AttackExecuted;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _isInitialized;
        
        public TeamsType TeamType { get; private set; }
        
        public void Initialize(Explosion explosion, TeamsType teamType, MineConfig config)
        {
            TeamType = teamType;
            _explosion = explosion;
            
            GetComponent<SphereCollider>().radius = config.Radius;
            
            _isInitialized.Value = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITeam team))
            {
                if (team.TeamType != TeamType)
                {
                    _explosion.Execute(transform.position, this);
                    Destroy(gameObject);
                }
            }
        }
    }
}
