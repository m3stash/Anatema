// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/MainMenuControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainMenuControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @MainMenuControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainMenuControls"",
    ""maps"": [
        {
            ""name"": ""LeftMenu"",
            ""id"": ""1a6a29cc-7420-419f-8131-95d97ef1ddef"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""39e56a1b-56b0-4ae6-9314-c07c8c26fc5f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""7964bad9-add6-460a-a179-d9c39a453e56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""310fb2de-76a5-4dd7-ae76-c9ed4203071b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a281a0d4-af22-44ab-95a5-410e53c7e20e"",
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
                    ""id"": ""c34923b8-5b6d-4d8d-a358-de92eae53298"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f71532b8-08e0-40a6-89e3-76ba522c6419"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e67a0994-fd3a-4d17-9205-bf2a3f6f2610"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""adab4dcd-bf65-4e1d-8e55-0bc6060262e8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""xbox-one-controller"",
                    ""id"": ""935f15d8-855e-4cf7-8a67-3bba2c09ed3e"",
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
                    ""id"": ""ecb39f36-2452-4bf8-940d-3fe3d29f46a6"",
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
                    ""id"": ""984d6512-7b6b-4dc1-8b3d-934330233c50"",
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
                    ""id"": ""ce098b30-13d7-4dfa-b6a7-42b2f33ed228"",
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
                    ""id"": ""9f821766-0b65-46cb-a07c-551a654597ae"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e117476c-5559-4d59-9ca7-52eb93f75c89"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0c9986d-4740-4210-a544-5556c6b7c36c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""078ab324-e373-48fd-8a39-c82ca2425fe7"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""864d125a-6cb4-48fa-8c31-ecaa1e8445c2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""StartMenu"",
            ""id"": ""0e3639c6-fd52-4acf-bbff-f1c84032eb33"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""ae500c7e-3dd2-41f4-9aa7-67fcf9d548c7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""559aa832-a1e7-4a32-aed7-9fd9511f405e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delete"",
                    ""type"": ""Button"",
                    ""id"": ""404a2313-8b3a-4549-9082-063ff100497b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""ec993604-cc3b-4bdc-9b71-e6418f717c69"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""184ee08a-4aa7-4655-b7a1-75159d2356b8"",
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
                    ""id"": ""f0db6830-8a97-4e35-902c-21343d1b5df9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""466ace78-8d82-418a-a19c-23ac6f579c8a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce147fb9-8101-4557-9144-522a547ce3b1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f9f08fe2-d02e-4b25-bbf9-3649493ab717"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a8de3d62-996a-4fe7-95a2-7d50c87bf918"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8707f7ab-ee5b-4215-880d-595094990ec4"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b17f838-d07e-4c1a-9607-18e8ee1ae54a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""xbox-one-controller"",
                    ""id"": ""62e09aad-69f9-4d3a-91f8-a38bbb3f52f8"",
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
                    ""id"": ""8aeae508-945d-4a93-9e35-b8d6b530c1ce"",
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
                    ""id"": ""9c914a1b-9a8d-48e1-baf4-67598a755e0b"",
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
                    ""id"": ""c7cb8cab-d6c5-4a16-b23a-bc5f0aca57ed"",
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
                    ""id"": ""f6ca100c-6277-4fa1-93d2-3f863c5b96e7"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""30d209b9-a1d2-4d28-8ce5-68e19af955d5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c36e9c37-73bd-4e7b-9a32-22f7f278c9fc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OptionsMenu"",
            ""id"": ""0cc92ba5-b4ab-4a03-ad19-e28bbd224087"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""009a4f6c-f948-467f-a0b6-64ad513899f7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b08f3112-2c54-4c11-8a52-a8eea1545c50"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DialogModal"",
            ""id"": ""01b747de-8af1-435a-9b13-16e58abc9c53"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""6ca4efc7-6eda-492b-bdad-e1b52a40df76"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""8420d9bc-0c52-4422-9745-c6893ec1ea24"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""dc1b88c6-6dce-4203-aeff-1792bb728d1a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""42ff9040-892a-463b-830e-fdf2bc151a3f"",
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
                    ""id"": ""292ab072-8215-47b5-baaa-a7a98d8c6ee7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b85b8d2a-56db-44cf-99e0-9c581785235e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0e3d8cc1-73e3-46fc-95de-2ff4fd49ea5f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""856770e5-a563-48ed-a6e1-c7e18c87410b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ce70f9d3-e3ca-4e31-9b63-ba7030c4eb57"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""884e70b6-bfa0-49d2-86ed-49612389b943"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""xbox-one-controller"",
                    ""id"": ""2e20e076-23ba-4606-9799-78b3a04a9660"",
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
                    ""id"": ""8d513a18-473e-4202-b949-d0384017a80d"",
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
                    ""id"": ""d2371a95-37eb-412f-a2d4-311e81825606"",
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
                    ""id"": ""40ecc728-4de4-4bc3-ba01-b0ad5a301759"",
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
                    ""id"": ""0f06b652-7644-4786-97cc-cb74225d24ac"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""390d0043-1937-4974-aece-7d10a8c162d0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8807632-4cf1-46b6-8fcb-06d764b790c6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // LeftMenu
        m_LeftMenu = asset.FindActionMap("LeftMenu", throwIfNotFound: true);
        m_LeftMenu_Navigate = m_LeftMenu.FindAction("Navigate", throwIfNotFound: true);
        m_LeftMenu_Cancel = m_LeftMenu.FindAction("Cancel", throwIfNotFound: true);
        m_LeftMenu_Select = m_LeftMenu.FindAction("Select", throwIfNotFound: true);
        // StartMenu
        m_StartMenu = asset.FindActionMap("StartMenu", throwIfNotFound: true);
        m_StartMenu_Navigate = m_StartMenu.FindAction("Navigate", throwIfNotFound: true);
        m_StartMenu_Select = m_StartMenu.FindAction("Select", throwIfNotFound: true);
        m_StartMenu_Delete = m_StartMenu.FindAction("Delete", throwIfNotFound: true);
        m_StartMenu_Cancel = m_StartMenu.FindAction("Cancel", throwIfNotFound: true);
        // OptionsMenu
        m_OptionsMenu = asset.FindActionMap("OptionsMenu", throwIfNotFound: true);
        m_OptionsMenu_Newaction = m_OptionsMenu.FindAction("New action", throwIfNotFound: true);
        // DialogModal
        m_DialogModal = asset.FindActionMap("DialogModal", throwIfNotFound: true);
        m_DialogModal_Navigate = m_DialogModal.FindAction("Navigate", throwIfNotFound: true);
        m_DialogModal_Select = m_DialogModal.FindAction("Select", throwIfNotFound: true);
        m_DialogModal_Cancel = m_DialogModal.FindAction("Cancel", throwIfNotFound: true);
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

    // LeftMenu
    private readonly InputActionMap m_LeftMenu;
    private ILeftMenuActions m_LeftMenuActionsCallbackInterface;
    private readonly InputAction m_LeftMenu_Navigate;
    private readonly InputAction m_LeftMenu_Cancel;
    private readonly InputAction m_LeftMenu_Select;
    public struct LeftMenuActions
    {
        private @MainMenuControls m_Wrapper;
        public LeftMenuActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_LeftMenu_Navigate;
        public InputAction @Cancel => m_Wrapper.m_LeftMenu_Cancel;
        public InputAction @Select => m_Wrapper.m_LeftMenu_Select;
        public InputActionMap Get() { return m_Wrapper.m_LeftMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LeftMenuActions set) { return set.Get(); }
        public void SetCallbacks(ILeftMenuActions instance)
        {
            if (m_Wrapper.m_LeftMenuActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnNavigate;
                @Cancel.started -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnCancel;
                @Select.started -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_LeftMenuActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_LeftMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public LeftMenuActions @LeftMenu => new LeftMenuActions(this);

    // StartMenu
    private readonly InputActionMap m_StartMenu;
    private IStartMenuActions m_StartMenuActionsCallbackInterface;
    private readonly InputAction m_StartMenu_Navigate;
    private readonly InputAction m_StartMenu_Select;
    private readonly InputAction m_StartMenu_Delete;
    private readonly InputAction m_StartMenu_Cancel;
    public struct StartMenuActions
    {
        private @MainMenuControls m_Wrapper;
        public StartMenuActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_StartMenu_Navigate;
        public InputAction @Select => m_Wrapper.m_StartMenu_Select;
        public InputAction @Delete => m_Wrapper.m_StartMenu_Delete;
        public InputAction @Cancel => m_Wrapper.m_StartMenu_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_StartMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StartMenuActions set) { return set.Get(); }
        public void SetCallbacks(IStartMenuActions instance)
        {
            if (m_Wrapper.m_StartMenuActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnNavigate;
                @Select.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnSelect;
                @Delete.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnDelete;
                @Delete.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnDelete;
                @Delete.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnDelete;
                @Cancel.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_StartMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Delete.started += instance.OnDelete;
                @Delete.performed += instance.OnDelete;
                @Delete.canceled += instance.OnDelete;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public StartMenuActions @StartMenu => new StartMenuActions(this);

    // OptionsMenu
    private readonly InputActionMap m_OptionsMenu;
    private IOptionsMenuActions m_OptionsMenuActionsCallbackInterface;
    private readonly InputAction m_OptionsMenu_Newaction;
    public struct OptionsMenuActions
    {
        private @MainMenuControls m_Wrapper;
        public OptionsMenuActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_OptionsMenu_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_OptionsMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OptionsMenuActions set) { return set.Get(); }
        public void SetCallbacks(IOptionsMenuActions instance)
        {
            if (m_Wrapper.m_OptionsMenuActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_OptionsMenuActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_OptionsMenuActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_OptionsMenuActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_OptionsMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public OptionsMenuActions @OptionsMenu => new OptionsMenuActions(this);

    // DialogModal
    private readonly InputActionMap m_DialogModal;
    private IDialogModalActions m_DialogModalActionsCallbackInterface;
    private readonly InputAction m_DialogModal_Navigate;
    private readonly InputAction m_DialogModal_Select;
    private readonly InputAction m_DialogModal_Cancel;
    public struct DialogModalActions
    {
        private @MainMenuControls m_Wrapper;
        public DialogModalActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_DialogModal_Navigate;
        public InputAction @Select => m_Wrapper.m_DialogModal_Select;
        public InputAction @Cancel => m_Wrapper.m_DialogModal_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_DialogModal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogModalActions set) { return set.Get(); }
        public void SetCallbacks(IDialogModalActions instance)
        {
            if (m_Wrapper.m_DialogModalActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnNavigate;
                @Select.started -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnSelect;
                @Cancel.started -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_DialogModalActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_DialogModalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public DialogModalActions @DialogModal => new DialogModalActions(this);
    public interface ILeftMenuActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface IStartMenuActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnDelete(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IOptionsMenuActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IDialogModalActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}
