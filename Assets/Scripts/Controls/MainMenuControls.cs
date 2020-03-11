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
            ""name"": ""Main"",
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
                    ""id"": ""ea538a2c-ca6d-4a6d-8f97-808800191fd2"",
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
                    ""name"": """",
                    ""id"": ""b540f14f-e020-4260-815e-dca439fec1f5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9199579e-e2bc-42e7-9e2c-433e98116b93"",
                    ""path"": ""<Keyboard>/enter"",
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
            ""name"": ""Continue"",
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
                    ""type"": ""Value"",
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
                    ""name"": ""2D Vector"",
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
                    ""path"": ""<Keyboard>/backspace"",
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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Navigate = m_Main.FindAction("Navigate", throwIfNotFound: true);
        m_Main_Cancel = m_Main.FindAction("Cancel", throwIfNotFound: true);
        m_Main_Select = m_Main.FindAction("Select", throwIfNotFound: true);
        // Continue
        m_Continue = asset.FindActionMap("Continue", throwIfNotFound: true);
        m_Continue_Navigate = m_Continue.FindAction("Navigate", throwIfNotFound: true);
        m_Continue_Select = m_Continue.FindAction("Select", throwIfNotFound: true);
        m_Continue_Delete = m_Continue.FindAction("Delete", throwIfNotFound: true);
        m_Continue_Cancel = m_Continue.FindAction("Cancel", throwIfNotFound: true);
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

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Navigate;
    private readonly InputAction m_Main_Cancel;
    private readonly InputAction m_Main_Select;
    public struct MainActions
    {
        private @MainMenuControls m_Wrapper;
        public MainActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Main_Navigate;
        public InputAction @Cancel => m_Wrapper.m_Main_Cancel;
        public InputAction @Select => m_Wrapper.m_Main_Select;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_MainActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnNavigate;
                @Cancel.started -= m_Wrapper.m_MainActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnCancel;
                @Select.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
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
    public MainActions @Main => new MainActions(this);

    // Continue
    private readonly InputActionMap m_Continue;
    private IContinueActions m_ContinueActionsCallbackInterface;
    private readonly InputAction m_Continue_Navigate;
    private readonly InputAction m_Continue_Select;
    private readonly InputAction m_Continue_Delete;
    private readonly InputAction m_Continue_Cancel;
    public struct ContinueActions
    {
        private @MainMenuControls m_Wrapper;
        public ContinueActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Continue_Navigate;
        public InputAction @Select => m_Wrapper.m_Continue_Select;
        public InputAction @Delete => m_Wrapper.m_Continue_Delete;
        public InputAction @Cancel => m_Wrapper.m_Continue_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Continue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ContinueActions set) { return set.Get(); }
        public void SetCallbacks(IContinueActions instance)
        {
            if (m_Wrapper.m_ContinueActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_ContinueActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_ContinueActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_ContinueActionsCallbackInterface.OnNavigate;
                @Select.started -= m_Wrapper.m_ContinueActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_ContinueActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_ContinueActionsCallbackInterface.OnSelect;
                @Delete.started -= m_Wrapper.m_ContinueActionsCallbackInterface.OnDelete;
                @Delete.performed -= m_Wrapper.m_ContinueActionsCallbackInterface.OnDelete;
                @Delete.canceled -= m_Wrapper.m_ContinueActionsCallbackInterface.OnDelete;
                @Cancel.started -= m_Wrapper.m_ContinueActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_ContinueActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_ContinueActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_ContinueActionsCallbackInterface = instance;
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
    public ContinueActions @Continue => new ContinueActions(this);
    public interface IMainActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface IContinueActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnDelete(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}
