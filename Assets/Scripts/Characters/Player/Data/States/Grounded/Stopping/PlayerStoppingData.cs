using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerStoppingData
    {
        [field: SerializeField] [field: Range(0f, 15f)] public float lightDecelerateForce { get; private set; } = 5f;
        [field: SerializeField] [field: Range(0f, 15f)] public float mediumDecelerateForce { get; private set; } = 6.5f;
        [field: SerializeField] [field: Range(0f, 15f)] public float hardDecelerateForce { get; private set; } = 5f;
    }
}
