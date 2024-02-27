using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public int points = 10; // Очки, начисляемые за попадание в эту мишень

    public void RegisterHit()
    {
        // Добавляем очки за попадание
        ScoreManager.Instance.RegisterHit(points);
        // Дополнительные действия при попадании, например, анимация или звук
        gameObject.SetActive(false); // Деактивируем цель
    }

}
