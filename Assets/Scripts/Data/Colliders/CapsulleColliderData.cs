using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    public class CapsulleColliderData
    {
        public CapsuleCollider collider { get; private set; }
        public Vector3 colliderCenterInLocalSpace { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if (collider != null)
            {
                return;
            }

            collider = gameObject.GetComponent<CapsuleCollider>();
            UpdateColliderData();
        }

        public void UpdateColliderData()
        {
            colliderCenterInLocalSpace = collider.center;
        }
    }
}