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
                    ""name"": ""LeftStickSwitch"",
                    ""id"": ""2d8c781a-f58c-43ea-93df-9aed26b861ea"",
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
                    ""id"": ""6e7bf136-8ba3-405b-b1ef-39ec6c21dcc4"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f50c8744-44c2-4f50-ab2a-8f444ff5aa73"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/right"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""351d77eb-f2ce-4883-9ff8-462a068175b9"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button3"",
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
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""895dbc35-38e4-4d50-9bcc-c44f94e5449e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HorizontalMove"",
                    ""type"": ""Value"",
                    ""id"": ""3c3ae861-baa0-43eb-9771-62353fc4b636"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VerticalMove"",
                    ""type"": ""Value"",
                    ""id"": ""d829c719-dbd6-4a55-88bd-ddc2b710cbcd"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""f555f03f-b0dd-4916-919e-4752c12196ca"",
                    ""expectedControlType"": """",
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
                    ""id"": ""f9e73344-3861-41a4-a52d-55dc9c7ac57b"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16d4e606-cb43-45e6-bb5f-6671f1d87990"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button6"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""d34fced6-03e8-4579-8c42-5cf47f70aa34"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a7ea777-1e97-4809-b0cf-6fa51a59a6a9"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button6"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LR"",
                    ""id"": ""d0cbdadc-8da5-454b-b960-b8c24c21f0e9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.9,max=0.925)"",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4fa69552-87e2-429f-b520-6309cc073f14"",
                    ""path"": ""<XInputController>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e728a5b-256f-4730-9017-f06496579fe3"",
                    ""path"": ""<XInputController>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""48cec733-75ab-453a-a2cc-98f0a578cdf6"",
                    ""path"": ""<XInputController>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6fe9d727-8c51-4a0c-a71c-201f212af605"",
                    ""path"": ""<XInputController>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b6ccd913-72b2-4c79-84e9-2257f8c94fd3"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43d342e8-2230-475a-bea5-0bd5d0521d92"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ed023b4-c3c6-4371-8354-0939e592a6ff"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df9e8228-2130-4e6d-ac4a-42af6fc1a62e"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
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
                    ""name"": ""ChangeStepper"",
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
                    ""path"": ""<XInputController>/dpad/up"",
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
                    ""path"": ""<XInputController>/dpad/down"",
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
                    ""path"": ""<XInputController>/dpad/left"",
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
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""CrossSwitch"",
                    ""id"": ""50bd933e-f951-4d05-b9df-581aaf8b1416"",
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
                    ""id"": ""118fff92-4549-4280-9cbc-d5465968f47a"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""379fa94b-bff7-467c-9749-79a74adf9b0f"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b3f68449-0b92-4102-8fef-572a912774bb"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c3bb74b6-4d24-4de9-a926-ddf6b31cecaa"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""leftStickHoripad"",
                    ""id"": ""a9ac7995-431e-4e85-9c92-1dd5454c0cde"",
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
                    ""id"": ""e14467bd-e9db-42b0-8bb7-35ac420a1e2d"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6fa193e5-8c18-4a25-8478-a349b75835c1"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""32913f74-ebe7-4f15-9ae1-913d2999eb99"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""30bcae0d-c1c9-479a-828e-f8fad442a0e9"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""ee38c9ab-967d-48cf-9421-256b79808bd3"",
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
                    ""id"": ""53351aaa-f92b-4f8d-a06d-36edd4106da2"",
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
                    ""id"": ""cccd1f8e-11b2-40bf-a333-8a3cf32fbf59"",
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
                    ""id"": ""880d3ef2-69cd-4c53-823e-c52a35d60aa6"",
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
                    ""id"": ""742ad1a9-c6b9-445d-b5ad-9e058ebd3bf8"",
                    ""path"": ""<Keyboard>/rightArrow"",
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
                    ""action"": ""ChangeStepper"",
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
                    ""action"": ""ChangeStepper"",
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
                    ""action"": ""ChangeStepper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""BumpersSwitch"",
                    ""id"": ""d7a61b35-42c1-4c20-94fe-09d49a15e815"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeStepper"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""db4f00e5-3553-4c87-b6f7-33e1471f8752"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeStepper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2d625370-2a61-4ddc-bb10-40440c7abb1e"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeStepper"",
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
                    ""id"": ""a4e533dc-8788-43ae-92f9-d4cc6025162f"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button2"",
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
                    ""id"": ""5f10f014-4e12-4cd3-a0fd-6dd81dc9c73e"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button4"",
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
                    ""id"": ""2cd0b152-b219-4a0b-bceb-4ddb6a22224a"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/trigger"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""b098e40f-9004-4c4d-80ef-6054429fa185"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9070c765-9349-4235-a516-a373ce207b05"",
                    ""path"": ""<Keyboard>/n"",
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
                    ""name"": ""CrossSwitch"",
                    ""id"": ""84c4cb93-2df7-4588-99f8-b73681597672"",
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
                    ""id"": ""24903827-1f23-4f63-8f29-ad3c705880f4"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""40f3b198-6415-4700-8512-358adae639e6"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/right"",
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
                    ""id"": ""c9b79bb2-1c5b-4d96-92be-25de492b336b"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/up"",
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
                    ""id"": ""bc55d2ae-21e2-4942-9964-1c1a952970d9"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/right"",
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
                    ""id"": ""8b0d4193-c0f3-4418-8ac3-9b1b1720154c"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/down"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""fd9ba853-5278-4551-a9a3-32c908e94a7e"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""36ecce96-855a-4c3a-b482-f6d669be9726"",
            ""actions"": [
                {
                    ""name"": ""LookVertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7e01e78d-b657-43c6-9b16-7f738fde3650"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""XBOX"",
                    ""id"": ""234dcb8a-5233-4538-af6d-66e6bc628c70"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9695d6ad-2cca-46a7-829b-dcc89098ff47"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3f3375bc-2747-4312-98ac-7cce285daf9d"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""HORIPAD"",
                    ""id"": ""1f85b9ed-717f-4838-bb6f-f4cebcd4f8db"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""04b5e4d4-3a60-49b3-a9bf-41c9e4457933"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""50afc30d-ba35-4ec6-8222-f898ed76ec89"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""PC"",
                    ""id"": ""bc8054f0-8768-4ce6-ad13-d496715ce9e8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""94fb7bbf-4730-4c87-96a5-c946b0cfc9f5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""59b650b0-5e24-4c27-aef0-48fae1b9289d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""ToolSelector"",
            ""id"": ""270bb24e-2f6f-4246-869b-3bda7576fb36"",
            ""actions"": [
                {
                    ""name"": ""PressClick"",
                    ""type"": ""Button"",
                    ""id"": ""7f8db891-05b2-4699-855f-f9447ac873a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReleaseClick"",
                    ""type"": ""Button"",
                    ""id"": ""1ca10135-078c-44d5-8ac1-27fde30e4ce9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookDirection"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e4903f1d-89d7-4d19-9dbd-56e90fcb40ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""238f8782-806b-4e4d-a10e-1f023ffd5a01"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59bf25e0-3d21-46fc-beb2-2544f93ca1b5"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button6"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6003b1c7-049c-4835-b045-3f52114fe49a"",
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
                    ""id"": ""fc197d66-8178-4442-bbfc-74536df67565"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7115d44-69ff-4497-b5e5-4b2f3b429e8a"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccc0e5b7-66f5-4905-acfe-f7bbc30fd3a1"",
                    ""path"": ""<HID::HORI CO.,LTD. HORIPAD S>/button6"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7392748-8a6f-4b5a-bf7b-e664debc654c"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookDirection"",
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
        m_TileSelector_Navigate = m_TileSelector.FindAction("Navigate", throwIfNotFound: true);
        m_TileSelector_HorizontalMove = m_TileSelector.FindAction("HorizontalMove", throwIfNotFound: true);
        m_TileSelector_VerticalMove = m_TileSelector.FindAction("VerticalMove", throwIfNotFound: true);
        m_TileSelector_Click = m_TileSelector.FindAction("Click", throwIfNotFound: true);
        // Core
        m_Core = asset.FindActionMap("Core", throwIfNotFound: true);
        m_Core_Position = m_Core.FindAction("Position", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Navigate = m_Inventory.FindAction("Navigate", throwIfNotFound: true);
        m_Inventory_ChangeStepper = m_Inventory.FindAction("ChangeStepper", throwIfNotFound: true);
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
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_LookVertical = m_Camera.FindAction("LookVertical", throwIfNotFound: true);
        // ToolSelector
        m_ToolSelector = asset.FindActionMap("ToolSelector", throwIfNotFound: true);
        m_ToolSelector_PressClick = m_ToolSelector.FindAction("PressClick", throwIfNotFound: true);
        m_ToolSelector_ReleaseClick = m_ToolSelector.FindAction("ReleaseClick", throwIfNotFound: true);
        m_ToolSelector_LookDirection = m_ToolSelector.FindAction("LookDirection", throwIfNotFound: true);
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
    private readonly InputAction m_TileSelector_Navigate;
    private readonly InputAction m_TileSelector_HorizontalMove;
    private readonly InputAction m_TileSelector_VerticalMove;
    private readonly InputAction m_TileSelector_Click;
    public struct TileSelectorActions
    {
        private @GameplayControls m_Wrapper;
        public TileSelectorActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PressClick => m_Wrapper.m_TileSelector_PressClick;
        public InputAction @ReleaseClick => m_Wrapper.m_TileSelector_ReleaseClick;
        public InputAction @Navigate => m_Wrapper.m_TileSelector_Navigate;
        public InputAction @HorizontalMove => m_Wrapper.m_TileSelector_HorizontalMove;
        public InputAction @VerticalMove => m_Wrapper.m_TileSelector_VerticalMove;
        public InputAction @Click => m_Wrapper.m_TileSelector_Click;
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
                @Navigate.started -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnNavigate;
                @HorizontalMove.started -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.performed -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.canceled -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnHorizontalMove;
                @VerticalMove.started -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnVerticalMove;
                @VerticalMove.performed -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnVerticalMove;
                @VerticalMove.canceled -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnVerticalMove;
                @Click.started -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_TileSelectorActionsCallbackInterface.OnClick;
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
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @HorizontalMove.started += instance.OnHorizontalMove;
                @HorizontalMove.performed += instance.OnHorizontalMove;
                @HorizontalMove.canceled += instance.OnHorizontalMove;
                @VerticalMove.started += instance.OnVerticalMove;
                @VerticalMove.performed += instance.OnVerticalMove;
                @VerticalMove.canceled += instance.OnVerticalMove;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
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
    private readonly InputAction m_Inventory_ChangeStepper;
    private readonly InputAction m_Inventory_DropItem;
    private readonly InputAction m_Inventory_DeleteItem;
    private readonly InputAction m_Inventory_Cancel;
    private readonly InputAction m_Inventory_Interact;
    public struct InventoryActions
    {
        private @GameplayControls m_Wrapper;
        public InventoryActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Inventory_Navigate;
        public InputAction @ChangeStepper => m_Wrapper.m_Inventory_ChangeStepper;
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
                @ChangeStepper.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnChangeStepper;
                @ChangeStepper.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnChangeStepper;
                @ChangeStepper.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnChangeStepper;
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
                @ChangeStepper.started += instance.OnChangeStepper;
                @ChangeStepper.performed += instance.OnChangeStepper;
                @ChangeStepper.canceled += instance.OnChangeStepper;
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_LookVertical;
    public struct CameraActions
    {
        private @GameplayControls m_Wrapper;
        public CameraActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookVertical => m_Wrapper.m_Camera_LookVertical;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @LookVertical.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookVertical;
                @LookVertical.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookVertical;
                @LookVertical.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookVertical;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LookVertical.started += instance.OnLookVertical;
                @LookVertical.performed += instance.OnLookVertical;
                @LookVertical.canceled += instance.OnLookVertical;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // ToolSelector
    private readonly InputActionMap m_ToolSelector;
    private IToolSelectorActions m_ToolSelectorActionsCallbackInterface;
    private readonly InputAction m_ToolSelector_PressClick;
    private readonly InputAction m_ToolSelector_ReleaseClick;
    private readonly InputAction m_ToolSelector_LookDirection;
    public struct ToolSelectorActions
    {
        private @GameplayControls m_Wrapper;
        public ToolSelectorActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PressClick => m_Wrapper.m_ToolSelector_PressClick;
        public InputAction @ReleaseClick => m_Wrapper.m_ToolSelector_ReleaseClick;
        public InputAction @LookDirection => m_Wrapper.m_ToolSelector_LookDirection;
        public InputActionMap Get() { return m_Wrapper.m_ToolSelector; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ToolSelectorActions set) { return set.Get(); }
        public void SetCallbacks(IToolSelectorActions instance)
        {
            if (m_Wrapper.m_ToolSelectorActionsCallbackInterface != null)
            {
                @PressClick.started -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnPressClick;
                @PressClick.performed -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnPressClick;
                @PressClick.canceled -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnPressClick;
                @ReleaseClick.started -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnReleaseClick;
                @ReleaseClick.performed -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnReleaseClick;
                @ReleaseClick.canceled -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnReleaseClick;
                @LookDirection.started -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnLookDirection;
                @LookDirection.performed -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnLookDirection;
                @LookDirection.canceled -= m_Wrapper.m_ToolSelectorActionsCallbackInterface.OnLookDirection;
            }
            m_Wrapper.m_ToolSelectorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PressClick.started += instance.OnPressClick;
                @PressClick.performed += instance.OnPressClick;
                @PressClick.canceled += instance.OnPressClick;
                @ReleaseClick.started += instance.OnReleaseClick;
                @ReleaseClick.performed += instance.OnReleaseClick;
                @ReleaseClick.canceled += instance.OnReleaseClick;
                @LookDirection.started += instance.OnLookDirection;
                @LookDirection.performed += instance.OnLookDirection;
                @LookDirection.canceled += instance.OnLookDirection;
            }
        }
    }
    public ToolSelectorActions @ToolSelector => new ToolSelectorActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ITileSelectorActions
    {
        void OnPressClick(InputAction.CallbackContext context);
        void OnReleaseClick(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
        void OnHorizontalMove(InputAction.CallbackContext context);
        void OnVerticalMove(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
    public interface ICoreActions
    {
        void OnPosition(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnChangeStepper(InputAction.CallbackContext context);
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
    public interface ICameraActions
    {
        void OnLookVertical(InputAction.CallbackContext context);
    }
    public interface IToolSelectorActions
    {
        void OnPressClick(InputAction.CallbackContext context);
        void OnReleaseClick(InputAction.CallbackContext context);
        void OnLookDirection(InputAction.CallbackContext context);
    }
}
