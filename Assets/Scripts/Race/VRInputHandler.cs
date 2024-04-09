using UnityEngine;
using UnityEngine.XR;

public class VRInputHandler : IInputHandler
{
    private XRNode leftHandNode = XRNode.LeftHand;
    private XRNode rightHandNode = XRNode.RightHand;
    private Vector3 lastLeftHandPosition, lastRightHandPosition;
    private float pushingThreshold = 0.1f;
    private float pushDirectionThreshold = 0.75f;

    public VRInputHandler()
    {
        lastLeftHandPosition = GetHandPosition(leftHandNode);
        lastRightHandPosition = GetHandPosition(rightHandNode);
    }

    public bool IsPushing()
    {
        Vector3 currentLeftHandPosition = GetHandPosition(leftHandNode);
        Vector3 currentRightHandPosition = GetHandPosition(rightHandNode);

        Vector3 leftHandMovement = currentLeftHandPosition - lastLeftHandPosition;
        Vector3 rightHandMovement = currentRightHandPosition - lastRightHandPosition;

        lastLeftHandPosition = currentLeftHandPosition;
        lastRightHandPosition = currentRightHandPosition;

        return IsPushMovement(leftHandMovement, currentLeftHandPosition) || IsPushMovement(rightHandMovement, currentRightHandPosition);
    }

    public Vector3 GetPushDirection()
    {
        return (lastRightHandPosition - lastLeftHandPosition).normalized;
    }

    private bool IsPushMovement(Vector3 handMovement, Vector3 handPosition)
    {
        float movementMagnitude = handMovement.magnitude;
        if (movementMagnitude < pushingThreshold) return false;

        Vector3 normalizedMovement = handMovement.normalized;
        float movementDirection = Vector3.Dot(normalizedMovement, Vector3.down + Vector3.back);

        return movementDirection > pushDirectionThreshold;
    }

    private Vector3 GetHandPosition(XRNode handNode)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);
        device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        return position;
    }
}