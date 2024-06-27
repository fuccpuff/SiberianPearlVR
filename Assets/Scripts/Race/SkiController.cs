using UnityEngine;

public class SkiController : MonoBehaviour
{
    public GameObject leftControllerObject; // left controller object
    public GameObject rightControllerObject; // right controller object

    private Vector3 lastLeftPosition; // last position of the left controller
    private Vector3 lastRightPosition; // last position of the right controller

    private SpeedController speedController; // component for speed control
    private SlopeAdjuster slopeAdjuster; // component for adjusting speed by slope
    private TerrainClipPrevention terrainClipPrevention; // component for preventing terrain clipping
    private SpeedUIUpdater speedUIUpdater; // component for updating speed UI
    private SkiSoundManager skiSoundManager; // component for managing wind sound
    private MenuController menuController; // component for menu control

    void Start()
    {
        lastLeftPosition = leftControllerObject.transform.position; // save initial position of the left controller
        lastRightPosition = rightControllerObject.transform.position; // save initial position of the right controller

        speedController = GetComponent<SpeedController>(); // get SpeedController component
        slopeAdjuster = GetComponent<SlopeAdjuster>(); // get SlopeAdjuster component
        terrainClipPrevention = GetComponent<TerrainClipPrevention>(); // get TerrainClipPrevention component
        speedUIUpdater = GetComponent<SpeedUIUpdater>(); // get SpeedUIUpdater component
        skiSoundManager = GetComponent<SkiSoundManager>(); // get SkiSoundManager component
        menuController = GetComponent<MenuController>(); // get MenuController component
    }

    void Update()
    {
        Vector3 leftDelta = leftControllerObject.transform.position - lastLeftPosition; // calculate left controller position change
        Vector3 rightDelta = rightControllerObject.transform.position - lastRightPosition; // calculate right controller position change

        float leftDirection = Vector3.Dot(leftDelta.normalized, -Camera.main.transform.forward); // left controller movement direction
        float rightDirection = Vector3.Dot(rightDelta.normalized, -Camera.main.transform.forward); // right controller movement direction

        lastLeftPosition = leftControllerObject.transform.position; // update last position of the left controller
        lastRightPosition = rightControllerObject.transform.position; // update last position of the right controller

        bool isPushing = leftDirection > 0 && rightDirection > 0; // determine if the player is pushing
        float pushPower = CalculatePushPower(leftDelta, rightDelta, isPushing); // calculate push power

        speedController.UpdateSpeed(pushPower, isPushing); // update speed based on push power or no push

        speedController.ApplyMovement(transform); // apply movement based on current speed
        slopeAdjuster.AdjustSpeedBySlope(transform); // adjust speed based on slope
        terrainClipPrevention.PreventTerrainClip(transform); // prevent terrain clipping
        speedUIUpdater.UpdateSpeedText(speedController.Speed); // update speed UI
        skiSoundManager.Update(); // update wind sound
        menuController.Update(); // update menu control
    }

    /// <summary>
    /// calculates push power based on controller position changes.
    /// </summary>
    /// <param name="leftDelta">position change of the left controller.</param>
    /// <param name="rightDelta">position change of the right controller.</param>
    /// <param name="isPushing">indicates whether the player is currently pushing.</param>
    /// <returns>push power.</returns>
    private float CalculatePushPower(Vector3 leftDelta, Vector3 rightDelta, bool isPushing)
    {
        if (isPushing && (leftDelta.magnitude > speedController.minPushDistance || rightDelta.magnitude > speedController.minPushDistance)) // check if movement is sufficient to consider a push
        {
            return (leftDelta.magnitude + rightDelta.magnitude) * speedController.acceleration; // calculate push power
        }
        return 0f; // return zero push power if movement is too small
    }
}