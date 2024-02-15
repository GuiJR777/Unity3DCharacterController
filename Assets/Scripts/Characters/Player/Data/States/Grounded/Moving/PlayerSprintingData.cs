using System;
using UnityEngine;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerSprintingData
    {
        [field: SerializeField] [field: Range(1f, 5f)] public float speedModifier { get; private set; } = 1.7f;
        [field: SerializeField] [field: Range(0f, 5f)] public float sprintingToRunTime { get; private set; } = 1f;
    }
}
