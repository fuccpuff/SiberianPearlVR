using System.Collections.Generic;
using UnityEngine;

public class SessionAnalytics : MonoBehaviour
{
    private List<int> sessionScores = new List<int>();
    private List<float> sessionAccuracies = new List<float>();

    public void RecordSession()
    {
        // Используем свойство TotalScore вместо метода GetTotalScore()
        sessionScores.Add(ScoreManager.Instance.TotalScore);
        sessionAccuracies.Add(ScoreManager.Instance.GetAccuracy());
    }

    public void DisplaySessionHistory()
    {
        for (int i = 0; i < sessionScores.Count; i++)
        {
            Debug.Log($"Сессия {i + 1}: Очки - {sessionScores[i]}, Точность - {sessionAccuracies[i]:F2}%");
        }
    }
}
