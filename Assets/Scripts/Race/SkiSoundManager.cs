using UnityEngine;

/// <summary>
/// Управляет звуком ветра в зависимости от скорости.
/// </summary>
public class SkiSoundManager : MonoBehaviour
{
    public AudioSource windSound; // звук ветра

    private SpeedController speedController; // ссылка на компонент SpeedController

    void Awake()
    {
        speedController = GetComponent<SpeedController>(); // инициализирую компонент SpeedController
    }

    public void Update()
    {
        float speed = speedController.Speed; // получаю текущую скорость
        AdjustWindSound(speed); // корректирую звук ветра
    }

    private void AdjustWindSound(float speed)
    {
        float targetVolume = Mathf.Clamp01(speed / speedController.maxSpeed); // устанавливаю громкость звука ветра в зависимости от скорости
        if (!windSound.isPlaying && targetVolume > 0) // если звук не проигрывается, но должен
        {
            windSound.Play(); // запускаю звук
        }
        else if (windSound.isPlaying && targetVolume == 0) // если звук проигрывается, но не должен
        {
            windSound.Stop(); // останавливаю звук
        }
        windSound.volume = targetVolume; // устанавливаю громкость
    }
}