using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingRangeMenuItem : MonoBehaviour, IMenuItem
{
    public void Activate()
    {
        // logging that the shooting range menu item is activated
        Debug.Log("ShootingRange activated");
        // starting the coroutine to load the scene asynchronously
        StartCoroutine(LoadSceneAsync("ShootingRange"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // loading the scene asynchronously and checking if the operation is successful
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        if (asyncLoad == null)
        {
            // if the async operation is null, something went wrong
            Debug.LogError($"Failed to load scene: {sceneName}");
            yield break;
        }

        // waiting for the scene to fully load
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}