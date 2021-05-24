// GENERATED AUTOMATICALLY FROM 'Assets/InputManager/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""8263681d-ea8a-4b35-a645-ac0e179b8eb5"",
            ""actions"": [
                {
                    ""name"": ""Axis"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8d0202e3-be8a-41a5-8349-4ba55be70cc4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionStart"",
                    ""type"": ""Button"",
                    ""id"": ""2d560084-af7a-4710-8af8-037cfef2222c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ActionEnd"",
                    ""type"": ""Button"",
                    ""id"": ""0138cc57-54a4-46ef-9d9b-1396ca6ecb5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""58ba4774-6e20-4cf7-858c-84ba5e5be92b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d6a31781-0795-454f-89da-36b521e68798"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""644c8cd2-b5aa-4e7e-bf29-554b3e85a954"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""00b42e94-0b1e-4989-a8ec-f2a4a8c38a18"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""31790319-1f73-40d4-a31d-ea9ff0fbc73c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""287587f3-269b-4988-bdc7-c22f039ccc5a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f22bc887-1a42-4e03-b78f-590502b609ae"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""66e3dffa-09ac-4bb6-a277-0e9a3f10f255"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""86b121b6-ec66-466d-ae62-5c4881fdb926"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fe22caec-bedf-47d0-b73a-187a27d7e9e4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1a0c6dbf-8f57-4677-8c10-791e0f7e88ad"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ActionStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3f89227-a303-42ce-aa95-3a2868e77e64"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ActionEnd"",
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
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
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
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Axis = m_Movement.FindAction("Axis", throwIfNotFound: true);
        m_Movement_ActionStart = m_Movement.FindAction("ActionStart", throwIfNotFound: true);
        m_Movement_ActionEnd = m_Movement.FindAction("ActionEnd", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Axis;
    private readonly InputAction m_Movement_ActionStart;
    private readonly InputAction m_Movement_ActionEnd;
    public struct MovementActions
    {
        private @InputActions m_Wrapper;
        public MovementActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Axis => m_Wrapper.m_Movement_Axis;
        public InputAction @ActionStart => m_Wrapper.m_Movement_ActionStart;
        public InputAction @ActionEnd => m_Wrapper.m_Movement_ActionEnd;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Axis.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnAxis;
                @Axis.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnAxis;
                @Axis.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnAxis;
                @ActionStart.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnActionStart;
                @ActionStart.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnActionStart;
                @ActionStart.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnActionStart;
                @ActionEnd.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnActionEnd;
                @ActionEnd.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnActionEnd;
                @ActionEnd.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnActionEnd;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Axis.started += instance.OnAxis;
                @Axis.performed += instance.OnAxis;
                @Axis.canceled += instance.OnAxis;
                @ActionStart.started += instance.OnActionStart;
                @ActionStart.performed += instance.OnActionStart;
                @ActionStart.canceled += instance.OnActionStart;
                @ActionEnd.started += instance.OnActionEnd;
                @ActionEnd.performed += instance.OnActionEnd;
                @ActionEnd.canceled += instance.OnActionEnd;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnAxis(InputAction.CallbackContext context);
        void OnActionStart(InputAction.CallbackContext context);
        void OnActionEnd(InputAction.CallbackContext context);
    }
}
