using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;


public class ResetAllBindings : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    public void ResetBindings()
    {
        foreach (InputActionMap map in inputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("rebinds");
        EventSystem.current.SetSelectedGameObject(null);
    }
}
