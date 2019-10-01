using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] float walkSpeed;

    [SerializeField] CinemachineVirtualCamera vCamObject;

    [SerializeField] bool doWalk;

    private void Start()
    {
        vCamObject = GetComponent<CinemachineVirtualCamera>();

        vCamObject.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0f;
    }

    private void Update()
    { 
        if (doWalk)
        {
            doMovement();
        }
    }
    
    void doMovement()
    {
        vCamObject.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += walkSpeed * 0.001f;
    }
}
