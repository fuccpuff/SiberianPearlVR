using UnityEngine;

/// <summary>
/// controls the player's speed, including acceleration and deceleration.
/// </summary>
public class SpeedController : MonoBehaviour
{
    [Tooltip("the maximum speed the player can reach.")]
    public float maxSpeed = 20f; // the player's maximum speed

    [Tooltip("acceleration rate when pushing.")]
    public float acceleration = 0.2f; // acceleration rate when pushing

    [Tooltip("deceleration rate when the player is not pushing.")]
    public float noPushDeceleration = 0.02f; // deceleration rate when not pushing

    [Tooltip("minimum controller movement distance to count as a push.")]
    public float minPushDistance = 0.01f; // minimum controller movement distance to count as a push

    private float speed = 0f; // current player speed
    private float lastPushTime; // time of the last push

    /// <summary>
    /// returns the current speed.
    /// </summary>
    public float Speed => speed; // return the current speed

    /// <summary>
    /// updates the player's speed based on push power and push status.
    /// </summary>
    /// <param name="pushPower">calculated push power.</param>
    /// <param name="isPushing">indicates whether the player is currently pushing.</param>
    public void UpdateSpeed(float pushPower, bool isPushing)
    {
        if (isPushing) // if the player is pushing
        {
            speed += pushPower * Time.deltaTime; // increase speed by the push power, considering delta time
            lastPushTime = Time.time; // update the time of the last push
        }
        else // if the player is not pushing
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0); // decrease speed considering deceleration
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed); // clamp speed between 0 and maxSpeed
    }

    /// <summary>
    /// applies movement based on the current speed.
    /// </summary>
    /// <param name="transform">the transform of the player object.</param>
    public void ApplyMovement(Transform transform)
    {
        Vector3 direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // calculate movement direction
        transform.position += direction * speed * Time.deltaTime; // update position based on speed and direction
    }

    /// <summary>
    /// adjusts the player's speed based on the slope angle.
    /// </summary>
    /// <param name="forwardSlopeAngle">the forward slope angle.</param>
    public void AdjustSpeedBySlope(float forwardSlopeAngle)
    {
        if (forwardSlopeAngle < 0) // if the slope is downhill
        {
            speed += Mathf.Abs(forwardSlopeAngle) * acceleration * Time.deltaTime; // increase speed based on the slope
        }
        else if (Time.time - lastPushTime > 1f) // if more than a second has passed since the last push
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0); // decrease speed considering deceleration
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed); // clamp speed between 0 and maxSpeed
    }
}