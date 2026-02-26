using UnityEngine;

namespace _Project.Code.Runtime.Utility.PositionRandomizer
{
    public interface IPositionRandomizer
    {
        Vector3 GetRandomPosition(Vector3 offset = new Vector3());
    }
}
