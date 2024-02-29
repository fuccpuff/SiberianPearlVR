using System.Collections;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static event Action OnScoreChanged;

    private int totalShots = 0;
    private int totalHits = 0;
    private int totalScore = 0;
    private int currentStreak = 0;
    private int longestStreak = 0;
    private float comboMultiplier = 1;
    private bool isChallengeActive = false;
    private int hitsDuringChallenge = 0;
    private int challengeTargetHits = 5;
    private float challengeTime = 30f;

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
        if (currentStreak > 0 && totalShots > totalHits)
        {
            ResetStreak();
        }
        Debug.Log($"Shot registered. Total shots: {totalShots}");
        OnScoreChanged?.Invoke();
    }

    public void RegisterHit(int points)
    {
        totalHits++;
        UpdateComboMultiplier();
        totalScore += Mathf.RoundToInt(points * comboMultiplier);
        currentStreak++;
        longestStreak = Mathf.Max(longestStreak, currentStreak);

        Debug.Log($"Hit registered. Total hits: {totalHits}, Current Streak: {currentStreak}, Combo Multiplier: {comboMultiplier}");

        if (isChallengeActive)
        {
            hitsDuringChallenge++;
            Debug.Log($"Challenge Hit! Hits during challenge: {hitsDuringChallenge}/{challengeTargetHits}");
        }

        OnScoreChanged?.Invoke();
    }

    private void StartChallenge()
    {
        if (isChallengeActive)
        {
            Debug.Log("Challenge already active.");
            return;
        }
        hitsDuringChallenge = 0;
        isChallengeActive = true;
        Debug.Log("Challenge started.");
        StartCoroutine(ChallengeTimer(challengeTime));
    }

    IEnumerator ChallengeTimer(float time)
    {
        Debug.Log($"Challenge will end in {time} seconds.");
        yield return new WaitForSeconds(time);
        EndChallenge();
    }

    private void EndChallenge()
    {
        if (!isChallengeActive)
        {
            Debug.Log("Challenge wasn't active.");
            return;
        }
        Debug.Log($"Challenge ended. Hits during challenge: {hitsDuringChallenge}");
        if (hitsDuringChallenge >= challengeTargetHits)
        {
            AddScore(100);
            Debug.Log("Challenge Completed: +100 Score Added");
        }
        else
        {
            Debug.Log("Challenge failed: Not enough hits.");
        }
        isChallengeActive = false;
        hitsDuringChallenge = 0;
        OnScoreChanged?.Invoke();
    }

    private void ResetStreak()
    {
        currentStreak = 0;
        comboMultiplier = 1;
        Debug.Log("Streak reset.");
    }

    private void UpdateComboMultiplier()
    {
        comboMultiplier = 1 + (currentStreak / 10f);
    }

    public float GetAccuracy() => totalShots > 0 ? (totalHits / (float)totalShots) * 100 : 0;

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"Score added: {score}. Total score: {totalScore}");
        OnScoreChanged?.Invoke();
    }

    public int TotalShots => totalShots;
    public int TotalHits => totalHits;
    public int TotalScore => totalScore;
    public int LongestStreak => longestStreak;
}
