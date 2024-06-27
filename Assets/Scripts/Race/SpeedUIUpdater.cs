using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// updates the ui with the player's speed.
/// </summary>
public class SpeedUIUpdater : MonoBehaviour
{
    private TMP_Text speedText; // text object for displaying speed

    void Start()
    {
        StartCoroutine(FindSpeedTextObject()); // start searching for the text object
    }

    /// <summary>
    /// searches for the speed text object.
    /// </summary>
    private IEnumerator FindSpeedTextObject()
    {
        while (speedText == null) // while the text object is not found
        {
            GameObject obj = GameObject.FindWithTag("speedtext"); // search for the object with the tag "speedtext"
            if (obj != null)
            {
                speedText = obj.GetComponent<TMP_Text>(); // get the TMP_Text component
            }
            yield return new WaitForSeconds(0.5f); // wait half a second before the next attempt
        }
    }

    /// <summary>
    /// updates the speed text.
    /// </summary>
    /// <param name="speed">the current speed of the player.</param>
    public void UpdateSpeedText(float speed)
    {
        if (speedText != null) // if the text object is found
        {
            speedText.text = $"Скорость: {speed.ToString("F2")} m/s"; // update the speed text
        }
    }
}