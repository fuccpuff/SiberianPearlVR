using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections;

public class sk2 : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public float maxSpeed = 20f;
    public float acceleration = 0.2f;
    public float deceleration = 0.1f;
    public float noPushDeceleration = 0.02f;
    public LayerMask groundLayer;
    public Terrain terrain;

    private Vector3 lastLeftPosition;
    private Vector3 lastRightPosition;
    private float speed = 0f;
    private TMP_Text speedText;
    private float lastPushTime;

    void Start()
    {
        lastLeftPosition = leftController.transform.position;
        lastRightPosition = rightController.transform.position;
        StartCoroutine(FindSpeedTextObject());
    }

    void Update()
    {
        if (speedText == null) return;

        Vector3 leftDelta = leftController.transform.position - lastLeftPosition;
        Vector3 rightDelta = rightController.transform.position - lastRightPosition;

        float leftDirection = Vector3.Dot(leftDelta.normalized, -Camera.main.transform.forward);
        float rightDirection = Vector3.Dot(rightDelta.normalized, -Camera.main.transform.forward);

        lastLeftPosition = leftController.transform.position;
        lastRightPosition = rightController.transform.position;

        if (leftDirection > 0 && rightDirection > 0)
        {
            float pushPower = CalculatePushPower(leftDelta, rightDelta);
            UpdateSpeed(pushPower, true);
            lastPushTime = Time.time;
        }
        else if (Time.time - lastPushTime > 1f)
        {
            UpdateSpeed(0, false);
        }
        ApplyMovement();
        AdjustSpeedBySlope();
        PreventTerrainClip();
        speedText.text = $"—корость: {speed.ToString("F2")} м/с";
    }

    private float CalculatePushPower(Vector3 leftDelta, Vector3 rightDelta)
    {
        return (leftDelta.magnitude + rightDelta.magnitude) * acceleration;
    }

    private void UpdateSpeed(float pushPower, bool isPushing)
    {
        if (isPushing)
        {
            speed += pushPower;
        }
        else
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0);
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);
    }

    private void ApplyMovement()
    {
        Vector3 direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void AdjustSpeedBySlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 3f, groundLayer))
        {
            Vector3 flatForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up) - 90;
            float forwardSlopeAngle = Vector3.Angle(flatForward, hit.normal) - 90;

            if (forwardSlopeAngle < 0)
            {
                speed += Mathf.Abs(forwardSlopeAngle) * acceleration * Time.deltaTime;
            }
            else
            {
                if (Time.time - lastPushTime > 1f)
                {
                    speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0);
                }
            }
            speed = Mathf.Clamp(speed, 0, maxSpeed);
        }
    }

    private void PreventTerrainClip()
    {
        float checkDistance = 1.0f;
        Vector3 forwardDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 forwardCheckPoint = transform.position + forwardDirection * checkDistance;

        float currentTerrainHeight = Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.transform.position.y;
        float forwardTerrainHeight = Terrain.activeTerrain.SampleHeight(forwardCheckPoint) + Terrain.activeTerrain.transform.position.y;
        float currentPlayerHeight = transform.position.y;

        if (currentPlayerHeight > forwardTerrainHeight)
        {
            transform.position = new Vector3(transform.position.x, forwardTerrainHeight, transform.position.z);
        }
        else if (currentPlayerHeight < currentTerrainHeight)
        {
            transform.position = new Vector3(transform.position.x, currentTerrainHeight, transform.position.z);
        }
    }

    private IEnumerator FindSpeedTextObject()
    {
        while (speedText == null)
        {
            GameObject obj = GameObject.FindWithTag("speedtext");
            if (obj != null)
            {
                speedText = obj.GetComponent<TMP_Text>();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}