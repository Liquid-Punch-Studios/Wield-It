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
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""dc890858-0c13-4aa8-91c6-b27b6d96462a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
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
                    ""name"": ""Dash Right"",
                    ""type"": ""Button"",
                    ""id"": ""b93a6e58-a613-4367-96e9-14a78d519e7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash Left"",
                    ""type"": ""Button"",
                    ""id"": ""6f8fcccb-b1b2-4c09-8333-e60e5db8204e"",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""Angle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""4638660c-5c2b-4981-a3fe-c8930d40c3db"",
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
                    ""id"": ""26ed8d1b-1786-414f-8ad9-e64c5bc92c81"",
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
                    ""id"": ""84b4aad1-24b1-4791-acc7-d80ff7411905"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c87679b1-842f-4fca-b361-a5a1f5a4f622"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""261c6644-27c0-49f5-82ab-78a428b56b9f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da7e0187-100b-443f-94bd-f6e18779bdec"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""MultiTap(tapTime=0.15,tapDelay=0.15)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b915f6d-53fd-4a71-a78e-be8d05c04ee8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""MultiTap(tapTime=0.15,tapDelay=0.15)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash Left"",
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
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""7114a289-e6d8-41f6-8555-65178cabe23c"",
                    ""expectedControlType"": ""Vector2"",
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
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""Scroll"",
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
        m_Player_Up = m_Player.FindAction("Up", throwIfNotFound: true);
        m_Player_Down = m_Player.FindAction("Down", throwIfNotFound: true);
        m_Player_Wield = m_Player.FindAction("Wield", throwIfNotFound: true);
        m_Player_Angle = m_Player.FindAction("Angle", throwIfNotFound: true);
        m_Player_DashRight = m_Player.FindAction("Dash Right", throwIfNotFound: true);
        m_Player_DashLeft = m_Player.FindAction("Dash Left", throwIfNotFound: true);
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
        m_UI_Scroll = m_UI.FindAction("Scroll", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Up;
    private readonly InputAction m_Player_Down;
    private readonly InputAction m_Player_Wield;
    private readonly InputAction m_Player_Angle;
    private readonly InputAction m_Player_DashRight;
    private readonly InputAction m_Player_DashLeft;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Up => m_Wrapper.m_Player_Up;
        public InputAction @Down => m_Wrapper.m_Player_Down;
        public InputAction @Wield => m_Wrapper.m_Player_Wield;
        public InputAction @Angle => m_Wrapper.m_Player_Angle;
        public InputAction @DashRight => m_Wrapper.m_Player_DashRight;
        public InputAction @DashLeft => m_Wrapper.m_Player_DashLeft;
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
                @Up.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Wield.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWield;
                @Wield.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWield;
                @Wield.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWield;
                @Angle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAngle;
                @Angle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAngle;
                @Angle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAngle;
                @DashRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashRight;
                @DashRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashRight;
                @DashRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashRight;
                @DashLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashLeft;
                @DashLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashLeft;
                @DashLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashLeft;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Wield.started += instance.OnWield;
                @Wield.performed += instance.OnWield;
                @Wield.canceled += instance.OnWield;
                @Angle.started += instance.OnAngle;
                @Angle.performed += instance.OnAngle;
                @Angle.canceled += instance.OnAngle;
                @DashRight.started += instance.OnDashRight;
                @DashRight.performed += instance.OnDashRight;
                @DashRight.canceled += instance.OnDashRight;
                @DashLeft.started += instance.OnDashLeft;
                @DashLeft.performed += instance.OnDashLeft;
                @DashLeft.canceled += instance.OnDashLeft;
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
    private readonly InputAction m_UI_Scroll;
    public struct UIActions
    {
        private @Controls m_Wrapper;
        public UIActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse => m_Wrapper.m_UI_Mouse;
        public InputAction @MouseClick => m_Wrapper.m_UI_MouseClick;
        public InputAction @Scroll => m_Wrapper.m_UI_Scroll;
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
                @Scroll.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScroll;
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
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnWield(InputAction.CallbackContext context);
        void OnAngle(InputAction.CallbackContext context);
        void OnDashRight(InputAction.CallbackContext context);
        void OnDashLeft(InputAction.CallbackContext context);
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
        void OnScroll(InputAction.CallbackContext context);
    }
}