using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RamiresTechGames
{
    [Serializable]
    public class PlayerLayerData
    {
        [field: SerializeField] public LayerMask groundLayer { get; private set; }
    }
}
