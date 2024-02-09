using System;
using UnityEngine;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerRunData
    {
        [field: SerializeField] [field: Range(1f, 2f)] public float speedModifier { get; private set; } = 1f;
    }
}
