//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/Controller.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controller: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""actionsController"",
            ""id"": ""b145f46b-288e-4609-b1d4-b2d5a4b441fd"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""Value"",
                    ""id"": ""bc06aece-1aba-4398-aea0-eceabd96784c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0d4e2bb6-440c-44d7-81a5-33b67c74eddb"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // actionsController
        m_actionsController = asset.FindActionMap("actionsController", throwIfNotFound: true);
        m_actionsController_Touch = m_actionsController.FindAction("Touch", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // actionsController
    private readonly InputActionMap m_actionsController;
    private List<IActionsControllerActions> m_ActionsControllerActionsCallbackInterfaces = new List<IActionsControllerActions>();
    private readonly InputAction m_actionsController_Touch;
    public struct ActionsControllerActions
    {
        private @Controller m_Wrapper;
        public ActionsControllerActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch => m_Wrapper.m_actionsController_Touch;
        public InputActionMap Get() { return m_Wrapper.m_actionsController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsControllerActions set) { return set.Get(); }
        public void AddCallbacks(IActionsControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_ActionsControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActionsControllerActionsCallbackInterfaces.Add(instance);
            @Touch.started += instance.OnTouch;
            @Touch.performed += instance.OnTouch;
            @Touch.canceled += instance.OnTouch;
        }

        private void UnregisterCallbacks(IActionsControllerActions instance)
        {
            @Touch.started -= instance.OnTouch;
            @Touch.performed -= instance.OnTouch;
            @Touch.canceled -= instance.OnTouch;
        }

        public void RemoveCallbacks(IActionsControllerActions instance)
        {
            if (m_Wrapper.m_ActionsControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActionsControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_ActionsControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActionsControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActionsControllerActions @actionsController => new ActionsControllerActions(this);
    public interface IActionsControllerActions
    {
        void OnTouch(InputAction.CallbackContext context);
    }
}
