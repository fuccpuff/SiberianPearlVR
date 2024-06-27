using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// ��������� UI �� ��������� ������.
/// </summary>
public class SpeedUIUpdater : MonoBehaviour
{
    private TMP_Text speedText; // ��������� ������ ��� ����������� ��������

    void Start()
    {
        StartCoroutine(FindSpeedTextObject()); // ������� ����� ���������� �������
    }

    /// <summary>
    /// ���� ������ ������ �� ���������.
    /// </summary>
    private IEnumerator FindSpeedTextObject()
    {
        while (speedText == null) // ���� ��������� ������ �� ������
        {
            GameObject obj = GameObject.FindWithTag("speedtext"); // ��� ������ � ����� "speedtext"
            if (obj != null)
            {
                speedText = obj.GetComponent<TMP_Text>(); // ������� ��������� TMP_Text
            }
            yield return new WaitForSeconds(0.5f); // ��� ���������� ����� ��������� ��������
        }
    }

    /// <summary>
    /// ��������� ����� ��������.
    /// </summary>
    /// <param name="speed">������� �������� ������.</param>
    public void UpdateSpeedText(float speed)
    {
        if (speedText != null) // ���� ��������� ������ ������
        {
            speedText.text = $"��������: {speed.ToString("F2")} �/�"; // �������� ����� ��������
        }
    }
}