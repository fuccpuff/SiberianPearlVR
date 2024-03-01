using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ToggleMenu(GameObject menuObject, bool show)
    {
        // checking if the menu object is null before trying to show or hide it
        if (menuObject == null)
        {
            // if it's null, i log an error because there's no menu object to toggle
            Debug.LogError("Menu object is null.");
            return;
        }

        // setting the active state of the menu object based on the show parameter
        menuObject.SetActive(show);
    }
}