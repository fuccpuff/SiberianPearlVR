using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SkiController : MonoBehaviour
{
    public float pushPower = 5.0f;
    private IInputHandler inputHandler;
    private IMovementCalculator movementCalculator;
    private SkiAudioManager audioManager;
    private SnowEffectManager snowEffectManager;
    private CharacterController characterController;

    void Awake()
    {
        inputHandler = new VRInputHandler();
        movementCalculator = new SkiMovementCalculator();
        audioManager = GetComponent<SkiAudioManager>();
        snowEffectManager = GetComponent<SnowEffectManager>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isPushing = inputHandler.IsPushing();
        Vector3 pushDirection = inputHandler.GetPushDirection();
        float speed = characterController.velocity.magnitude;

        Vector3 movement = movementCalculator.CalculateMovement(pushDirection, isPushing, pushPower, speed, transform);
        characterController.Move(movement * Time.deltaTime);

        audioManager.PlaySoundBasedOnSpeed(speed);
        snowEffectManager.AdjustSnowEffectBasedOnSpeed(speed);
    }
}