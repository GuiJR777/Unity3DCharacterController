using UnityEngine;

namespace RamiresTechGames
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player", order = 0)]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData playerGroundedData { get; private set; }
    }
}
