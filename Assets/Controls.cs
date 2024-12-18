﻿// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""568f8164-d3a1-4afd-88a3-26a5e5456045"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1aec33c3-9b55-4620-9c2c-7a21a2dd2161"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""dc890858-0c13-4aa8-91c6-b27b6d96462a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slam"",
                    ""type"": ""Button"",
                    ""id"": ""cca7b228-c552-453e-8a13-1105dfa52472"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Wield"",
                    ""type"": ""Value"",
                    ""id"": ""0f641628-e188-4e61-a238-1160dccc46fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Angle"",
                    ""type"": ""Button"",
                    ""id"": ""7ada5061-8388-4911-a1a8-4c2ba01f33a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Value"",
                    ""id"": ""8046a645-cc1d-4b62-9641-8b76e34c0118"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""75f43a28-0a52-4a48-a60d-c9c7cf6d4bac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EscapeMenu"",
                    ""type"": ""Button"",
                    ""id"": ""69f93ba8-746a-47c7-9964-0c2dae55c945"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClickRelease"",
                    ""type"": ""Button"",
                    ""id"": ""bde1e516-71b2-44a7-80e6-4dcd1a0b86c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f0f7e313-9942-4e40-a840-629b3b06c096"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Wield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9f477a2-0a7a-4519-8ddb-45567cbec405"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=20,y=20)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Wield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd3f6ff1-948f-4c95-8c92-16d30d9827f7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Angle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c35d362f-fa16-4323-8e8e-24ce7c5bc792"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Angle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0450caf-dff9-4a1b-abf9-ff73b6b07109"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af6b5f64-97ce-477e-a4da-059a8d6914c2"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ebd88f0-cc93-4379-a501-47937cc4a8f3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""EscapeMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b40383ec-85d6-41ac-8a60-7e0fb3b90ba3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""EscapeMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""809eec82-c9d6-4e50-9548-1f53ef093a12"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ClickRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b1191fd-6ba1-449f-9f1c-27996cbfef87"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ClickRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e14c93a1-2616-4791-95b3-9a3fd5e4f359"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Slam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""369554dd-4e18-45e9-81d2-e8c1f001edcc"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c2ebbc47-b650-4fea-9d6f-61bf24325dff"",
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
                    ""id"": ""79cc56e7-6e36-4191-9b5d-e8c9784c55c1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7538962b-2cd8-4521-aa25-855743457c50"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis Arrows"",
                    ""id"": ""ec41494a-ce6b-410d-a8a0-0854df3ba1a2"",
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
                    ""id"": ""1d70129f-fefe-4c07-bcd0-0b4c4853801a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a3505d3f-a75c-486e-968e-8278ec57f064"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bf46869e-fedf-4eee-bc01-c3abd7f1aa15"",
                    ""path"": ""<Gamepad>/dpad/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9aa1e31d-759d-4ebc-b4fa-569e07fbd37a"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""deef4bcd-97f3-44d4-8602-f8177a9c4475"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03df9541-ecb2-4ec8-8543-13531e441e58"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edcf5e59-037d-4f3a-853e-92e3f2055241"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cde214c-3a29-442d-8513-62f5db24e64f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""675273c0-1e0f-443c-b7ba-042639057f61"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebd35d45-5255-4dfa-9998-f9faa78a6334"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Spectator"",
            ""id"": ""3fc4b2d6-4e63-4596-bd94-692b72266687"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3ddcf87c-8a64-4961-85a2-d2aba76b178b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fly"",
                    ""type"": ""Value"",
                    ""id"": ""e8e3e747-8ccb-4cc7-aea8-6cec697b0233"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""128a8b29-8ab1-4cc3-80f5-109b7198eac9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Button"",
                    ""id"": ""407c9280-0d4b-4e50-a15b-0c0147b05809"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b51bbdec-35b0-47a5-9f14-b05b44819d76"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aac18a86-94da-4380-9807-3873e1662f0f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1d23cacc-05d1-41cc-9980-4f6dfe1454f8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9ffa8994-7a36-42db-9112-b7d7f315a0f2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""90dda0e2-caaf-42ff-8131-000c362dcb51"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5788939c-4e3b-4391-9926-abe5833f3ef7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e59122fa-4325-4bb5-8864-521b1865a676"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e184ed0a-80de-4bac-9d30-ac6b63c7c0a6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e64d6a3c-cec7-42f2-9a66-405f3ceae252"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""29c68596-e101-496e-a774-ad829764aa44"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0d846bcd-6bb6-4d9b-b7c5-9cfedc93431a"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82d9fe3d-d8b4-4a62-b54c-009506880c27"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""bd3a30c2-0da5-46ab-b11a-ec3db5c5cd39"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fly"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""82300846-b4e0-4433-9bd3-2e37f4bc4532"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""47a74939-1fab-43bf-b1c3-6ef8f74dc4c7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""6ff03a7f-7685-4bdc-a472-6031e21926b8"",
            ""actions"": [
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""43e6d4ae-16b2-44ae-b2a6-3c8881e77245"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""44f11227-ac14-4827-b665-3cc759df9850"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MouseRelease"",
                    ""type"": ""Button"",
                    ""id"": ""9eaa0cb8-51ed-4c9f-b1c2-da1e8e48070a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""7114a289-e6d8-41f6-8555-65178cabe23c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""1b6f24fd-ac0f-4e2c-a4e0-4bc8793fe3b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""c4055920-377d-4548-82af-ee745cfa0a54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""TabRelease"",
                    ""type"": ""Button"",
                    ""id"": ""6a320eee-70f0-4073-9557-149459c7922b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2803a397-1bbb-4418-a510-588901ca6f79"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4285d471-1082-46ba-b728-2acca0eb3df8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d14abaab-6e0e-409b-9a9f-f0aa87b31b33"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""958a5678-949a-48e4-994c-7bcb68e28024"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76277e40-333f-4efd-8e33-c5353abd7155"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90340538-b502-40ba-8c02-f80768d19c25"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d88c84f-582e-4aac-997d-3905fc642fe9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92d21955-39fa-4614-a6b5-0d8264c94bdf"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bb4b660-68b5-49f1-a463-953671f8eb4a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8866d56-b003-4ec5-bf49-b805d3d2e0a0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MouseRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7cf2908-0c19-4723-9b15-b746f7854b5b"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e6eabaa-c090-4193-9167-fe9c8d05481c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5038ec7d-46bb-45a1-9656-824e6b9b831a"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TabRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5814cdc3-da38-4f82-a23b-7d1c48e7c30c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TabRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Slam = m_Player.FindAction("Slam", throwIfNotFound: true);
        m_Player_Wield = m_Player.FindAction("Wield", throwIfNotFound: true);
        m_Player_Angle = m_Player.FindAction("Angle", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Interaction = m_Player.FindAction("Interaction", throwIfNotFound: true);
        m_Player_EscapeMenu = m_Player.FindAction("EscapeMenu", throwIfNotFound: true);
        m_Player_ClickRelease = m_Player.FindAction("ClickRelease", throwIfNotFound: true);
        // Spectator
        m_Spectator = asset.FindActionMap("Spectator", throwIfNotFound: true);
        m_Spectator_Move = m_Spectator.FindAction("Move", throwIfNotFound: true);
        m_Spectator_Fly = m_Spectator.FindAction("Fly", throwIfNotFound: true);
        m_Spectator_Look = m_Spectator.FindAction("Look", throwIfNotFound: true);
        m_Spectator_Turn = m_Spectator.FindAction("Turn", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Mouse = m_UI.FindAction("Mouse", throwIfNotFound: true);
        m_UI_MouseClick = m_UI.FindAction("MouseClick", throwIfNotFound: true);
        m_UI_MouseRelease = m_UI.FindAction("MouseRelease", throwIfNotFound: true);
        m_UI_Scroll = m_UI.FindAction("Scroll", throwIfNotFound: true);
        m_UI_Exit = m_UI.FindAction("Exit", throwIfNotFound: true);
        m_UI_Tab = m_UI.FindAction("Tab", throwIfNotFound: true);
        m_UI_TabRelease = m_UI.FindAction("TabRelease", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Slam;
    private readonly InputAction m_Player_Wield;
    private readonly InputAction m_Player_Angle;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Interaction;
    private readonly InputAction m_Player_EscapeMenu;
    private readonly InputAction m_Player_ClickRelease;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Slam => m_Wrapper.m_Player_Slam;
        public InputAction @Wield => m_Wrapper.m_Player_Wield;
        public InputAction @Angle => m_Wrapper.m_Player_Angle;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Interaction => m_Wrapper.m_Player_Interaction;
        public InputAction @EscapeMenu => m_Wrapper.m_Player_EscapeMenu;
        public InputAction @ClickRelease => m_Wrapper.m_Player_ClickRelease;
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
                @Slam.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlam;
                @Slam.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlam;
                @Slam.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlam;
                @Wield.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWield;
                @Wield.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWield;
                @Wield.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWield;
                @Angle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAngle;
                @Angle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAngle;
                @Angle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAngle;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Interaction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @EscapeMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEscapeMenu;
                @EscapeMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEscapeMenu;
                @EscapeMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEscapeMenu;
                @ClickRelease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClickRelease;
                @ClickRelease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClickRelease;
                @ClickRelease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClickRelease;
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
                @Slam.started += instance.OnSlam;
                @Slam.performed += instance.OnSlam;
                @Slam.canceled += instance.OnSlam;
                @Wield.started += instance.OnWield;
                @Wield.performed += instance.OnWield;
                @Wield.canceled += instance.OnWield;
                @Angle.started += instance.OnAngle;
                @Angle.performed += instance.OnAngle;
                @Angle.canceled += instance.OnAngle;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @EscapeMenu.started += instance.OnEscapeMenu;
                @EscapeMenu.performed += instance.OnEscapeMenu;
                @EscapeMenu.canceled += instance.OnEscapeMenu;
                @ClickRelease.started += instance.OnClickRelease;
                @ClickRelease.performed += instance.OnClickRelease;
                @ClickRelease.canceled += instance.OnClickRelease;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Spectator
    private readonly InputActionMap m_Spectator;
    private ISpectatorActions m_SpectatorActionsCallbackInterface;
    private readonly InputAction m_Spectator_Move;
    private readonly InputAction m_Spectator_Fly;
    private readonly InputAction m_Spectator_Look;
    private readonly InputAction m_Spectator_Turn;
    public struct SpectatorActions
    {
        private @Controls m_Wrapper;
        public SpectatorActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Spectator_Move;
        public InputAction @Fly => m_Wrapper.m_Spectator_Fly;
        public InputAction @Look => m_Wrapper.m_Spectator_Look;
        public InputAction @Turn => m_Wrapper.m_Spectator_Turn;
        public InputActionMap Get() { return m_Wrapper.m_Spectator; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpectatorActions set) { return set.Get(); }
        public void SetCallbacks(ISpectatorActions instance)
        {
            if (m_Wrapper.m_SpectatorActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnMove;
                @Fly.started -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnFly;
                @Fly.performed -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnFly;
                @Fly.canceled -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnFly;
                @Look.started -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnLook;
                @Turn.started -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_SpectatorActionsCallbackInterface.OnTurn;
            }
            m_Wrapper.m_SpectatorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fly.started += instance.OnFly;
                @Fly.performed += instance.OnFly;
                @Fly.canceled += instance.OnFly;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
            }
        }
    }
    public SpectatorActions @Spectator => new SpectatorActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Mouse;
    private readonly InputAction m_UI_MouseClick;
    private readonly InputAction m_UI_MouseRelease;
    private readonly InputAction m_UI_Scroll;
    private readonly InputAction m_UI_Exit;
    private readonly InputAction m_UI_Tab;
    private readonly InputAction m_UI_TabRelease;
    public struct UIActions
    {
        private @Controls m_Wrapper;
        public UIActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse => m_Wrapper.m_UI_Mouse;
        public InputAction @MouseClick => m_Wrapper.m_UI_MouseClick;
        public InputAction @MouseRelease => m_Wrapper.m_UI_MouseRelease;
        public InputAction @Scroll => m_Wrapper.m_UI_Scroll;
        public InputAction @Exit => m_Wrapper.m_UI_Exit;
        public InputAction @Tab => m_Wrapper.m_UI_Tab;
        public InputAction @TabRelease => m_Wrapper.m_UI_TabRelease;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Mouse.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMouse;
                @MouseClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseClick;
                @MouseRelease.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseRelease;
                @MouseRelease.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseRelease;
                @MouseRelease.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseRelease;
                @Scroll.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScroll;
                @Exit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnExit;
                @Tab.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @TabRelease.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTabRelease;
                @TabRelease.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTabRelease;
                @TabRelease.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTabRelease;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @MouseRelease.started += instance.OnMouseRelease;
                @MouseRelease.performed += instance.OnMouseRelease;
                @MouseRelease.canceled += instance.OnMouseRelease;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @TabRelease.started += instance.OnTabRelease;
                @TabRelease.performed += instance.OnTabRelease;
                @TabRelease.canceled += instance.OnTabRelease;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSlam(InputAction.CallbackContext context);
        void OnWield(InputAction.CallbackContext context);
        void OnAngle(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnEscapeMenu(InputAction.CallbackContext context);
        void OnClickRelease(InputAction.CallbackContext context);
    }
    public interface ISpectatorActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFly(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMouse(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnMouseRelease(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnTabRelease(InputAction.CallbackContext context);
    }
}
