using UnityEngine;

namespace _Project.Code.Runtime.UI.Core
{
    public class UIRoot : MonoBehaviour
    {
        [field: SerializeField] public Transform HUDLayer { get; private set; }
        [field: SerializeField] public Transform UnderPopupsLayer { get; private set; }
        [field: SerializeField] public Transform PopupsLayer { get; private set; }
        [field: SerializeField] public Transform OverPopupsLayer { get; private set; }
    }
}
