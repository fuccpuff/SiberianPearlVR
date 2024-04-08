using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SkiController : MonoBehaviour
{
    public float pushPower = 5.0f;
    private IInputHandler inputHandler;
    private IMovementCalculator movementCalculator;
    private CharacterController characterController;

    void Awake()
    {
        inputHandler = new VRInputHandler();
        movementCalculator = new SkiMovementCalculator();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isPushing = inputHandler.IsPushing();
        Vector3 pushDirection = inputHandler.GetPushDirection();
        Vector3 movement = movementCalculator.CalculateMovement(pushDirection, isPushing, pushPower);

        characterController.Move(movement * Time.deltaTime);

        Debug.Log($"Current Movement Speed: {characterController.velocity.magnitude}");
    }

}
