using UnityEngine;

public class SkiController : MonoBehaviour
{
    public GameObject leftControllerObject; // объект левого контроллера
    public GameObject rightControllerObject; // объект правого контроллера

    private Vector3 lastLeftPosition; // последняя позиция левого контроллера
    private Vector3 lastRightPosition; // последняя позиция правого контроллера

    private SpeedController speedController; // компонент для управления скоростью
    private SlopeAdjuster slopeAdjuster; // компонент для корректировки скорости по наклону
    private TerrainClipPrevention terrainClipPrevention; // компонент для предотвращения проваливания через поверхность
    private SpeedUIUpdater speedUIUpdater; // компонент для обновления UI скорости
    private SkiSoundManager skiSoundManager; // компонент для управления звуком ветра
    private MenuController menuController; // компонент для управления меню

    void Start()
    {
        lastLeftPosition = leftControllerObject.transform.position; // сохраняю начальную позицию левого контроллера
        lastRightPosition = rightControllerObject.transform.position; // сохраняю начальную позицию правого контроллера

        speedController = GetComponent<SpeedController>(); // получаю компонент SpeedController
        slopeAdjuster = GetComponent<SlopeAdjuster>(); // получаю компонент SlopeAdjuster
        terrainClipPrevention = GetComponent<TerrainClipPrevention>(); // получаю компонент TerrainClipPrevention
        speedUIUpdater = GetComponent<SpeedUIUpdater>(); // получаю компонент SpeedUIUpdater
        skiSoundManager = GetComponent<SkiSoundManager>(); // получаю компонент SkiSoundManager
        menuController = GetComponent<MenuController>(); // получаю компонент MenuController
    }

    void Update()
    {
        Vector3 leftDelta = leftControllerObject.transform.position - lastLeftPosition; // вычисляю изменение позиции левого контроллера
        Vector3 rightDelta = rightControllerObject.transform.position - lastRightPosition; // вычисляю изменение позиции правого контроллера

        float leftDirection = Vector3.Dot(leftDelta.normalized, -Camera.main.transform.forward); // направление движения левого контроллера
        float rightDirection = Vector3.Dot(rightDelta.normalized, -Camera.main.transform.forward); // направление движения правого контроллера

        lastLeftPosition = leftControllerObject.transform.position; // обновляю последнюю позицию левого контроллера
        lastRightPosition = rightControllerObject.transform.position; // обновляю последнюю позицию правого контроллера

        bool isPushing = leftDirection > 0 && rightDirection > 0; // определяю, толкается ли игрок
        float pushPower = CalculatePushPower(leftDelta, rightDelta, isPushing); // рассчитываю мощность толчка

        speedController.UpdateSpeed(pushPower, isPushing); // обновляю скорость с учетом толчка или его отсутствия

        speedController.ApplyMovement(transform); // применяю движение на основе текущей скорости
        slopeAdjuster.AdjustSpeedBySlope(transform); // корректирую скорость по наклону поверхности
        terrainClipPrevention.PreventTerrainClip(transform); // предотвращаю проваливание через поверхность
        speedUIUpdater.UpdateSpeedText(speedController.Speed); // обновляю UI скорости
        skiSoundManager.Update(); // обновляю звук ветра
        menuController.Update(); // обновляю управление меню
    }

    /// <summary>
    /// Рассчитывает мощность толчка на основе изменения позиции контроллеров.
    /// </summary>
    /// <param name="leftDelta">Изменение позиции левого контроллера.</param>
    /// <param name="rightDelta">Изменение позиции правого контроллера.</param>
    /// <param name="isPushing">Указывает, толкается ли игрок в данный момент.</param>
    /// <returns>Мощность толчка.</returns>
    private float CalculatePushPower(Vector3 leftDelta, Vector3 rightDelta, bool isPushing)
    {
        if (isPushing && (leftDelta.magnitude > speedController.minPushDistance || rightDelta.magnitude > speedController.minPushDistance)) // проверяю, достаточно ли большое движение для учета толчка
        {
            return (leftDelta.magnitude + rightDelta.magnitude) * speedController.acceleration; // рассчитываю мощность толчка
        }
        return 0f; // возвращаю нулевую мощность толчка, если движение слишком мало
    }
}