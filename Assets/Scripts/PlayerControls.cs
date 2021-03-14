// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ebd8f511-56b7-4864-a422-45e664c2ad65"",
            ""actions"": [
                {
                    ""name"": ""Go"",
                    ""type"": ""Button"",
                    ""id"": ""e3f72f6b-0c28-4041-8055-efe755f0d532"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""dc0c702e-d779-409b-85dc-ddcde7b71cd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MosuePosition"",
                    ""type"": ""Value"",
                    ""id"": ""e87488d4-59f3-4973-b278-9057515b8709"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""763d53eb-8c6c-4ea2-acf1-ac5bbd2ff257"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""35c6d9bc-bdee-48ba-8384-a4722572ea16"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0be413dd-f0f9-4c45-bace-f9c4e9f08b52"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""849dff11-7e5c-4779-a5b6-4074aa0aac5e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MosuePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5c8d124-6a96-48f1-ab13-c61144970c19"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold(duration=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
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
        m_Player_Go = m_Player.FindAction("Go", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_MosuePosition = m_Player.FindAction("MosuePosition", throwIfNotFound: true);
        m_Player_Drop = m_Player.FindAction("Drop", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Go;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_MosuePosition;
    private readonly InputAction m_Player_Drop;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Go => m_Wrapper.m_Player_Go;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @MosuePosition => m_Wrapper.m_Player_MosuePosition;
        public InputAction @Drop => m_Wrapper.m_Player_Drop;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Go.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGo;
                @Go.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGo;
                @Go.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGo;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @MosuePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMosuePosition;
                @MosuePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMosuePosition;
                @MosuePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMosuePosition;
                @Drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Go.started += instance.OnGo;
                @Go.performed += instance.OnGo;
                @Go.canceled += instance.OnGo;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @MosuePosition.started += instance.OnMosuePosition;
                @MosuePosition.performed += instance.OnMosuePosition;
                @MosuePosition.canceled += instance.OnMosuePosition;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnGo(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMosuePosition(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
    }
}
