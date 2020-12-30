//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.0
//     from Assets/InputActions/FlyDangerousActions.inputactions
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

public partial class @FlyDangerousActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @FlyDangerousActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""FlyDangerousActions"",
    ""maps"": [
        {
            ""name"": ""Ship"",
            ""id"": ""319c08b3-97e0-4636-a79a-bb6bde0a90f7"",
            ""actions"": [
                {
                    ""name"": ""Pitch"",
                    ""type"": ""Value"",
                    ""id"": ""8043ded1-0ef3-4f85-b28b-3e3139edb133"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Value"",
                    ""id"": ""4fd269dd-a7ed-42d2-864e-e528e090bd09"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Yaw"",
                    ""type"": ""Value"",
                    ""id"": ""f1e682e9-2180-45dd-bf3e-cdefbee29575"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throttle"",
                    ""type"": ""Value"",
                    ""id"": ""725b3d83-6282-4fd3-8a1b-ad37029f5146"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LateralH"",
                    ""type"": ""Value"",
                    ""id"": ""509f03a6-fbc1-428b-be5b-adef4a4a1d5f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LateralV"",
                    ""type"": ""Value"",
                    ""id"": ""dbbf62a4-6b89-4ff1-b0c2-1399be3f5b1f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""ae9270a1-0fa9-4611-bb01-798738d7842c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FlightAssistToggle"",
                    ""type"": ""Button"",
                    ""id"": ""ce9c21e5-76be-402d-beed-36b5b132f0ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""89d913f5-1c0b-4f48-ad2b-3bcf86512369"",
                    ""path"": ""<Joystick>/stick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard axis"",
                    ""id"": ""0c3f738d-702d-461a-ad1a-035961539daa"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pitch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3f84313b-3551-4448-a201-85f7bff2ea23"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9298a74d-c1c7-4f47-bf8c-387b27c32459"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d9365d2d-6058-4a31-b55d-b2b8fb28062c"",
                    ""path"": ""<Joystick>/z"",
                    ""interactions"": """",
                    ""processors"": ""Invert,Normalize(min=-1,max=0.722)"",
                    ""groups"": ""flight stick"",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard axis"",
                    ""id"": ""63f29310-dc19-451f-94cc-4ad6e84f182b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c081f54c-70ab-423f-b2d8-2efdc1761fe0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a749d237-31cd-4d12-90f5-b03d4cf04cfc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7abb1e17-bf1a-4f18-bde7-9f3833f1f333"",
                    ""path"": ""<Joystick>/stick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard axis"",
                    ""id"": ""8120d13b-aea1-4179-993b-df84b0940b34"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""60058f84-5d0d-4c06-aa72-4365a03feda5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""acb04a75-4a54-43b8-83cf-d9966afa224b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""331ba9bf-d059-408b-9c90-91a373888d8b"",
                    ""path"": ""<Joystick>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard axis"",
                    ""id"": ""e6ecec4c-37ac-4679-8d7d-5e761a20654f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""64ccfdd2-3c2a-4961-9687-010eef565c5c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8092fd1c-b58a-4a58-8b47-11ff7c7d69d5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard axis"",
                    ""id"": ""bcd22e4c-b938-4fe0-8fb0-b23cae25bd58"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralH"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f5f5b2b9-5172-45b4-8818-0c4daadfb4f1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""LateralH"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""331a2f6e-cd1a-4b4f-9ae7-5a4a87416cc1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""LateralH"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""473a7484-45f4-4487-974b-1c2217427e04"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralH"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""eba04c70-1cff-462b-bfd5-86e5c2d2a139"",
                    ""path"": ""<HID::Saitek Saitek X52 Pro Flight Control System>/button25"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""LateralH"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5f43ec82-7fb8-466f-9ddd-fc08b5f72652"",
                    ""path"": ""<HID::Saitek Saitek X52 Pro Flight Control System>/button27"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""LateralH"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard axis"",
                    ""id"": ""78f6e3b5-17bb-458c-8b1c-3fa19d8b382b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralV"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""494849b9-b571-4ba5-8cb7-521964f6e452"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""LateralV"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""550a0776-272e-43a0-978a-23d708dc2a09"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""LateralV"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a9c40068-8436-4f06-81ac-0220d3b71ccc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralV"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7a93a7bb-fd73-4963-b375-2e4713846ee7"",
                    ""path"": ""<HID::Saitek Saitek X52 Pro Flight Control System>/button24"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""LateralV"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3570a57c-02f8-46fc-95c8-ea6f9b0dce14"",
                    ""path"": ""<HID::Saitek Saitek X52 Pro Flight Control System>/button26"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""LateralV"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""14aaf69e-25ad-48cb-9525-86177bca1162"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54866b4b-381b-451e-ae16-5b261fad8874"",
                    ""path"": ""<Joystick>/button31"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc747143-d180-4195-aa6c-e34a265716fc"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard + mouse"",
                    ""action"": ""FlightAssistToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c79d88a-9c6c-4284-9ec7-98990003e4e8"",
                    ""path"": ""<Joystick>/button7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""flight stick"",
                    ""action"": ""FlightAssistToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""keyboard + mouse"",
            ""bindingGroup"": ""keyboard + mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""flight stick"",
            ""bindingGroup"": ""flight stick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Ship
        m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
        m_Ship_Pitch = m_Ship.FindAction("Pitch", throwIfNotFound: true);
        m_Ship_Roll = m_Ship.FindAction("Roll", throwIfNotFound: true);
        m_Ship_Yaw = m_Ship.FindAction("Yaw", throwIfNotFound: true);
        m_Ship_Throttle = m_Ship.FindAction("Throttle", throwIfNotFound: true);
        m_Ship_LateralH = m_Ship.FindAction("LateralH", throwIfNotFound: true);
        m_Ship_LateralV = m_Ship.FindAction("LateralV", throwIfNotFound: true);
        m_Ship_Boost = m_Ship.FindAction("Boost", throwIfNotFound: true);
        m_Ship_FlightAssistToggle = m_Ship.FindAction("FlightAssistToggle", throwIfNotFound: true);
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

    // Ship
    private readonly InputActionMap m_Ship;
    private IShipActions m_ShipActionsCallbackInterface;
    private readonly InputAction m_Ship_Pitch;
    private readonly InputAction m_Ship_Roll;
    private readonly InputAction m_Ship_Yaw;
    private readonly InputAction m_Ship_Throttle;
    private readonly InputAction m_Ship_LateralH;
    private readonly InputAction m_Ship_LateralV;
    private readonly InputAction m_Ship_Boost;
    private readonly InputAction m_Ship_FlightAssistToggle;
    public struct ShipActions
    {
        private @FlyDangerousActions m_Wrapper;
        public ShipActions(@FlyDangerousActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pitch => m_Wrapper.m_Ship_Pitch;
        public InputAction @Roll => m_Wrapper.m_Ship_Roll;
        public InputAction @Yaw => m_Wrapper.m_Ship_Yaw;
        public InputAction @Throttle => m_Wrapper.m_Ship_Throttle;
        public InputAction @LateralH => m_Wrapper.m_Ship_LateralH;
        public InputAction @LateralV => m_Wrapper.m_Ship_LateralV;
        public InputAction @Boost => m_Wrapper.m_Ship_Boost;
        public InputAction @FlightAssistToggle => m_Wrapper.m_Ship_FlightAssistToggle;
        public InputActionMap Get() { return m_Wrapper.m_Ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void SetCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterface != null)
            {
                @Pitch.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnPitch;
                @Pitch.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnPitch;
                @Pitch.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnPitch;
                @Roll.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnRoll;
                @Yaw.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnYaw;
                @Yaw.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnYaw;
                @Yaw.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnYaw;
                @Throttle.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnThrottle;
                @Throttle.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnThrottle;
                @Throttle.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnThrottle;
                @LateralH.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnLateralH;
                @LateralH.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnLateralH;
                @LateralH.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnLateralH;
                @LateralV.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnLateralV;
                @LateralV.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnLateralV;
                @LateralV.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnLateralV;
                @Boost.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnBoost;
                @FlightAssistToggle.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnFlightAssistToggle;
                @FlightAssistToggle.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnFlightAssistToggle;
                @FlightAssistToggle.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnFlightAssistToggle;
            }
            m_Wrapper.m_ShipActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pitch.started += instance.OnPitch;
                @Pitch.performed += instance.OnPitch;
                @Pitch.canceled += instance.OnPitch;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Yaw.started += instance.OnYaw;
                @Yaw.performed += instance.OnYaw;
                @Yaw.canceled += instance.OnYaw;
                @Throttle.started += instance.OnThrottle;
                @Throttle.performed += instance.OnThrottle;
                @Throttle.canceled += instance.OnThrottle;
                @LateralH.started += instance.OnLateralH;
                @LateralH.performed += instance.OnLateralH;
                @LateralH.canceled += instance.OnLateralH;
                @LateralV.started += instance.OnLateralV;
                @LateralV.performed += instance.OnLateralV;
                @LateralV.canceled += instance.OnLateralV;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
                @FlightAssistToggle.started += instance.OnFlightAssistToggle;
                @FlightAssistToggle.performed += instance.OnFlightAssistToggle;
                @FlightAssistToggle.canceled += instance.OnFlightAssistToggle;
            }
        }
    }
    public ShipActions @Ship => new ShipActions(this);
    private int m_keyboardmouseSchemeIndex = -1;
    public InputControlScheme keyboardmouseScheme
    {
        get
        {
            if (m_keyboardmouseSchemeIndex == -1) m_keyboardmouseSchemeIndex = asset.FindControlSchemeIndex("keyboard + mouse");
            return asset.controlSchemes[m_keyboardmouseSchemeIndex];
        }
    }
    private int m_flightstickSchemeIndex = -1;
    public InputControlScheme flightstickScheme
    {
        get
        {
            if (m_flightstickSchemeIndex == -1) m_flightstickSchemeIndex = asset.FindControlSchemeIndex("flight stick");
            return asset.controlSchemes[m_flightstickSchemeIndex];
        }
    }
    public interface IShipActions
    {
        void OnPitch(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnYaw(InputAction.CallbackContext context);
        void OnThrottle(InputAction.CallbackContext context);
        void OnLateralH(InputAction.CallbackContext context);
        void OnLateralV(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
        void OnFlightAssistToggle(InputAction.CallbackContext context);
    }
}
