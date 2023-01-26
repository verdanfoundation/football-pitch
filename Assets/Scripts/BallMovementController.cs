using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    [Header("Main Ball Settings")]
    public GameObject ballObject;

    [Header("Ball Movement Settings")]
    public float movementForce;
    public float jumpForce;

    [Header("Ball Physics Settings")]
    public Rigidbody ballRigidbody;
    public float maxAngularVelocity;
    private bool _isRigidbody;

    [HideInInspector] public int second;

    void Start()
    {
        Ball.ResetPosition(ballObject);
        
        _isRigidbody = TryGetComponent<Rigidbody>(out ballRigidbody);
        ballRigidbody.maxAngularVelocity = maxAngularVelocity;
        second = (int)(1 / Time.deltaTime);
    }
    
    void Update()
    {
        float horizontalAcceleration;
        float verticalAcceleration;
        
        if (_isRigidbody && (horizontalAcceleration = Input.GetAxis("Horizontal")) != 0)
            ballRigidbody.AddTorque(0, 0, -horizontalAcceleration * movementForce * Time.deltaTime);

        if (_isRigidbody && (verticalAcceleration = Input.GetAxis("Vertical")) != 0)
            ballRigidbody.AddTorque(verticalAcceleration * movementForce * Time.deltaTime, 0, 0);

        if (_isRigidbody && Input.GetKey(KeyCode.Space))
        {
            if (transform.position.y < 1f)
                ballRigidbody.AddForce(0, jumpForce * Time.deltaTime, 0);
            else
            {
                if (Ball.IsOnGround(transform))
                    ballRigidbody.AddForce(0, jumpForce * Time.deltaTime, 0);
            }
        }

        if (Ball.IsOut(transform))
        {
            TextManager.ToggleOutText(true);
            Ball.MoveBackOnPitch(transform, ballRigidbody);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Goal"))
            Ball.ResetPosition(ballObject);
    }
}