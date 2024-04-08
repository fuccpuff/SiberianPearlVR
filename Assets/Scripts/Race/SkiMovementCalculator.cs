using UnityEngine;

public class SkiMovementCalculator : IMovementCalculator
{
    public Vector3 CalculateMovement(Vector3 pushDirection, bool isPushing, float pushPower, float speed, Transform playerTransform)
    {
        float airResistanceCoefficient = 0.05f;
        float frictionCoefficient = 0.1f;
        float gravityEffect = 9.8f;

        Vector3 airResistance = -speed * airResistanceCoefficient * playerTransform.forward;
        Vector3 friction = -speed * frictionCoefficient * playerTransform.forward;
        Vector3 gravity = gravityEffect * Vector3.down;

        Vector3 totalForce = Vector3.zero;

        if (isPushing)
        {
            totalForce += pushDirection * pushPower;
        }

        totalForce += airResistance + friction + gravity;

        return totalForce;
    }
}
