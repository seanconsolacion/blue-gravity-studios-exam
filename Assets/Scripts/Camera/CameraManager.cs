using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Singleton;
        [SerializeField] private CinemachineVirtualCamera _normalCamera;
        [SerializeField] private CinemachineVirtualCamera _closeupCamera;

        private void Awake()
        {
            Singleton = this;
        }

        public void ChangeToCloseupCamera()
        {
            _normalCamera.Priority = 0;
            _closeupCamera.Priority = 1;
        }

        public void ChangeToNormalCamera()
        {
            _normalCamera.Priority = 1;
            _closeupCamera.Priority = 0;
        }

        public void SetCameraFollow(Transform toFollow)
        {
            _normalCamera.Follow = toFollow;
            _closeupCamera.Follow = toFollow;
        }
    }
}