using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceMenuItem : MonoBehaviour, IMenuItem
{
    public void Activate()
    {
        Debug.Log("Race activated");
        StartCoroutine(LoadSceneAsync("Race"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}