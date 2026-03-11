using UnityEngine;

namespace _Project.Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "BonusConfig", menuName = "Configs/BonusConfig")]
    public class BonusConfig : ScriptableObject
    {
        [field: SerializeField] public int WinBonus { get; private set; }
    }
}
