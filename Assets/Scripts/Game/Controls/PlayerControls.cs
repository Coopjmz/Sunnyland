using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Sunnyland.Game.Controls
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""ef3639cc-a5c0-4f84-929a-5097f9e633d3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""201cb41e-fad5-4f76-97d6-8a1d03d46202"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Climb"",
                    ""type"": ""Button"",
                    ""id"": ""d607b5e2-91e4-49bd-8149-10e5767f413e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""11bd9998-8169-453d-81b0-fd71acb52a62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""737c3481-8159-432c-b3fe-61d0d17e0072"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""X axis"",
                    ""id"": ""3ae652f8-7301-4ea5-95dc-b0fee861f399"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b326ade0-25ec-41bd-a9f2-ad7fc2633fd1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2a078cd1-2aa4-402b-b71f-637b3160ba28"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""61978aa8-c5d3-4699-9e31-5b6c22dcb824"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8040052d-c8b1-48df-8fa3-07be5b928f59"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Y axis"",
                    ""id"": ""b5bd1d30-1bf1-4f0d-a708-5d849d032581"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""edab14fd-0632-4fc3-9e79-ce372b1ccfa0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""708e627c-0495-4dde-a297-92f0296a2103"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Utility"",
            ""id"": ""4b3cad03-71e5-40f2-9a3c-70ebee6fcd9d"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""125ff074-6394-4f59-ac7d-12b410e58050"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c3812227-c5c2-4260-8438-d15a4dc5d76d"",
                    ""path"": ""<Keyboard>/space"",
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
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
            m_Movement_Climb = m_Movement.FindAction("Climb", throwIfNotFound: true);
            m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
            m_Movement_Crouch = m_Movement.FindAction("Crouch", throwIfNotFound: true);
            // Utility
            m_Utility = asset.FindActionMap("Utility", throwIfNotFound: true);
            m_Utility_Interact = m_Utility.FindAction("Interact", throwIfNotFound: true);
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
        private readonly InputAction m_Movement_Move;
        private readonly InputAction m_Movement_Climb;
        private readonly InputAction m_Movement_Jump;
        private readonly InputAction m_Movement_Crouch;
        public struct MovementActions
        {
            private @PlayerControls m_Wrapper;
            public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Movement_Move;
            public InputAction @Climb => m_Wrapper.m_Movement_Climb;
            public InputAction @Jump => m_Wrapper.m_Movement_Jump;
            public InputAction @Crouch => m_Wrapper.m_Movement_Crouch;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Climb.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnClimb;
                    @Climb.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnClimb;
                    @Climb.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnClimb;
                    @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                    @Crouch.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                    @Crouch.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                    @Crouch.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Climb.started += instance.OnClimb;
                    @Climb.performed += instance.OnClimb;
                    @Climb.canceled += instance.OnClimb;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Crouch.started += instance.OnCrouch;
                    @Crouch.performed += instance.OnCrouch;
                    @Crouch.canceled += instance.OnCrouch;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Utility
        private readonly InputActionMap m_Utility;
        private IUtilityActions m_UtilityActionsCallbackInterface;
        private readonly InputAction m_Utility_Interact;
        public struct UtilityActions
        {
            private @PlayerControls m_Wrapper;
            public UtilityActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Interact => m_Wrapper.m_Utility_Interact;
            public InputActionMap Get() { return m_Wrapper.m_Utility; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UtilityActions set) { return set.Get(); }
            public void SetCallbacks(IUtilityActions instance)
            {
                if (m_Wrapper.m_UtilityActionsCallbackInterface != null)
                {
                    @Interact.started -= m_Wrapper.m_UtilityActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_UtilityActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_UtilityActionsCallbackInterface.OnInteract;
                }
                m_Wrapper.m_UtilityActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                }
            }
        }
        public UtilityActions @Utility => new UtilityActions(this);
        public interface IMovementActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnClimb(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
        }
        public interface IUtilityActions
        {
            void OnInteract(InputAction.CallbackContext context);
        }
    }
}
