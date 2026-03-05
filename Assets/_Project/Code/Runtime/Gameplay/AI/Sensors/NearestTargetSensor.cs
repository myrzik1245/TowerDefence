using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.AI.Sensors
{
    public class NearestTargetSensor : MonoBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _bufferSize;
        
        private IBlackboard _blackboard;
        private ITeam _sourceTeam;
        private Buffer<Collider> _buffer;
        
        
        private void Awake()
        {
            _blackboard = GetComponent<IBlackboard>();
            _sourceTeam = GetComponent<ITeam>();
            _buffer = new Buffer<Collider>(_bufferSize);
        }
        
        private void FixedUpdate()
        {
            _buffer.Count = Physics.OverlapSphereNonAlloc(
                transform.position, _range, _buffer.Items, _layerMask, QueryTriggerInteraction.Ignore);

            Transform nearestTransform = null;
            float minDistance = float.MaxValue;
            
            for (int i = 0; i < _buffer.Count; i++)
            {
                Transform currentTransform = _buffer.Items[i].transform;
                
                if (currentTransform.TryGetComponent(out ITeam team))
                    if (team.TeamType != _sourceTeam.TeamType)
                        if (Vector3.Distance(currentTransform.position, transform.position) < minDistance)
                            nearestTransform = currentTransform;
            }
            
            _blackboard.WriteData(BlackboardKeys.Target, nearestTransform);
        }
    }
}
