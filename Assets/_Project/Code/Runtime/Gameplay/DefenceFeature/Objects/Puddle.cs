using _Project.Code.Runtime.Gameplay.AttackFeature.Explosion;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.Reactive.Event;
using _Project.Code.Runtime.Utility.Reactive.Variable;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature.Objects
{
    public class Puddle : MonoBehaviour, ITeam, IExplosion, IInitializableCharacter, IClearOnStage
    {
        private float _cooldown;
        private Explosion _explosion;
        
        private float _time;
        private readonly ReactiveVariable<bool> _isInitialized = new();

        public IReadOnlyReactiveEvent<Vector3> AttackExecuted => _explosion.AttackExecuted;
        public IReadOnlyReactiveVariable<bool> IsInitialized => _isInitialized;
        public TeamsType TeamType { get; private set; }

        public void Initialize(float cooldown, Explosion explosion, TeamsType teamType)
        {
            _cooldown = cooldown;
            _explosion = explosion;
            TeamType = teamType;
            
            _isInitialized.Value = true;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _cooldown)
            {
                _time = 0;
                _explosion.Execute(transform.position, this);
            }
        }
        
        public void Release()
        {
            Destroy(gameObject);
        }
    }
}