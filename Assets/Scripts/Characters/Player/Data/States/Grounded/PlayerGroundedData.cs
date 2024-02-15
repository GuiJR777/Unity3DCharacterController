using System;
using UnityEngine;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField] [field: Range(0f, 25f)] public float baseSpeed { get; private set; } = 5f;
        [field: SerializeField] public AnimationCurve slopeSpeedAngles { get; private set; }

        [field: SerializeField] public PlayerRotationData baseRotationData { get; private set; }

        [field: SerializeField] public PlayerDashData dashData { get; private set; }
        [field: SerializeField] public PlayerWalkData walkData { get; private set; }
        [field: SerializeField] public PlayerRunData runData { get; private set; }
        [field: SerializeField] public PlayerSprintingData sprintingData { get; private set; }

        [field: SerializeField] public PlayerStoppingData stoppingData { get; private set; }
    }
}
