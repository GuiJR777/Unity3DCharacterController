using System;
using UnityEngine;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField] [field: Range(1f, 3f)] public float speedModifier { get; private set; } = 2f;
        [field: SerializeField] [field: Range(1, 10)] public int limitConsecutiveDashes { get; private set; } = 2;
        [field: SerializeField] [field: Range(0f, 2f)] public float timeToBeConsideredConsecutive { get; private set; } = 1f;
        [field: SerializeField] [field: Range(0f, 5f)] public float dashLimitReachedCooldown { get; private set; } = 1.75f;
    }
}
