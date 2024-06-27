using UnityEngine;

/// <summary>
/// ��������� ��������� ������, ������� ��������� � ����������.
/// </summary>
public class SpeedController : MonoBehaviour
{
    [Tooltip("������������ ��������, ������� ����� ������� �����.")]
    public float maxSpeed = 20f; // ������������ �������� ������

    [Tooltip("�������� ��������� ��� ������.")]
    public float acceleration = 0.2f; // �������� ��������� ��� ������

    [Tooltip("�������� ����������, ����� ����� �� ���������.")]
    public float noPushDeceleration = 0.02f; // �������� ���������� ��� ������

    [Tooltip("����������� ���������� �������� ����������� ��� ����� ������.")]
    public float minPushDistance = 0.01f; // ����������� ���������� �������� ����������� ��� ����� ������

    private float speed = 0f; // ������� �������� ������
    private float lastPushTime; // ����� ���������� ������

    /// <summary>
    /// ���������� ������� ��������.
    /// </summary>
    public float Speed => speed; // ��������� ������� ��������

    /// <summary>
    /// ��������� �������� ������ �� ������ �������� ������ � ������� ������.
    /// </summary>
    /// <param name="pushPower">������������ �������� ������.</param>
    /// <param name="isPushing">���������, ��������� �� ����� � ������ ������.</param>
    public void UpdateSpeed(float pushPower, bool isPushing)
    {
        if (isPushing) // ���� ����� ���������
        {
            speed += pushPower * Time.deltaTime; // ���������� �������� �� �������� ������, �������� ������ �������
            lastPushTime = Time.time; // �������� ����� ���������� ������
        }
        else // ���� ����� �� ���������
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0); // �������� �������� � ������ ����������
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed); // ����������� �������� � ��������� �� 0 �� maxSpeed
    }

    /// <summary>
    /// ��������� �������� �� ������ ������� ��������.
    /// </summary>
    /// <param name="transform">��������� ������� ������.</param>
    public void ApplyMovement(Transform transform)
    {
        Vector3 direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // �������� ����������� ��������
        transform.position += direction * speed * Time.deltaTime; // ������� ������� ������� �� ������ �������� � �����������
    }

    /// <summary>
    /// ������������ �������� ������ � ����������� �� ������� �����������.
    /// </summary>
    /// <param name="forwardSlopeAngle">���� ������� ������.</param>
    public void AdjustSpeedBySlope(float forwardSlopeAngle)
    {
        if (forwardSlopeAngle < 0) // ���� ������ ������
        {
            speed += Mathf.Abs(forwardSlopeAngle) * acceleration * Time.deltaTime; // ���������� �������� �� ������ �������
        }
        else if (Time.time - lastPushTime > 1f) // ���� ������ ������ ������� ����� ���������� ������
        {
            speed = Mathf.Max(speed - noPushDeceleration * Time.deltaTime, 0); // �������� �������� � ������ ����������
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed); // ����������� �������� � ��������� �� 0 �� maxSpeed
    }
}