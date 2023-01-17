using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class RebindingControls : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedButton;

    [SerializeField]
    private TMP_Text buttonText;

    [SerializeField]
    private InputActionReference actionReference;

    [SerializeField]
    private int bindingIndex;

    MessageBrokerImpl broker = MessageBrokerImpl.Instance;

    public static event ButtonTextHandler ButtonTextHandler;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Awake()
    {
        UpdateButtonText();
    }
    private void EventHandlerButtonText(MessagePayload<bool> flag)
    {
        if (flag.payload)
        {
            buttonText.text = InputControlPath.ToHumanReadableString(actionReference.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else
        {
            return;
        }
    }

    public void UpdateButtonText()
    {
        Action<MessagePayload<bool>> actionUpdateButtonText = EventHandlerButtonText;
        broker.Subscribe<bool>(actionUpdateButtonText);
    }

    public void StartRebiding()
    {
        EventSystem.current.SetSelectedGameObject(null);

        buttonText.text = "PRESS NEW BINDING";

        PerformRebind(actionReference.action, bindingIndex);

        actionReference.action.Enable();
    }

    private void PerformRebind(InputAction action, int bindingIndex)
    {
        actionReference.action.Disable();

        rebindingOperation = action.PerformInteractiveRebinding(bindingIndex)

                     .OnMatchWaitForAnother(0.1f)
                     .WithControlsExcluding("<Mouse>/leftButton")
                     .WithControlsExcluding("<Mouse>/rightButton")
                     .WithControlsExcluding("<Mouse>/press")
                     .WithControlsExcluding("<Pointer>/position")
                     .WithCancelingThrough("<Keyboard>/escape")
                     .OnComplete(
                         operation =>
                         {
                             if (CheckDuplicateBindings(action, bindingIndex))
                             {
                                 action.RemoveBindingOverride(bindingIndex);
                                 rebindingOperation.Dispose();
                                 PerformRebind(action, bindingIndex);

                             }

                             buttonText.text = InputControlPath.ToHumanReadableString(action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

                             rebindingOperation.Dispose();
                             EventSystem.current.SetSelectedGameObject(null);
                         }
                     )
                     .Start();
    }

    private bool CheckDuplicateBindings(InputAction action, int bindingIndex)
    {
        InputBinding newBinding = action.bindings[bindingIndex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            if (binding.action == newBinding.action)
            {
                continue;
            }
            if (binding.effectivePath == newBinding.effectivePath)
            {
                Debug.Log("Duplicate binding found: " + newBinding.effectivePath);
                return true;
            }
        }
        return false;
    }



}
