using UnityEngine;

/// <summary>
///  орректирует скорость игрока в зависимости от наклона поверхности.
/// </summary>
public class SlopeAdjuster : MonoBehaviour
{
    public LayerMask groundLayer; // слой земли дл€ проверки
    private SpeedController speedController; // ссылка на компонент SpeedController

    void Awake()
    {
        speedController = GetComponent<SpeedController>(); // инициализирую компонент SpeedController
    }

    /// <summary>
    ///  орректирует скорость игрока в зависимости от наклона поверхности.
    /// </summary>
    /// <param name="transform">“рансформ объекта игрока.</param>
    public void AdjustSpeedBySlope(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 3f, groundLayer)) // провер€ю наличие земли под игроком
        {
            Vector3 flatForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // вычисл€ю плоское направление вперед
            float forwardSlopeAngle = Vector3.Angle(flatForward, hit.normal) - 90; // вычисл€ю угол наклона вперед
            speedController.AdjustSpeedBySlope(forwardSlopeAngle); // корректирую скорость на основе угла наклона
        }
    }
}