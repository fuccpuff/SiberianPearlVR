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
        ScoreManager.OnScoreChanged += UpdateUI; // Подписка на событие изменения счета
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateUI; // Отписка от события
    }

    private void Start()
    {
        UpdateUI(); // Инициализационное обновление UI
    }

    private void UpdateUI()
    {
        if (ScoreManager.Instance == null) return;

        shotsText.text = $"Выстрелов: {ScoreManager.Instance.TotalShots}";
        hitsText.text = $"Попаданий: {ScoreManager.Instance.TotalHits}";
        scoreText.text = $"Очков: {ScoreManager.Instance.TotalScore}";
        accuracyText.text = $"Точность: {ScoreManager.Instance.GetAccuracy():F2}%";
    }
}
