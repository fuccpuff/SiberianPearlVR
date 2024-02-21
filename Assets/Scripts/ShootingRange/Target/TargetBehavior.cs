using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public int points = 10; // Очки, начисляемые за попадание в эту мишень

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ScoreManager.Instance.RegisterHit(points); // Добавляем очки за попадание
            gameObject.SetActive(false);
        }
    }
}
