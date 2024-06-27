using UnityEngine;

/// <summary>
/// ������������ �������� ������ � ����������� �� ������� �����������.
/// </summary>
public class SlopeAdjuster : MonoBehaviour
{
    public LayerMask groundLayer; // ���� ����� ��� ��������
    private SpeedController speedController; // ������ �� ��������� SpeedController

    void Awake()
    {
        speedController = GetComponent<SpeedController>(); // ������������� ��������� SpeedController
    }

    /// <summary>
    /// ������������ �������� ������ � ����������� �� ������� �����������.
    /// </summary>
    /// <param name="transform">��������� ������� ������.</param>
    public void AdjustSpeedBySlope(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 3f, groundLayer)) // �������� ������� ����� ��� �������
        {
            Vector3 flatForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // �������� ������� ����������� ������
            float forwardSlopeAngle = Vector3.Angle(flatForward, hit.normal) - 90; // �������� ���� ������� ������
            speedController.AdjustSpeedBySlope(forwardSlopeAngle); // ����������� �������� �� ������ ���� �������
        }
    }
}