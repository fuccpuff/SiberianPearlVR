using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ToggleMenu(GameObject menuObject, bool show)
    {
        if (menuObject != null)
        {
            menuObject.SetActive(show);
        }
    }
}
