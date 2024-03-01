using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingRangeMenuItem : MonoBehaviour, IMenuItem
{
    public void Activate()
    {
        Debug.Log("ShootingRange activated");
        StartCoroutine(LoadSceneAsync("ShootingRange"));
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