using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmCameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera_;
    Cinemachine3rdPersonFollow thirdPersonFollow_;
    CinemachineComposer composer_;

    private void Awake()
    {
        SetVirtualCamera();
    }
    /// <summary>
    /// do at awake
    /// </summary>
    public void SetVirtualCamera()
    {
        thirdPersonFollow_ = virtualCamera_.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        composer_ = virtualCamera_.GetCinemachineComponent<CinemachineComposer>();

    }
}
