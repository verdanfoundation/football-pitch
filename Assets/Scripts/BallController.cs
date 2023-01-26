using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Position Settings")]
    public static Vector3 StartingPosition = new(0, 3.5f, 0);
    public float ballOnFallTransition = 5f;

    [Header("Ball Movement Settings")]
    public float movementForce = 200f;
    public float jumpForce = 4000f;
    private float _horizontalAcceleration;
    private float _verticalAcceleration;

    [Header("Ball Physics Settings")]
    public Rigidbody ballRigidbody;
    public float maxAngularVelocity;
    private bool _isRigidbody;

    [HideInInspector] public int second;
    private bool _isOutTextEnabled;
    private int _framesToHideOutText;
    
    void Start()
    {
        transform.position = StartingPosition;
        _isRigidbody = TryGetComponent<Rigidbody>(out ballRigidbody);
        ballRigidbody.maxAngularVelocity = maxAngularVelocity;
        second = (int)(1 / Time.deltaTime);
    }
    
    void Update()
    {
        if (_isRigidbody && (_horizontalAcceleration = Input.GetAxis("Horizontal")) != 0)
            ballRigidbody.AddTorque(0, 0, -_horizontalAcceleration * movementForce * Time.deltaTime);

        if (_isRigidbody && (_verticalAcceleration = Input.GetAxis("Vertical")) != 0)
            ballRigidbody.AddTorque(_verticalAcceleration * movementForce * Time.deltaTime, 0, 0);

        if (_isRigidbody && Input.GetKey(KeyCode.Space))
        {
            if (transform.position.y < 1f)
                ballRigidbody.AddForce(0, jumpForce * Time.deltaTime, 0);
            else
            {
                if (IsBallOnGround())
                    ballRigidbody.AddForce(0, jumpForce * Time.deltaTime, 0);
            }
        }

        if (IsBallOut())
        {
            TextManager.ToggleOutText(true);
            _isOutTextEnabled = true;
            _framesToHideOutText = second * 3;
            BallBackOnPitch();
        }

        if (_framesToHideOutText == 0 && _isOutTextEnabled)
        {
            TextManager.ToggleOutText(false);
            _isOutTextEnabled = false;
        }

        if (_isOutTextEnabled)
            _framesToHideOutText--;
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Goal"))
        {
            transform.position = StartingPosition;
        }
    }

    private bool IsBallOnGround()
    {
        return transform.position.y is < 0.51f and > 0.49f;
    }

    private bool IsBallOut()
    {
        return transform.position.y < -3.5f;
    }

    private void BallBackOnPitch()
    {
        var ballPosition = transform.position;
        Vector3 newPosition = new Vector3(ballPosition.x, 3.5f, ballPosition.z);
        
        if (ballPosition.z > 0)
            newPosition.z -= ballOnFallTransition;
        else
            newPosition.z += ballOnFallTransition;

        if (ballPosition.x > 0)
            newPosition.x -= ballOnFallTransition;
        else
            newPosition.x += ballOnFallTransition;

        StopBall();
        transform.position = newPosition;
    }

    private void StopBall()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
    }
}
