using UnityEngine;

/// <summary>
/// adjusts the player's speed based on the slope of the surface.
/// </summary>
public class SlopeAdjuster : MonoBehaviour
{
    public LayerMask groundLayer; // layer for ground objects
    private SpeedController speedController; // reference to the SpeedController component

    void Awake()
    {
        speedController = GetComponent<SpeedController>(); // initialize the SpeedController component
    }

    /// <summary>
    /// adjusts the player's speed based on the slope of the surface.
    /// </summary>
    /// <param name="transform">the transform of the player object.</param>
    public void AdjustSpeedBySlope(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 3f, groundLayer)) // check for ground under the player
        {
            Vector3 flatForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // calculate flat forward direction
            float forwardSlopeAngle = Vector3.Angle(flatForward, hit.normal) - 90; // calculate the forward slope angle
            speedController.AdjustSpeedBySlope(forwardSlopeAngle); // adjust speed based on the slope angle
        }
    }
}