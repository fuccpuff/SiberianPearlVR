using UnityEngine;

public class SkiMovementCalculator : IMovementCalculator
{
    public Vector3 CalculateMovement(Vector3 pushDirection, bool isPushing, float pushPower)
    {
        if (isPushing)
        {
            Vector3 movement = pushDirection * pushPower;
            Debug.Log($"Calculating movement: Direction {pushDirection}, Power {pushPower}, Resulting Movement {movement}");
            return movement;
        }
        return Vector3.zero;
    }
}
