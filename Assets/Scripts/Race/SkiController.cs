using UnityEngine;

public class SkiController : MonoBehaviour
{
    public GameObject leftControllerObject; // ������ ������ �����������
    public GameObject rightControllerObject; // ������ ������� �����������

    private Vector3 lastLeftPosition; // ��������� ������� ������ �����������
    private Vector3 lastRightPosition; // ��������� ������� ������� �����������

    private SpeedController speedController; // ��������� ��� ���������� ���������
    private SlopeAdjuster slopeAdjuster; // ��������� ��� ������������� �������� �� �������
    private TerrainClipPrevention terrainClipPrevention; // ��������� ��� �������������� ������������ ����� �����������
    private SpeedUIUpdater speedUIUpdater; // ��������� ��� ���������� UI ��������
    private SkiSoundManager skiSoundManager; // ��������� ��� ���������� ������ �����
    private MenuController menuController; // ��������� ��� ���������� ����

    void Start()
    {
        lastLeftPosition = leftControllerObject.transform.position; // �������� ��������� ������� ������ �����������
        lastRightPosition = rightControllerObject.transform.position; // �������� ��������� ������� ������� �����������

        speedController = GetComponent<SpeedController>(); // ������� ��������� SpeedController
        slopeAdjuster = GetComponent<SlopeAdjuster>(); // ������� ��������� SlopeAdjuster
        terrainClipPrevention = GetComponent<TerrainClipPrevention>(); // ������� ��������� TerrainClipPrevention
        speedUIUpdater = GetComponent<SpeedUIUpdater>(); // ������� ��������� SpeedUIUpdater
        skiSoundManager = GetComponent<SkiSoundManager>(); // ������� ��������� SkiSoundManager
        menuController = GetComponent<MenuController>(); // ������� ��������� MenuController
    }

    void Update()
    {
        Vector3 leftDelta = leftControllerObject.transform.position - lastLeftPosition; // �������� ��������� ������� ������ �����������
        Vector3 rightDelta = rightControllerObject.transform.position - lastRightPosition; // �������� ��������� ������� ������� �����������

        float leftDirection = Vector3.Dot(leftDelta.normalized, -Camera.main.transform.forward); // ����������� �������� ������ �����������
        float rightDirection = Vector3.Dot(rightDelta.normalized, -Camera.main.transform.forward); // ����������� �������� ������� �����������

        lastLeftPosition = leftControllerObject.transform.position; // �������� ��������� ������� ������ �����������
        lastRightPosition = rightControllerObject.transform.position; // �������� ��������� ������� ������� �����������

        bool isPushing = leftDirection > 0 && rightDirection > 0; // ���������, ��������� �� �����
        float pushPower = CalculatePushPower(leftDelta, rightDelta, isPushing); // ����������� �������� ������

        speedController.UpdateSpeed(pushPower, isPushing); // �������� �������� � ������ ������ ��� ��� ����������

        speedController.ApplyMovement(transform); // �������� �������� �� ������ ������� ��������
        slopeAdjuster.AdjustSpeedBySlope(transform); // ����������� �������� �� ������� �����������
        terrainClipPrevention.PreventTerrainClip(transform); // ������������ ������������ ����� �����������
        speedUIUpdater.UpdateSpeedText(speedController.Speed); // �������� UI ��������
        skiSoundManager.Update(); // �������� ���� �����
        menuController.Update(); // �������� ���������� ����
    }

    /// <summary>
    /// ������������ �������� ������ �� ������ ��������� ������� ������������.
    /// </summary>
    /// <param name="leftDelta">��������� ������� ������ �����������.</param>
    /// <param name="rightDelta">��������� ������� ������� �����������.</param>
    /// <param name="isPushing">���������, ��������� �� ����� � ������ ������.</param>
    /// <returns>�������� ������.</returns>
    private float CalculatePushPower(Vector3 leftDelta, Vector3 rightDelta, bool isPushing)
    {
        if (isPushing && (leftDelta.magnitude > speedController.minPushDistance || rightDelta.magnitude > speedController.minPushDistance)) // ��������, ���������� �� ������� �������� ��� ����� ������
        {
            return (leftDelta.magnitude + rightDelta.magnitude) * speedController.acceleration; // ����������� �������� ������
        }
        return 0f; // ��������� ������� �������� ������, ���� �������� ������� ����
    }
}