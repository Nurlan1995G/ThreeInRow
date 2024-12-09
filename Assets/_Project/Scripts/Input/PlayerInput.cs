//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/_Project/Scripts/Input/PlayerInput.inputactions
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

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Clicked"",
            ""id"": ""90e6a501-f4b6-4e4e-97ad-a7ca312a33e7"",
            ""actions"": [
                {
                    ""name"": ""MouseTap"",
                    ""type"": ""Button"",
                    ""id"": ""f105eeb9-790f-4527-bcef-74993ceb0ea5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dd471369-8913-43e2-91ec-596b58e1f49d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Clicked
        m_Clicked = asset.FindActionMap("Clicked", throwIfNotFound: true);
        m_Clicked_MouseTap = m_Clicked.FindAction("MouseTap", throwIfNotFound: true);
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

    // Clicked
    private readonly InputActionMap m_Clicked;
    private List<IClickedActions> m_ClickedActionsCallbackInterfaces = new List<IClickedActions>();
    private readonly InputAction m_Clicked_MouseTap;
    public struct ClickedActions
    {
        private @PlayerInput m_Wrapper;
        public ClickedActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseTap => m_Wrapper.m_Clicked_MouseTap;
        public InputActionMap Get() { return m_Wrapper.m_Clicked; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ClickedActions set) { return set.Get(); }
        public void AddCallbacks(IClickedActions instance)
        {
            if (instance == null || m_Wrapper.m_ClickedActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ClickedActionsCallbackInterfaces.Add(instance);
            @MouseTap.started += instance.OnMouseTap;
            @MouseTap.performed += instance.OnMouseTap;
            @MouseTap.canceled += instance.OnMouseTap;
        }

        private void UnregisterCallbacks(IClickedActions instance)
        {
            @MouseTap.started -= instance.OnMouseTap;
            @MouseTap.performed -= instance.OnMouseTap;
            @MouseTap.canceled -= instance.OnMouseTap;
        }

        public void RemoveCallbacks(IClickedActions instance)
        {
            if (m_Wrapper.m_ClickedActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IClickedActions instance)
        {
            foreach (var item in m_Wrapper.m_ClickedActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ClickedActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ClickedActions @Clicked => new ClickedActions(this);
    public interface IClickedActions
    {
        void OnMouseTap(InputAction.CallbackContext context);
    }
}
