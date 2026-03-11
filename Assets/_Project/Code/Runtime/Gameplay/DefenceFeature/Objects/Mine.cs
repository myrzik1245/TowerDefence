using _Project.Code.Runtime.Configs.Mine;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature.Objects
{
    [RequireComponent(typeof(SphereCollider))]
    public class Mine : MonoBehaviour, ITeam
    {
        private Explosion _explosion;

        public TeamsType TeamType { get; private set; }
        
        public void Initialize(Explosion explosion, TeamsType teamType, MineConfig config)
        {
            TeamType = teamType;
            _explosion = explosion;
            
            GetComponent<SphereCollider>().radius = config.Radius;
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
