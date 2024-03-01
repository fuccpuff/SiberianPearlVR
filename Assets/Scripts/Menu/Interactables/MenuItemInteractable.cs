using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuItemInteractable : MonoBehaviour
{
    protected XRBaseInteractable interactable;

    protected virtual void OnEnable()
    {
        // getting the XRBaseInteractable component from the game object
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            // if it's not null, i add the listener for select entered
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            // log an error if the XRBaseInteractable component isn't found
            Debug.LogError("XRBaseInteractable component not found.", this);
        }
    }

    protected virtual void OnDisable()
    {
        // removing the listener when the object is disabled, but first checking if interactable is not null
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // trying to get the IMenuItem component from the game object
        var menuItem = GetComponent<IMenuItem>();
        if (menuItem != null)
        {
            // if i find it, i activate the menu item
            menuItem.Activate();
        }
        else
        {
            // warning if the IMenuItem component isn't found
            Debug.LogWarning("IMenuItem component not found.", this);
        }
    }
}