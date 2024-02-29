using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text shotsText;
    public TMP_Text hitsText;
    public TMP_Text scoreText;
    public TMP_Text accuracyText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateUI; // �������� �� ������� ��������� �����
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateUI; // ������� �� �������
    }

    private void Start()
    {
        UpdateUI(); // ����������������� ���������� UI
    }

    private void UpdateUI()
    {
        if (ScoreManager.Instance == null) return;

        shotsText.text = $"���������: {ScoreManager.Instance.TotalShots}";
        hitsText.text = $"���������: {ScoreManager.Instance.TotalHits}";
        scoreText.text = $"�����: {ScoreManager.Instance.TotalScore}";
        accuracyText.text = $"��������: {ScoreManager.Instance.GetAccuracy():F2}%";
    }
}
