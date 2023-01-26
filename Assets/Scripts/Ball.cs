using UnityEngine;

public static class Ball
{
    private const float OnFallTransition = 7.5f;
    private static readonly Vector3 StartingPosition = new(0, 3.5f, 0);

    public static bool IsOnGround(Transform transform)
    {
        return transform.position.y is < 0.51f and > 0.49f;
    }

    public static bool IsOut(Transform transform)
    {
        return transform.position.y < -3.5f;
    }

    public static Vector3 GetNewPositionAfterOut(Vector3 currentPosition)
    {
        var newPosition = new Vector3(currentPosition.x, 3.5f, currentPosition.z);
        
        if (currentPosition.z > 0)
            newPosition.z -= OnFallTransition;
        else
            newPosition.z += OnFallTransition;

        if (currentPosition.x > 0)
            newPosition.x -= OnFallTransition;
        else
            newPosition.x += OnFallTransition;

        return newPosition;
    }

    public static void Stop(Rigidbody ballRigidbody)
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
    }

    public static void ResetPosition(GameObject ballObject)
    {
        ballObject.transform.position = StartingPosition;
    }

    public static void MoveBackOnPitch(Transform transform, Rigidbody ballRigidbody)
    {
        var newPosition = GetNewPositionAfterOut(transform.position);
        Stop(ballRigidbody);

        transform.position = newPosition;
    }
}