using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Обновляет UI со скоростью игрока.
/// </summary>
public class SpeedUIUpdater : MonoBehaviour
{
    private TMP_Text speedText; // текстовый объект для отображения скорости

    void Start()
    {
        StartCoroutine(FindSpeedTextObject()); // начинаю поиск текстового объекта
    }

    /// <summary>
    /// Ищет объект текста со скоростью.
    /// </summary>
    private IEnumerator FindSpeedTextObject()
    {
        while (speedText == null) // пока текстовый объект не найден
        {
            GameObject obj = GameObject.FindWithTag("speedtext"); // ищу объект с тегом "speedtext"
            if (obj != null)
            {
                speedText = obj.GetComponent<TMP_Text>(); // получаю компонент TMP_Text
            }
            yield return new WaitForSeconds(0.5f); // жду полсекунды перед следующей попыткой
        }
    }

    /// <summary>
    /// Обновляет текст скорости.
    /// </summary>
    /// <param name="speed">Текущая скорость игрока.</param>
    public void UpdateSpeedText(float speed)
    {
        if (speedText != null) // если текстовый объект найден
        {
            speedText.text = $"Скорость: {speed.ToString("F2")} м/с"; // обновляю текст скорости
        }
    }
}