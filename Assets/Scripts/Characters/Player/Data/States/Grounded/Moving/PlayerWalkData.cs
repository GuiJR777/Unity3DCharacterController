using System;
using UnityEngine;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField] [field: Range(0f, 1f)] public float speedModifier { get; private set; } = 0.225f;
    }
}
