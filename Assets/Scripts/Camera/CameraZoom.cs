using System;
using Cinemachine;
using UnityEngine;

namespace RamiresTechGames
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] [Range(0f, 18f)] private float defaultDistance = 6f;
        [SerializeField] [Range(0f, 18f)] private float minimumDistance = 3f;
        [SerializeField] [Range(0f, 18f)] private float maximumDistance = 18f;

        [SerializeField] [Range(0f, 18f)] private float smoothing = 4f;
        [SerializeField] [Range(0f, 18f)] private float zoomSensitivity = 1f;

        private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;

        private float currentTargetDistance;

        private void Awake()
        {
            framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();

            currentTargetDistance = defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            float zoomInput = inputProvider.GetAxisValue(2) * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomInput, minimumDistance, maximumDistance);

            float currentDistance = framingTransposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

            framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}
