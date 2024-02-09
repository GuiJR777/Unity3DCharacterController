using System;
using UnityEngine;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerRotationData
    {
        [field: SerializeField] public Vector3 targetRotationReachTime { get; private set; }
    }
}
