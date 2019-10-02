using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    Camera cameraObject;

    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        doMovement();
    }

    void doMovement()
    {
        Vector3 _forward = cameraObject.transform.forward;
        _forward.y = 0;

        Vector3 _rSide = cameraObject.transform.right;
        _rSide.y = 0;

        float _vert = Input.GetAxis("Vertical");
        float _horz = Input.GetAxis("Horizontal");

        Debug.DrawRay(transform.position, _forward, Color.red);
        if (_vert > 0)
        {
            transform.position += _forward * moveSpeed * _vert;
        }

        if (_vert < 0)
        {
           transform.position += _forward * moveSpeed * _vert;
        }

        if (_horz < 0)
        {
            transform.position += _rSide * moveSpeed * _horz; 
        }

        if (_horz > 0)
        {
           transform.position += _rSide * moveSpeed * _horz;
        }
    }
}
