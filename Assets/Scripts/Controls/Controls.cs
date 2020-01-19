// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""6f9738ff-fd2c-4bf3-b0b4-0f79cab243a3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""935f497d-fe4b-4620-b7a2-13105390c604"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""182c535a-6aa7-4b17-a2cc-95465b218ab9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""264fd7f0-d12f-4d32-84ae-5618d2df1cd9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""86a59acd-f570-4caf-95a3-2599d73a87e3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d623be63-47c3-495d-88a2-19099c7533e7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""b0903555-ec04-468e-9abd-d9279663bf81"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2f409c7b-e029-4309-bffa-0b0f62063793"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5c27f65b-2c29-44e2-8135-cfa9b531de5a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""18c3a9b5-c9e0-40d0-bd5e-164c37f38d82"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47071eb1-c4ed-42a3-b75f-5de3218e51a3"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TileSelector"",
            ""id"": ""a9374bdb-4d6c-44f9-ba6c-ab946974b318"",
            ""actions"": [
                {
                    ""name"": ""PressClick"",
                    ""type"": ""Button"",
                    ""id"": ""75071100-949f-4782-adae-0d00c48bbbe9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReleaseClick"",
                    ""type"": ""Button"",
                    ""id"": ""8032b223-0c63-4dde-9929-80d84f53dc1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e25adeb5-52f5-4e1b-b2ef-ebc32b0bd955"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5276de37-311f-408c-824b-5c0a7f60d317"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Core"",
            ""id"": ""8d6f76a8-7fd5-4b8d-a060-b403aa919b43"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""PassThrough"",
                    ""id"": ""24bd281d-d71d-4425-ade3-d71055d31299"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af3681de-2725-41c3-9e79-f9198e75bb7f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""874268d0-b0b9-4982-ad29-b33c440c9fb9"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f74b3458-5e87-4a59-894b-6483e8d33018"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eb6dca65-a720-41c0-afe8-73201d1a97d1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Dpad"",
                    ""id"": ""f9594167-cdcc-4038-bdf3-b4cd25ae3140"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7e6ab453-bc92-4859-aa34-0ce1a43d95c6"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""99a16600-8a51-45f4-b8a8-90d3eb5fbe1f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce641fe0-6c31-4f51-a972-29a2a565d439"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7e77e756-4fc8-4192-bea2-32d10b7ec352"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Toolbar"",
            ""id"": ""2a5f91d8-a086-45da-84f1-d2d0e7bd5766"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""5f24bce1-80e4-4e2e-b2aa-111b80bc63e8"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""96f1cbf5-3b62-41fc-9c4a-73df7aea0450"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""d6f5485c-fdf4-4654-8cca-dbc953681837"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0e72cee9-d97d-4226-8149-5f77c6caef1d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""99ccea08-ec0f-4b26-9ee3-584702537008"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        // TileSelector
        m_TileSelector = asset.FindActionMap("TileSelector", throwIfNotFound: true);
        m_TileSelector_PressClick = m_TileSelector.FindAction("PressClick", throwIfNotFound: true);
        m_TileSelector_ReleaseClick = m_TileSelector.FindAction("ReleaseClick", throwIfNotFound: true);
        // Core
        m_Core = asset.FindActionMap("Core", throwIfNotFound: true);
        m_Core_Position = m_Core.FindAction("Position", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Navigate = m_Inventory.FindAction("Navigate", throwIfNotFound: true);
        // Toolbar
        m_Toolbar = asset.FindActionMap("Toolbar", throwIfNotFound: true);
        m_Toolbar_Navigate = m_Toolbar.FindAction("Navigate", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // TileSelector
    private readonly InputActionMap m_TileSelector;
    private ITileSelectorActions m_TileSelectorActionsCallbackInterface;
    private readonly InputAction m_TileSelector_PressClick;
    private readonly InputAction m_TileSelector_ReleaseClick;
    public struct TileSelectorActions
    {
        private @Controls m_Wrapper;
        public TileSelectorActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PressClick => m_Wrapper.m_TileSelector_PressClick;
        public InputAction @ReleaseClick => m_Wrapper.m_TileSelector_ReleaseClick;
        public InputActionMap Get() { return m_Wrapper.m_TileSelector; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TileSelectorActions set) { return set.Get(); }
        public void SetCallbacks(ITileSelectorActions instance)
        {
            if (m_Wrapper.m_TileSelectorActionsCallbackInterface != null)
            {
                @PressClick.started -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnPressClick;
                @PressClick.performed -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnPressClick;
                @PressClick.canceled -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnPressClick;
                @ReleaseClick.started -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnReleaseClick;
                @ReleaseClick.performed -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnReleaseClick;
                @ReleaseClick.canceled -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnReleaseClick;
            }
            m_Wrapper.m_TileSelectorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PressClick.started += instance.OnPressClick;
                @PressClick.performed += instance.OnPressClick;
                @PressClick.canceled += instance.OnPressClick;
                @ReleaseClick.started += instance.OnReleaseClick;
                @ReleaseClick.performed += instance.OnReleaseClick;
                @ReleaseClick.canceled += instance.OnReleaseClick;
            }
        }
    }
    public TileSelectorActions @TileSelector => new TileSelectorActions(this);

    // Core
    private readonly InputActionMap m_Core;
    private ICoreActions m_CoreActionsCallbackInterface;
    private readonly InputAction m_Core_Position;
    public struct CoreActions
    {
        private @Controls m_Wrapper;
        public CoreActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Position => m_Wrapper.m_Core_Position;
        public InputActionMap Get() { return m_Wrapper.m_Core; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CoreActions set) { return set.Get(); }
        public void SetCallbacks(ICoreActions instance)
        {
            if (m_Wrapper.m_CoreActionsCallbackInterface != null)
            {
                @Position.started -= m_Wrapper.m_CoreActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_CoreActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_CoreActionsCallbackInterface.OnPosition;
            }
            m_Wrapper.m_CoreActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
            }
        }
    }
    public CoreActions @Core => new CoreActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_Navigate;
    public struct InventoryActions
    {
        private @Controls m_Wrapper;
        public InventoryActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Inventory_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // Toolbar
    private readonly InputActionMap m_Toolbar;
    private IToolbarActions m_ToolbarActionsCallbackInterface;
    private readonly InputAction m_Toolbar_Navigate;
    public struct ToolbarActions
    {
        private @Controls m_Wrapper;
        public ToolbarActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Toolbar_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_Toolbar; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ToolbarActions set) { return set.Get(); }
        public void SetCallbacks(IToolbarActions instance)
        {
            if (m_Wrapper.m_ToolbarActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_ToolbarActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_ToolbarActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_ToolbarActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_ToolbarActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public ToolbarActions @Toolbar => new ToolbarActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ITileSelectorActions
    {
        void OnPressClick(InputAction.CallbackContext context);
        void OnReleaseClick(InputAction.CallbackContext context);
    }
    public interface ICoreActions
    {
        void OnPosition(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnNavigate(InputAction.CallbackContext context);
    }
    public interface IToolbarActions
    {
        void OnNavigate(InputAction.CallbackContext context);
    }
}
