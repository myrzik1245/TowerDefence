using UnityEngine;

namespace _Project.Code.Runtime.Utility.Extensions
{
    public class VectorExtensions
    {
        public static Vector3 CameraToWorldPoint(Vector2 mousePosition, Vector3 offset = default)
        {
            Plane groundPlane = new(Vector3.up, offset);
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            groundPlane.Raycast(ray, out float rayDistance);
            
            return ray.GetPoint(rayDistance);
        }
    }
}
