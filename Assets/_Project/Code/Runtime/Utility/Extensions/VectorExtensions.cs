using UnityEngine;

namespace _Project.Code.Runtime.Utility.Extensions
{
    public class VectorExtensions
    {
        public static Vector3 CameraToWorldPoint(Vector2 mousePosition, Vector3 offset = default)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane groundPlane = new Plane(Vector3.up, offset);

            groundPlane.Raycast(ray, out float rayDistance);
            
            return ray.GetPoint(rayDistance);
        }
    }
}
