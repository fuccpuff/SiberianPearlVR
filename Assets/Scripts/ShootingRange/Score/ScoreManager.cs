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
    }

    public void RegisterHit(int points)
    {
        totalHits++;
        totalScore += Mathf.RoundToInt(points * comboMultiplier);
        currentStreak++;
        longestStreak = Mathf.Max(longestStreak, currentStreak);
        comboMultiplier = 1 + (currentStreak / 10f);

        if (isChallengeActive) hitsDuringChallenge++;

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

    private void UpdateAccuracy()
    {
        float accuracy = (totalHits / (float)totalShots) * 100;
        Debug.Log($"Выстрелов: {totalShots}, Попаданий: {totalHits}, Очков: {totalScore}, Точность: {accuracy:F2}%");
    }

    public int GetTotalScore() => totalScore;

    public float GetAccuracy() => (totalHits / (float)totalShots) * 100;

    public void AddScore(int score)
    {
        totalScore += score;
    }
}
