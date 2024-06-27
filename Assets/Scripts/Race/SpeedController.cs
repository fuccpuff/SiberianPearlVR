using UnityEngine;

/// <summary>
/// Управляет скоростью игрока, включая ускорение и замедление.
/// </summary>
public class SpeedController : MonoBehaviour
{
    [Tooltip("Максимальная скорость, которую может достичь игрок.")]
    public float maxSpeed = 20f; // максимальная скорость игрока

    [Tooltip("Скорость ускорения при толчке.")]
    public float acceleration = 0.2f; // скорость ускорения при толчке

    [Tooltip("Скорость замедления, когда игрок не толкается.")]
    public float noPushDeceleration = 0.02f; // скорость замедления без толчка

    [Tooltip("Минимальное расстояние движения контроллера для учета толчка.")]
    public float minPushDistance = 0.01f; // минимальное расстояние движения контроллера для учета толчка

    private float speed = 0f; // текущая скорость игрока
    private float lastPushTime; // время последнего толчка

    /// <summary>
    /// Возвращает текущую скорость.
    /// </summary>
    public float Speed => speed; // возвращаю текущую скорость

    /// <summary>
    /// Обновляет скорость игрока на основе мощности толчка и статуса толчка.
    /// </summary>
    /// <param name="pushPower">Рассчитанная мощность толчка.</param>
    /// <param name="isPushing">Указывает, толкается ли игрок в данный момент.</param>
    public void UpdateSpeed(float pushPower, bool isPushing)
    {
        if (isPushing) // если игрок толкается
        {
            speed += pushPower * Time.deltaTime; // увеличиваю скорость на величину толчка, учитывая дельту времени
            lastPushTime = Time.time; // обновляю время последнего толчка
        }
        else // если игрок не толкается
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0); // уменьшаю скорость с учетом замедления
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed); // ограничиваю скорость в диапазоне от 0 до maxSpeed
    }

    /// <summary>
    /// Применяет движение на основе текущей скорости.
    /// </summary>
    /// <param name="transform">Трансформ объекта игрока.</param>
    public void ApplyMovement(Transform transform)
    {
        Vector3 direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // вычисляю направление движения
        transform.position += direction * speed * Time.deltaTime; // изменяю позицию объекта на основе скорости и направления
    }

    /// <summary>
    /// Корректирует скорость игрока в зависимости от наклона поверхности.
    /// </summary>
    /// <param name="forwardSlopeAngle">Угол наклона вперед.</param>
    public void AdjustSpeedBySlope(float forwardSlopeAngle)
    {
        if (forwardSlopeAngle < 0) // если наклон вперед
        {
            speed += Mathf.Abs(forwardSlopeAngle) * acceleration * Time.deltaTime; // увеличиваю скорость на основе наклона
        }
        else if (Time.time - lastPushTime > 1f) // если прошло больше секунды после последнего толчка
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0); // уменьшаю скорость с учетом замедления
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed); // ограничиваю скорость в диапазоне от 0 до maxSpeed
    }
}