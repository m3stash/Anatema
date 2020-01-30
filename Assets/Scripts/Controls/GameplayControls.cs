// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/GameplayControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameplayControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @GameplayControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameplayControls"",
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
                    ""processors"": """",
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
                    ""processors"": """",
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
                },
                {
                    ""name"": ""StepperChanged"",
                    ""type"": ""Button"",
                    ""id"": ""2f19f8d8-f3e0-4e78-9c46-1fdddade137d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""a38875cb-46d7-4b64-ae4c-91b42a958880"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DeleteItem"",
                    ""type"": ""Button"",
                    ""id"": ""ac193474-56ac-401d-87db-c2bd4372276f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""472aa6c8-e8fb-4300-8988-e2924bc9803c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c7cadf06-4808-4125-8e1e-7111d7fd078a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Cross"",
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
                    ""path"": ""<Gamepad>/dpad/up"",
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
                    ""path"": ""<Gamepad>/dpad/down"",
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
                    ""path"": ""<Gamepad>/dpad/left"",
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
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Bumpers"",
                    ""id"": ""426b28d3-6f28-4e75-ae40-5ff41b54e0f0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StepperChanged"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5ec7b2a9-c6f7-433f-adf4-09792174cc90"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StepperChanged"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""54c9f01b-8842-407a-9d0b-e3cd223613c9"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StepperChanged"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""73f5e417-6c1e-47a2-86b0-e5bb66bfa9d3"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e83a8e2a-e03e-4411-a9d6-8f8c54600986"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeleteItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf747fe0-d229-4c1e-805d-02ff37a5057c"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5df03f6-b6a1-4fd8-a04f-153707c967aa"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                    ""name"": ""Cross"",
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
                    ""path"": ""<Gamepad>/dpad/left"",
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
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""b0b01e61-6239-41ad-a053-d1cb5ddf4d2a"",
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
                    ""id"": ""c8f20393-6fb2-4d51-9c91-221dc940b566"",
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
                    ""id"": ""3632e2a4-edbb-4d99-bee7-2a55fbbc0a3b"",
                    ""path"": ""<Keyboard>/rightArrow"",
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
            ""name"": ""Shortcuts"",
            ""id"": ""8414608b-3d15-4892-9d5f-0f5cebb00738"",
            ""actions"": [
                {
                    ""name"": ""build"",
                    ""type"": ""Button"",
                    ""id"": ""b5a83ac2-17c0-4024-85e7-3260ed582bca"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""weapon"",
                    ""type"": ""Button"",
                    ""id"": ""f873691e-a180-4c62-b60b-75bf05252eb0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""potion"",
                    ""type"": ""Button"",
                    ""id"": ""7608f460-b7a4-4d64-82d2-a3fb3a498c70"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""tool"",
                    ""type"": ""Button"",
                    ""id"": ""6d62c83b-404a-4ad3-9f76-a9b15460991e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""50e0224b-19bf-49bf-a21b-d1b63574d232"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a84caaa9-095e-4610-a3c7-a48529de1482"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6357e9dd-6f96-4772-80d7-72ae981b496d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f27b443-e7d8-45fe-9686-232e77affd48"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""240baf21-ddc9-48e5-a9ae-61bfe21e36b7"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""potion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81f99ca6-9ae5-4087-b896-6d167ec007ef"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""potion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9f4cec9-c534-4393-b5ce-848a0308b5f7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12d5e607-c953-4016-b784-0d41cadcf739"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        m_Inventory_StepperChanged = m_Inventory.FindAction("StepperChanged", throwIfNotFound: true);
        m_Inventory_DropItem = m_Inventory.FindAction("DropItem", throwIfNotFound: true);
        m_Inventory_DeleteItem = m_Inventory.FindAction("DeleteItem", throwIfNotFound: true);
        m_Inventory_Cancel = m_Inventory.FindAction("Cancel", throwIfNotFound: true);
        m_Inventory_Interact = m_Inventory.FindAction("Interact", throwIfNotFound: true);
        // Toolbar
        m_Toolbar = asset.FindActionMap("Toolbar", throwIfNotFound: true);
        m_Toolbar_Navigate = m_Toolbar.FindAction("Navigate", throwIfNotFound: true);
        // Shortcuts
        m_Shortcuts = asset.FindActionMap("Shortcuts", throwIfNotFound: true);
        m_Shortcuts_build = m_Shortcuts.FindAction("build", throwIfNotFound: true);
        m_Shortcuts_weapon = m_Shortcuts.FindAction("weapon", throwIfNotFound: true);
        m_Shortcuts_potion = m_Shortcuts.FindAction("potion", throwIfNotFound: true);
        m_Shortcuts_tool = m_Shortcuts.FindAction("tool", throwIfNotFound: true);
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
        private @GameplayControls m_Wrapper;
        public PlayerActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
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
        private @GameplayControls m_Wrapper;
        public TileSelectorActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
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
        private @GameplayControls m_Wrapper;
        public CoreActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
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
    private readonly InputAction m_Inventory_StepperChanged;
    private readonly InputAction m_Inventory_DropItem;
    private readonly InputAction m_Inventory_DeleteItem;
    private readonly InputAction m_Inventory_Cancel;
    private readonly InputAction m_Inventory_Interact;
    public struct InventoryActions
    {
        private @GameplayControls m_Wrapper;
        public InventoryActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Inventory_Navigate;
        public InputAction @StepperChanged => m_Wrapper.m_Inventory_StepperChanged;
        public InputAction @DropItem => m_Wrapper.m_Inventory_DropItem;
        public InputAction @DeleteItem => m_Wrapper.m_Inventory_DeleteItem;
        public InputAction @Cancel => m_Wrapper.m_Inventory_Cancel;
        public InputAction @Interact => m_Wrapper.m_Inventory_Interact;
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
                @StepperChanged.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnStepperChanged;
                @StepperChanged.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnStepperChanged;
                @StepperChanged.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnStepperChanged;
                @DropItem.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem;
                @DeleteItem.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDeleteItem;
                @DeleteItem.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDeleteItem;
                @DeleteItem.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDeleteItem;
                @Cancel.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCancel;
                @Interact.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @StepperChanged.started += instance.OnStepperChanged;
                @StepperChanged.performed += instance.OnStepperChanged;
                @StepperChanged.canceled += instance.OnStepperChanged;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @DeleteItem.started += instance.OnDeleteItem;
                @DeleteItem.performed += instance.OnDeleteItem;
                @DeleteItem.canceled += instance.OnDeleteItem;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
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
        private @GameplayControls m_Wrapper;
        public ToolbarActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
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

    // Shortcuts
    private readonly InputActionMap m_Shortcuts;
    private IShortcutsActions m_ShortcutsActionsCallbackInterface;
    private readonly InputAction m_Shortcuts_build;
    private readonly InputAction m_Shortcuts_weapon;
    private readonly InputAction m_Shortcuts_potion;
    private readonly InputAction m_Shortcuts_tool;
    public struct ShortcutsActions
    {
        private @GameplayControls m_Wrapper;
        public ShortcutsActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @build => m_Wrapper.m_Shortcuts_build;
        public InputAction @weapon => m_Wrapper.m_Shortcuts_weapon;
        public InputAction @potion => m_Wrapper.m_Shortcuts_potion;
        public InputAction @tool => m_Wrapper.m_Shortcuts_tool;
        public InputActionMap Get() { return m_Wrapper.m_Shortcuts; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShortcutsActions set) { return set.Get(); }
        public void SetCallbacks(IShortcutsActions instance)
        {
            if (m_Wrapper.m_ShortcutsActionsCallbackInterface != null)
            {
                @build.started -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnBuild;
                @build.performed -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnBuild;
                @build.canceled -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnBuild;
                @weapon.started -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnWeapon;
                @weapon.performed -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnWeapon;
                @weapon.canceled -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnWeapon;
                @potion.started -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnPotion;
                @potion.performed -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnPotion;
                @potion.canceled -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnPotion;
                @tool.started -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnTool;
                @tool.performed -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnTool;
                @tool.canceled -= m_Wrapper.m_ShortcutsActionsCallbackInterface.OnTool;
            }
            m_Wrapper.m_ShortcutsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @build.started += instance.OnBuild;
                @build.performed += instance.OnBuild;
                @build.canceled += instance.OnBuild;
                @weapon.started += instance.OnWeapon;
                @weapon.performed += instance.OnWeapon;
                @weapon.canceled += instance.OnWeapon;
                @potion.started += instance.OnPotion;
                @potion.performed += instance.OnPotion;
                @potion.canceled += instance.OnPotion;
                @tool.started += instance.OnTool;
                @tool.performed += instance.OnTool;
                @tool.canceled += instance.OnTool;
            }
        }
    }
    public ShortcutsActions @Shortcuts => new ShortcutsActions(this);
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
        void OnStepperChanged(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
        void OnDeleteItem(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IToolbarActions
    {
        void OnNavigate(InputAction.CallbackContext context);
    }
    public interface IShortcutsActions
    {
        void OnBuild(InputAction.CallbackContext context);
        void OnWeapon(InputAction.CallbackContext context);
        void OnPotion(InputAction.CallbackContext context);
        void OnTool(InputAction.CallbackContext context);
    }
}
