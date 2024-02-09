using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        public float baseSpeed = 5f;

        [field: Header("References")]
        [field: SerializeField] public PlayerSO playerData { get; private set; }

        [field: Header("Collisions")]
        [field: SerializeField] public CapsuleColliderUtility colliderUtility { get; private set; }

        [field: Header("Layers")]
        [field: SerializeField] public PlayerLayerData layerData { get; private set; }

        public PlayerInput playerInput { get; private set; }

        public Rigidbody body { get; private set; }

        public Transform cameraTransform { get; private set; }

        private PlayerMovementStateMachine _movementStateMachine;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            body = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;

            colliderUtility.Initialize(gameObject);
            colliderUtility.CalculateCapsuleColliderDimensions();

            _movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void OnValidate()
        {
            colliderUtility.Initialize(gameObject);
            colliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            _movementStateMachine.ChangeState(_movementStateMachine.idlingState);
        }

        private void Update()
        {
            _movementStateMachine.HandleInput();
            _movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _movementStateMachine.FixedUpdate();
        }
    }
}
