using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuItemInteractable : MonoBehaviour
{
    protected XRBaseInteractable interactable;

    protected virtual void OnEnable()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    protected virtual void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        var menuItem = GetComponent<IMenuItem>();
        if (menuItem != null)
        {
            menuItem.Activate();
        }
        else
        {
            Debug.LogWarning("Component IMenuItem not find.", this);
        }
    }
}
