using UnityEngine;

public interface IMovementCalculator
{
    Vector3 CalculateMovement(Vector3 pushDirection, bool isPushing, float pushPower);
}
