using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public int points = 10;

    public void RegisterHit()
    {
        ScoreManager.Instance.RegisterHit(points);
        Destroy(gameObject);
    }
}
