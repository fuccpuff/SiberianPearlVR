using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text shotsText;
    public TMP_Text hitsText;
    public TMP_Text scoreText;
    public TMP_Text accuracyText;
    public TMP_Text streakText;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        if (scoreManager == null) Debug.LogError("ScoreManager is not initialized!");

        ScoreManager.OnScoreChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        if (scoreManager == null) return;

        UpdateShotsText();
        UpdateHitsText();
        UpdateScoreText();
        UpdateAccuracyText();
        UpdateStreakText();
    }

    private void UpdateShotsText()
    {
        if (shotsText != null) shotsText.text = $"Выстрелов: {scoreManager.TotalShots}";
    }

    private void UpdateHitsText()
    {
        if (hitsText != null) hitsText.text = $"Попаданий: {scoreManager.TotalHits}";
    }

    private void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = $"Очков: {scoreManager.TotalScore}";
    }

    private void UpdateAccuracyText()
    {
        if (accuracyText != null) accuracyText.text = $"Точность: {scoreManager.GetAccuracy():F2}%";
    }

    private void UpdateStreakText() 
    {
        if (streakText != null) streakText.text = $"Серия: {scoreManager.LongestStreak}";
    }
}