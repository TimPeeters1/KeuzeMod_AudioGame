using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    Camera cameraObject;

    [SerializeField] float moveSpeed;
    [SerializeField] float cinematicMoveSpeed;

    [Space]
    public bool isCinematic;
    [SerializeField] bool isVR;
    [SerializeField] CinemachineVirtualCamera cinematicVCam;

    [Space]
    [SerializeField] float footstepVolume;

    [Space]
    public SteamVR_Action_Boolean isPressing = null;
    public SteamVR_Action_Vector2 moveVector = null;

    float _vert;
    float _horz;

    public AudioSource footstepSource;
    public AudioClip indoorWalkSound;

    GameObject followObject;

    void Start()
    {
        cameraObject = GetComponentInChildren<Camera>();
        cinematicVCam = GetComponent<CinemachineVirtualCamera>();
        cinematicVCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0f;

        footstepSource.volume = 0;
    }

    void Update()
    {
        if (isVR)
        {
            if (isPressing.state)
            {
                _vert = moveVector.axis.y;
                _horz = moveVector.axis.x;
            }
        }
        else
        {
            _vert = Input.GetAxis("Vertical");
            _horz = Input.GetAxis("Horizontal");
        }

        if (!isCinematic)
        {
            doMovement();
        }
        else
        {
            doCinematicMove();
        }
    }

    private void doCinematicMove()
    {
        if (_vert > 0)
        {
            cinematicVCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += cinematicMoveSpeed * 0.01f * _vert;

            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }

            footstepSource.volume = _vert * footstepVolume;
        }
        if (_vert < 0)
        {
            cinematicVCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition -= cinematicMoveSpeed * 0.01f * -_vert;

            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }

            footstepSource.volume += _vert;
        }

        if(_vert == 0)
        {
            footstepSource.Stop();
            footstepSource.volume = 0;
        }
    }

    void doMovement()
    {
        Vector3 _forward = cameraObject.transform.forward;
        _forward.y = 0;

        Vector3 _side = cameraObject.transform.right;
        _side.y = 0;

        if (_vert > 0)
        {
            transform.position += _forward * moveSpeed * _vert;
            footstepSource.volume = _vert * footstepVolume;
        }

        if (_vert < 0)
        {
            transform.position += _forward * moveSpeed * _vert;
            footstepSource.volume = -_vert * footstepVolume;
        }

        if (_horz < 0)
        {
            transform.position += _side * moveSpeed * _horz;

            footstepSource.volume = -_horz * footstepVolume;
        }

        if (_horz > 0)
        {
            transform.position += _side * moveSpeed * _horz;
            footstepSource.volume = _horz * footstepVolume;
        }

        if (!footstepSource.isPlaying)
        {
            footstepSource.Play();
        }

        


    }
}
