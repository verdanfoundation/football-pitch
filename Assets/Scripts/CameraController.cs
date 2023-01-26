using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera (Ball) Settings")]
    public GameObject ballObject;
    public Vector3 distanceFromBall;

    [Header("Camera Position Settings")]
    public float lookUpDistance;
    public float lerpAmount;
    public CameraMode cameraMode = CameraMode.Stay;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            cameraMode = cameraMode == CameraMode.Stay ? CameraMode.Follow : CameraMode.Stay;

            var transformRotation = transform.rotation;
            if (cameraMode == CameraMode.Stay)
            {
                transform.position = new Vector3(0, 20, -28.5f);
                transformRotation.eulerAngles = new Vector3(38.141f, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        if (cameraMode == CameraMode.Follow)
        {
            var ballPosition = ballObject.transform.position;
            transform.position = Vector3.Lerp(transform.position, ballPosition + distanceFromBall,
                lerpAmount);
                
            transform.LookAt(ballPosition);
            transform.Rotate(lookUpDistance, 0, 0);
        }
    }
}
