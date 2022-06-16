//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/InputAction/KeyboardControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @KeyboardControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @KeyboardControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""KeyboardControls"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""a9d5c0b4-8a70-472a-ae47-72a84e48db6e"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""fa785c55-652d-488e-8bfb-23e0a5bc2e5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""76b006d4-65cd-4578-b008-19f429a357d3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MiddleButtonHold"",
                    ""type"": ""Button"",
                    ""id"": ""a2c25df2-7eb3-48f3-9dc2-5d042e2a6034"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.05)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""a5b46637-f2f2-4ac9-887a-fd8b38c98c7c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePositionDelta"",
                    ""type"": ""Value"",
                    ""id"": ""e1e0819b-222e-481c-9790-c8dad29adbe0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""537dee87-4b61-4635-8318-c0775d31f5ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a06c2272-b217-4abd-8f71-c8c5e5310017"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""4e34826e-9305-4792-b337-29e164f3ea16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af17a8fd-7b77-491c-aec0-46d5f9f2db2a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a30a0e80-2f10-47bf-8adf-68802be34a18"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80575e6a-cb8e-4161-8437-e1ebf1993716"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MiddleButtonHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83900499-5f3f-4938-8214-9237ab4cd09d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a247904f-e512-4490-97ae-a9cdd3142eb2"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePositionDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afe57e03-f8cb-4925-a22e-6c3c7120dc46"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e762ab61-4b00-4bbb-971d-c13b642681bb"",
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
                    ""id"": ""77723745-e8bf-4cbd-bbc1-3830a8758e11"",
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
                    ""id"": ""705ed8b2-90b3-4d1a-92d3-33abcc0d4783"",
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
                    ""id"": ""d0ca9417-1499-4bbf-8009-d89ef1dbf615"",
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
                    ""id"": ""c1175085-5658-420a-ae25-9a593676f0ee"",
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
                    ""id"": ""1b6b92ed-1b8d-4a2e-8f42-2fd4776b5d28"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_LeftClick = m_Mouse.FindAction("LeftClick", throwIfNotFound: true);
        m_Mouse_MousePosition = m_Mouse.FindAction("MousePosition", throwIfNotFound: true);
        m_Mouse_MiddleButtonHold = m_Mouse.FindAction("MiddleButtonHold", throwIfNotFound: true);
        m_Mouse_Scroll = m_Mouse.FindAction("Scroll", throwIfNotFound: true);
        m_Mouse_MousePositionDelta = m_Mouse.FindAction("MousePositionDelta", throwIfNotFound: true);
        m_Mouse_RightClick = m_Mouse.FindAction("RightClick", throwIfNotFound: true);
        m_Mouse_Move = m_Mouse.FindAction("Move", throwIfNotFound: true);
        m_Mouse_Interact = m_Mouse.FindAction("Interact", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_LeftClick;
    private readonly InputAction m_Mouse_MousePosition;
    private readonly InputAction m_Mouse_MiddleButtonHold;
    private readonly InputAction m_Mouse_Scroll;
    private readonly InputAction m_Mouse_MousePositionDelta;
    private readonly InputAction m_Mouse_RightClick;
    private readonly InputAction m_Mouse_Move;
    private readonly InputAction m_Mouse_Interact;
    public struct MouseActions
    {
        private @KeyboardControls m_Wrapper;
        public MouseActions(@KeyboardControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_Mouse_LeftClick;
        public InputAction @MousePosition => m_Wrapper.m_Mouse_MousePosition;
        public InputAction @MiddleButtonHold => m_Wrapper.m_Mouse_MiddleButtonHold;
        public InputAction @Scroll => m_Wrapper.m_Mouse_Scroll;
        public InputAction @MousePositionDelta => m_Wrapper.m_Mouse_MousePositionDelta;
        public InputAction @RightClick => m_Wrapper.m_Mouse_RightClick;
        public InputAction @Move => m_Wrapper.m_Mouse_Move;
        public InputAction @Interact => m_Wrapper.m_Mouse_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftClick;
                @MousePosition.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePosition;
                @MiddleButtonHold.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMiddleButtonHold;
                @MiddleButtonHold.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMiddleButtonHold;
                @MiddleButtonHold.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMiddleButtonHold;
                @Scroll.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnScroll;
                @MousePositionDelta.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePositionDelta;
                @MousePositionDelta.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePositionDelta;
                @MousePositionDelta.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMousePositionDelta;
                @RightClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightClick;
                @Move.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MiddleButtonHold.started += instance.OnMiddleButtonHold;
                @MiddleButtonHold.performed += instance.OnMiddleButtonHold;
                @MiddleButtonHold.canceled += instance.OnMiddleButtonHold;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @MousePositionDelta.started += instance.OnMousePositionDelta;
                @MousePositionDelta.performed += instance.OnMousePositionDelta;
                @MousePositionDelta.canceled += instance.OnMousePositionDelta;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IMouseActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMiddleButtonHold(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnMousePositionDelta(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
