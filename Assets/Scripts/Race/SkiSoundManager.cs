using UnityEngine;

/// <summary>
/// ��������� ������ ����� � ����������� �� ��������.
/// </summary>
public class SkiSoundManager : MonoBehaviour
{
    public AudioSource windSound; // ���� �����

    private SpeedController speedController; // ������ �� ��������� SpeedController

    void Awake()
    {
        speedController = GetComponent<SpeedController>(); // ������������� ��������� SpeedController
    }

    public void Update()
    {
        float speed = speedController.Speed; // ������� ������� ��������
        AdjustWindSound(speed); // ����������� ���� �����
    }

    private void AdjustWindSound(float speed)
    {
        float targetVolume = Mathf.Clamp01(speed / speedController.maxSpeed); // ������������ ��������� ����� ����� � ����������� �� ��������
        if (!windSound.isPlaying && targetVolume > 0) // ���� ���� �� �������������, �� ������
        {
            windSound.Play(); // �������� ����
        }
        else if (windSound.isPlaying && targetVolume == 0) // ���� ���� �������������, �� �� ������
        {
            windSound.Stop(); // ������������ ����
        }
        windSound.volume = targetVolume; // ������������ ���������
    }
}