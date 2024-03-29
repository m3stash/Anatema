// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/LayoutControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @LayoutControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @LayoutControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LayoutControls"",
    ""maps"": [
        {
            ""name"": ""Inventory"",
            ""id"": ""229576ad-29a1-42f6-a2a6-5f7d263f037a"",
            ""actions"": [
                {
                    ""name"": ""SwitchDisplay"",
                    ""type"": ""Button"",
                    ""id"": ""ff039720-cfe7-4822-b69b-c9292647ff5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""01bfaff2-adbc-4813-b127-4e11969db6da"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchDisplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69881bf8-5491-46c7-ace7-c875203c36af"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchDisplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c591f06e-6861-4880-9326-fdf2f3a99d24"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchDisplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""EscapeMenu"",
            ""id"": ""56bff35e-b111-4352-9262-003ed19b157d"",
            ""actions"": [
                {
                    ""name"": ""SwitchDisplay"",
                    ""type"": ""Button"",
                    ""id"": ""c5059342-5b5f-4d05-bd5c-4c0efc3cd461"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""880e0ccc-2360-4723-88ab-37e4504aa393"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchDisplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdfbec68-5971-4b47-9e4c-ca669afaeb57"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchDisplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa16732b-f080-4641-81a5-9dbb807e78a6"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchDisplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_SwitchDisplay = m_Inventory.FindAction("SwitchDisplay", throwIfNotFound: true);
        // EscapeMenu
        m_EscapeMenu = asset.FindActionMap("EscapeMenu", throwIfNotFound: true);
        m_EscapeMenu_SwitchDisplay = m_EscapeMenu.FindAction("SwitchDisplay", throwIfNotFound: true);
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

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_SwitchDisplay;
    public struct InventoryActions
    {
        private @LayoutControls m_Wrapper;
        public InventoryActions(@LayoutControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwitchDisplay => m_Wrapper.m_Inventory_SwitchDisplay;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @SwitchDisplay.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSwitchDisplay;
                @SwitchDisplay.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSwitchDisplay;
                @SwitchDisplay.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSwitchDisplay;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwitchDisplay.started += instance.OnSwitchDisplay;
                @SwitchDisplay.performed += instance.OnSwitchDisplay;
                @SwitchDisplay.canceled += instance.OnSwitchDisplay;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // EscapeMenu
    private readonly InputActionMap m_EscapeMenu;
    private IEscapeMenuActions m_EscapeMenuActionsCallbackInterface;
    private readonly InputAction m_EscapeMenu_SwitchDisplay;
    public struct EscapeMenuActions
    {
        private @LayoutControls m_Wrapper;
        public EscapeMenuActions(@LayoutControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwitchDisplay => m_Wrapper.m_EscapeMenu_SwitchDisplay;
        public InputActionMap Get() { return m_Wrapper.m_EscapeMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EscapeMenuActions set) { return set.Get(); }
        public void SetCallbacks(IEscapeMenuActions instance)
        {
            if (m_Wrapper.m_EscapeMenuActionsCallbackInterface != null)
            {
                @SwitchDisplay.started -= m_Wrapper.m_EscapeMenuActionsCallbackInterface.OnSwitchDisplay;
                @SwitchDisplay.performed -= m_Wrapper.m_EscapeMenuActionsCallbackInterface.OnSwitchDisplay;
                @SwitchDisplay.canceled -= m_Wrapper.m_EscapeMenuActionsCallbackInterface.OnSwitchDisplay;
            }
            m_Wrapper.m_EscapeMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwitchDisplay.started += instance.OnSwitchDisplay;
                @SwitchDisplay.performed += instance.OnSwitchDisplay;
                @SwitchDisplay.canceled += instance.OnSwitchDisplay;
            }
        }
    }
    public EscapeMenuActions @EscapeMenu => new EscapeMenuActions(this);
    public interface IInventoryActions
    {
        void OnSwitchDisplay(InputAction.CallbackContext context);
    }
    public interface IEscapeMenuActions
    {
        void OnSwitchDisplay(InputAction.CallbackContext context);
    }
}
