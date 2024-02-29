using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int totalShots = 0;
    private int totalHits = 0;
    private int totalScore = 0;
    private int currentStreak = 0;
    private int longestStreak = 0;
    private float comboMultiplier = 1;
    private bool isChallengeActive = false;
    private int hitsDuringChallenge = 0;
    private int challengeTargetHits = 5;
    private float challengeTime = 10f;

    // Определение события изменения счета
    public static event Action OnScoreChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterShot()
    {
        totalShots++;
        UpdateAccuracy();

        if (currentStreak > 0 && totalShots > totalHits)
        {
            currentStreak = 0;
            comboMultiplier = 1;
        }
        OnScoreChanged?.Invoke(); // Вызов события при изменении счета
    }

    public void RegisterHit(int points)
    {
        totalHits++;
        totalScore += Mathf.RoundToInt(points * comboMultiplier);
        currentStreak++;
        longestStreak = Mathf.Max(longestStreak, currentStreak);
        comboMultiplier = 1 + (currentStreak / 10f);

        if (isChallengeActive) hitsDuringChallenge++;

        OnScoreChanged?.Invoke(); // Вызов события при изменении счета
        UpdateAccuracy();
    }

    public void StartChallenge()
    {
        isChallengeActive = true;
        hitsDuringChallenge = 0;
        Invoke(nameof(EndChallenge), challengeTime);
    }

    private void EndChallenge()
    {
        isChallengeActive = false;
        if (hitsDuringChallenge >= challengeTargetHits)
        {
            AddScore(100); // Награда за выполнение задачи
        }
    }

    public int TotalShots => totalShots;
    public int TotalHits => totalHits;
    public int TotalScore => totalScore;

    public float GetAccuracy() => totalShots > 0 ? (totalHits / (float)totalShots) * 100 : 0;

    public void AddScore(int score)
    {
        totalScore += score;
        OnScoreChanged?.Invoke(); // Вызов события при изменении счета
    }

    private void UpdateAccuracy()
    {
        // Логика обновления точности теперь встроена в другие методы
    }
}
