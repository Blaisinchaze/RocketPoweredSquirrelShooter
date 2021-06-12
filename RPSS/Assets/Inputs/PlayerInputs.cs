// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Gameplay Combined"",
            ""id"": ""ea342e2a-e01a-472a-9068-ac25d0936f7c"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""21eaef50-9dc1-4b8c-a74f-3795b0f290f1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Firing"",
                    ""type"": ""Value"",
                    ""id"": ""5538cc5c-e334-4578-bbc4-b04441e9da0c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d5611321-3481-4a34-9714-ffdad8954eac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Separate"",
                    ""type"": ""Button"",
                    ""id"": ""2a6725ef-f3fb-4c2f-8546-b9646e6d4815"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Movement (KB+M)"",
                    ""id"": ""4f55b9fb-0c94-4023-94fa-a9ee0336767d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""23a822de-ebc0-4421-a4e9-9f49c5cb4564"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""63c713a5-6b46-4999-9e37-658a987fc2bf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bf74aca8-08da-4a01-9036-7aa51131ad28"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""281f1025-1e03-48c7-ac47-9215571690e7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement (Controller)"",
                    ""id"": ""7d499d02-59d8-49fd-b2ef-c912fb7d9bb0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a6564541-f7b9-44a6-8edb-0dbde16e21a8"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""82343200-c5d1-4985-9869-020be412a673"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2e39c82a-a513-4ce9-811f-0fab8746891a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b39616ac-a4cd-43c8-8dee-7cc71598414b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""84fc9f1b-d38b-481f-9e34-3dabfa97c0d9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Firing"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""65a7ab6a-50ab-475d-9a7b-2f86d8edd6d5"",
                    ""path"": ""<Pointer>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""713fe05c-171d-4387-beb3-eab8a7dfeab9"",
                    ""path"": ""<Pointer>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d4be150-80ae-4497-bb33-11e78cc21c67"",
                    ""path"": ""<Pointer>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e00c76d7-a6ce-4315-8956-bbdb1f7ccf2c"",
                    ""path"": ""<Pointer>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7bee8705-8557-422c-a5af-c302d29fa122"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Firing"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""99b4a934-24a3-45f8-b31b-83eba53a0af4"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""575e814c-7715-45f3-93b1-065fd0f86b71"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6eea6658-299d-4144-bc1c-b330ed551920"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0953fcef-f09a-45ca-a38e-0b788ecd67f5"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5cfed49a-a2a5-4c63-8c52-656771e14dfb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2869181b-6e1a-41ae-83eb-b9652a0f2a7c"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac84f204-abd8-4ed8-b80a-d25f0293eb01"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2dcfb25-bf0c-4130-8d3e-40418f4b8ef0"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07ad56c2-1124-4517-956d-924341792e7f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a41bc07-e217-46f8-8d7d-a68050720b3b"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5ee607d-0116-4084-8e9b-3f3a5750c514"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78ea22bd-4f71-4bf5-8366-ce054a8b258d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gameplay Separate"",
            ""id"": ""85a13ca4-523d-4547-8f95-8f726bcd3549"",
            ""actions"": [
                {
                    ""name"": ""Movement A"",
                    ""type"": ""Value"",
                    ""id"": ""6cfe8397-89d5-4b01-97fc-7a1a6323a3cd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement B"",
                    ""type"": ""Value"",
                    ""id"": ""16985a9b-b7e9-4156-8151-ed24b11f45f2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""f0a0f259-040b-4854-9bda-a6119c34e60e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Separate"",
                    ""type"": ""Button"",
                    ""id"": ""2984f87a-f206-45ac-bc12-e15944529782"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fist Firing"",
                    ""type"": ""Button"",
                    ""id"": ""b4950e1d-a8d9-4328-955e-5e1bf4c6c599"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Movement (KB+M)"",
                    ""id"": ""cdc0065a-dd21-4dac-bb48-ab3d0857931d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement A"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""402a76b5-b357-4b86-b25a-c481aed49f32"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3e4405f7-fdfb-4fcf-b0ff-f3102503711e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3e87b667-3fd7-4fc7-a380-29105af3cfa4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""022bb97e-2762-4034-897f-102980825358"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement (Controller)"",
                    ""id"": ""9fb9733e-ebbe-488b-a677-dc480d37e0f4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement A"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a28629c5-4000-4c76-8081-be22e87ef70e"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3a32435c-49c7-4bf4-bf1b-5bb4595ebcbd"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0f179b64-6417-4f62-b5ad-6e34c0d25438"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2719e300-3882-4f5d-9d17-eb0531e7cf09"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement + Firing (KB+M)"",
                    ""id"": ""b4725cca-dbe9-45ab-8d2b-6f28b9444ee9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement B"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""50658828-7dcd-4d1b-a8e2-c52e0cb85b95"",
                    ""path"": ""<Pointer>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3d8d2503-3098-4976-9e5d-d71748200a20"",
                    ""path"": ""<Pointer>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bcd5a978-233c-4ab6-a0b9-28baba639902"",
                    ""path"": ""<Pointer>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ff410d9f-c56b-493a-8d7a-2993c14d86ac"",
                    ""path"": ""<Pointer>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement + Firing (Controller)"",
                    ""id"": ""fda55a19-5b36-4323-bbfb-ccfe9c49063f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement B"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""455737a0-0526-4994-841b-39fc70ce5052"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2b395b71-961b-4e26-ac86-657ccb2f7264"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""94f9c03b-b254-4434-9197-f9c97999e9ff"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c047737a-247a-4ba6-8ce7-2326fbb14e51"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1f7046ac-cf3b-4ea9-96b6-8e1f65fe89a9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cb947cc-c448-4682-b2c3-2f315148d91e"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8ed2f5a-2066-4918-9edb-0e6941fcd261"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37e7e0e2-0652-44a5-96c1-84b400e1a5ec"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55c28c9b-500c-4262-aea9-9f7e7b409529"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c553cf1-83a0-4704-8b83-a5679e371c54"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ce9b298-3c8b-451c-8645-6389bacf80a9"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""015061e0-9d75-438e-a788-5f2cbd4dbb44"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Separate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c2e7fe1-ce56-4ec1-a82d-d0b66d65cbb5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Fist Firing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""54a259f7-c5f0-4cb9-8527-ff39c79b7b7a"",
            ""actions"": [
                {
                    ""name"": ""Target"",
                    ""type"": ""Value"",
                    ""id"": ""13bc1fe6-f39a-4c77-b5bb-11e5e4d780ae"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""33aec749-e939-4edc-ab5e-c795844047a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""46b21631-3a44-4f9b-a3c7-ba1f43dba1d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Menu Navigation"",
                    ""id"": ""0b1558bd-37c5-41fe-9f8e-6b1e7c28a2ab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Target"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1599f65d-451e-4832-b1ad-f4a21accefa0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9abfe934-a401-48b7-bc1b-fc8442ab2150"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""39695fbd-0832-4b36-a602-07942021f8ca"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07e65bda-017b-47ab-b281-36d029f14e4e"",
                    ""path"": ""*/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay Combined
        m_GameplayCombined = asset.FindActionMap("Gameplay Combined", throwIfNotFound: true);
        m_GameplayCombined_Movement = m_GameplayCombined.FindAction("Movement", throwIfNotFound: true);
        m_GameplayCombined_Firing = m_GameplayCombined.FindAction("Firing", throwIfNotFound: true);
        m_GameplayCombined_Interact = m_GameplayCombined.FindAction("Interact", throwIfNotFound: true);
        m_GameplayCombined_Separate = m_GameplayCombined.FindAction("Separate", throwIfNotFound: true);
        // Gameplay Separate
        m_GameplaySeparate = asset.FindActionMap("Gameplay Separate", throwIfNotFound: true);
        m_GameplaySeparate_MovementA = m_GameplaySeparate.FindAction("Movement A", throwIfNotFound: true);
        m_GameplaySeparate_MovementB = m_GameplaySeparate.FindAction("Movement B", throwIfNotFound: true);
        m_GameplaySeparate_Interact = m_GameplaySeparate.FindAction("Interact", throwIfNotFound: true);
        m_GameplaySeparate_Separate = m_GameplaySeparate.FindAction("Separate", throwIfNotFound: true);
        m_GameplaySeparate_FistFiring = m_GameplaySeparate.FindAction("Fist Firing", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Target = m_Menu.FindAction("Target", throwIfNotFound: true);
        m_Menu_Accept = m_Menu.FindAction("Accept", throwIfNotFound: true);
        m_Menu_Back = m_Menu.FindAction("Back", throwIfNotFound: true);
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

    // Gameplay Combined
    private readonly InputActionMap m_GameplayCombined;
    private IGameplayCombinedActions m_GameplayCombinedActionsCallbackInterface;
    private readonly InputAction m_GameplayCombined_Movement;
    private readonly InputAction m_GameplayCombined_Firing;
    private readonly InputAction m_GameplayCombined_Interact;
    private readonly InputAction m_GameplayCombined_Separate;
    public struct GameplayCombinedActions
    {
        private @PlayerInputs m_Wrapper;
        public GameplayCombinedActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_GameplayCombined_Movement;
        public InputAction @Firing => m_Wrapper.m_GameplayCombined_Firing;
        public InputAction @Interact => m_Wrapper.m_GameplayCombined_Interact;
        public InputAction @Separate => m_Wrapper.m_GameplayCombined_Separate;
        public InputActionMap Get() { return m_Wrapper.m_GameplayCombined; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayCombinedActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayCombinedActions instance)
        {
            if (m_Wrapper.m_GameplayCombinedActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnMovement;
                @Firing.started -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnFiring;
                @Firing.performed -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnFiring;
                @Firing.canceled -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnFiring;
                @Interact.started -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnInteract;
                @Separate.started -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnSeparate;
                @Separate.performed -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnSeparate;
                @Separate.canceled -= m_Wrapper.m_GameplayCombinedActionsCallbackInterface.OnSeparate;
            }
            m_Wrapper.m_GameplayCombinedActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Firing.started += instance.OnFiring;
                @Firing.performed += instance.OnFiring;
                @Firing.canceled += instance.OnFiring;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Separate.started += instance.OnSeparate;
                @Separate.performed += instance.OnSeparate;
                @Separate.canceled += instance.OnSeparate;
            }
        }
    }
    public GameplayCombinedActions @GameplayCombined => new GameplayCombinedActions(this);

    // Gameplay Separate
    private readonly InputActionMap m_GameplaySeparate;
    private IGameplaySeparateActions m_GameplaySeparateActionsCallbackInterface;
    private readonly InputAction m_GameplaySeparate_MovementA;
    private readonly InputAction m_GameplaySeparate_MovementB;
    private readonly InputAction m_GameplaySeparate_Interact;
    private readonly InputAction m_GameplaySeparate_Separate;
    private readonly InputAction m_GameplaySeparate_FistFiring;
    public struct GameplaySeparateActions
    {
        private @PlayerInputs m_Wrapper;
        public GameplaySeparateActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementA => m_Wrapper.m_GameplaySeparate_MovementA;
        public InputAction @MovementB => m_Wrapper.m_GameplaySeparate_MovementB;
        public InputAction @Interact => m_Wrapper.m_GameplaySeparate_Interact;
        public InputAction @Separate => m_Wrapper.m_GameplaySeparate_Separate;
        public InputAction @FistFiring => m_Wrapper.m_GameplaySeparate_FistFiring;
        public InputActionMap Get() { return m_Wrapper.m_GameplaySeparate; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplaySeparateActions set) { return set.Get(); }
        public void SetCallbacks(IGameplaySeparateActions instance)
        {
            if (m_Wrapper.m_GameplaySeparateActionsCallbackInterface != null)
            {
                @MovementA.started -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnMovementA;
                @MovementA.performed -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnMovementA;
                @MovementA.canceled -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnMovementA;
                @MovementB.started -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnMovementB;
                @MovementB.performed -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnMovementB;
                @MovementB.canceled -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnMovementB;
                @Interact.started -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnInteract;
                @Separate.started -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnSeparate;
                @Separate.performed -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnSeparate;
                @Separate.canceled -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnSeparate;
                @FistFiring.started -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnFistFiring;
                @FistFiring.performed -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnFistFiring;
                @FistFiring.canceled -= m_Wrapper.m_GameplaySeparateActionsCallbackInterface.OnFistFiring;
            }
            m_Wrapper.m_GameplaySeparateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementA.started += instance.OnMovementA;
                @MovementA.performed += instance.OnMovementA;
                @MovementA.canceled += instance.OnMovementA;
                @MovementB.started += instance.OnMovementB;
                @MovementB.performed += instance.OnMovementB;
                @MovementB.canceled += instance.OnMovementB;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Separate.started += instance.OnSeparate;
                @Separate.performed += instance.OnSeparate;
                @Separate.canceled += instance.OnSeparate;
                @FistFiring.started += instance.OnFistFiring;
                @FistFiring.performed += instance.OnFistFiring;
                @FistFiring.canceled += instance.OnFistFiring;
            }
        }
    }
    public GameplaySeparateActions @GameplaySeparate => new GameplaySeparateActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Target;
    private readonly InputAction m_Menu_Accept;
    private readonly InputAction m_Menu_Back;
    public struct MenuActions
    {
        private @PlayerInputs m_Wrapper;
        public MenuActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Target => m_Wrapper.m_Menu_Target;
        public InputAction @Accept => m_Wrapper.m_Menu_Accept;
        public InputAction @Back => m_Wrapper.m_Menu_Back;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Target.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnTarget;
                @Target.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnTarget;
                @Target.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnTarget;
                @Accept.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                @Back.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Target.started += instance.OnTarget;
                @Target.performed += instance.OnTarget;
                @Target.canceled += instance.OnTarget;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IGameplayCombinedActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFiring(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSeparate(InputAction.CallbackContext context);
    }
    public interface IGameplaySeparateActions
    {
        void OnMovementA(InputAction.CallbackContext context);
        void OnMovementB(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSeparate(InputAction.CallbackContext context);
        void OnFistFiring(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnTarget(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
}
