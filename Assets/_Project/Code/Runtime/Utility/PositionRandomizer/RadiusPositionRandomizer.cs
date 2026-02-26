using UnityEngine;

namespace _Project.Code.Runtime.Utility.PositionRandomizer
{
    public class RadiusPositionRandomizer : IPositionRandomizer
    {
        private readonly float _radius;
        
        public RadiusPositionRandomizer(float radius)
        {
            _radius = radius;
        }


        public Vector3 GetRandomPosition(Vector3 offset = new Vector3())
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 position = new Vector3(randomDirection.x, 0, randomDirection.y) * _radius;
            
            position += offset;
            
            return position;
        }
    }
}
