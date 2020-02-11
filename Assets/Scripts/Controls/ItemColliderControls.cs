// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/ItemColliderControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ItemColliderControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @ItemColliderControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ItemColliderControls"",
    ""maps"": [
        {
            ""name"": ""ItemColliderTool"",
            ""id"": ""a3a88a8a-28e3-4b8b-93cb-bfeda042a72f"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""78192421-da24-4313-9c97-02d8877b7871"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a6dc4d26-5d9e-4393-94c6-45d5d7d54904"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""37adf688-f2be-45ed-8fa4-b7d8a55aeab0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8771cc96-3b07-4559-a3c1-da53dabcc62f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ItemColliderTool
        m_ItemColliderTool = asset.FindActionMap("ItemColliderTool", throwIfNotFound: true);
        m_ItemColliderTool_Click = m_ItemColliderTool.FindAction("Click", throwIfNotFound: true);
        m_ItemColliderTool_MousePosition = m_ItemColliderTool.FindAction("MousePosition", throwIfNotFound: true);
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

    // ItemColliderTool
    private readonly InputActionMap m_ItemColliderTool;
    private IItemColliderToolActions m_ItemColliderToolActionsCallbackInterface;
    private readonly InputAction m_ItemColliderTool_Click;
    private readonly InputAction m_ItemColliderTool_MousePosition;
    public struct ItemColliderToolActions
    {
        private @ItemColliderControls m_Wrapper;
        public ItemColliderToolActions(@ItemColliderControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_ItemColliderTool_Click;
        public InputAction @MousePosition => m_Wrapper.m_ItemColliderTool_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_ItemColliderTool; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ItemColliderToolActions set) { return set.Get(); }
        public void SetCallbacks(IItemColliderToolActions instance)
        {
            if (m_Wrapper.m_ItemColliderToolActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_ItemColliderToolActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_ItemColliderToolActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_ItemColliderToolActionsCallbackInterface.OnClick;
                @MousePosition.started -= m_Wrapper.m_ItemColliderToolActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_ItemColliderToolActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_ItemColliderToolActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_ItemColliderToolActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public ItemColliderToolActions @ItemColliderTool => new ItemColliderToolActions(this);
    public interface IItemColliderToolActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
