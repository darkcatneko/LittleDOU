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
    private void Update()
    {
        //thirdPersonFollow_.ShoulderOffset.y = getCmDistance();
        thirdPersonFollow_.ShoulderOffset.y = Mathf.Lerp(thirdPersonFollow_.ShoulderOffset.y, getCmDistance(), 0.1f);
    }
    /// <summary>
    /// do at awake
    /// </summary>
    public void SetVirtualCamera()
    {
        thirdPersonFollow_ = virtualCamera_.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        composer_ = virtualCamera_.GetCinemachineComponent<CinemachineComposer>();
    }
    
    float getCmDistance()
    {
        var speedRef = GameManager.Instance.PlayerObject.GetComponent<Rigidbody>().velocity.magnitude;
        speedRef = Mathf.Clamp(speedRef, 0, 100);

        var y =  15f - (100f - speedRef) * 8f / 100f;
        return y;
    }    
}
