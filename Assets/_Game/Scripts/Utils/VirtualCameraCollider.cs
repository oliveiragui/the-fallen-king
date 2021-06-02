using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils
{
    [Serializable]
    public class VirtualCameraCollider : MonoBehaviour
    {
        CinemachineVirtualCamera vcam;

        [SerializeField] int targetPriority;

        void Start()
        {
            vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == vcam.LookAt.gameObject)
            {
                vcam.Priority = targetPriority;
                vcam.gameObject.SetActive(true);
            }
        }
    }
}