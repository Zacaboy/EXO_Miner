//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Mech/PLayer.inputactions
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

public partial class @PLayer: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PLayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PLayer"",
    ""maps"": [
        {
            ""name"": ""JoystickController"",
            ""id"": ""e4cc0b43-e090-4461-97ff-02d0c3588484"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""740dd713-e817-4862-8e68-2ebb21930b56"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""36633f88-502f-430d-a906-d2b180203b26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e346b73b-697d-424d-8656-56f9403f4e46"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Flashlight"",
                    ""type"": ""Button"",
                    ""id"": ""3eade1ce-46e1-40b7-9908-d47520a52c67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mine"",
                    ""type"": ""Button"",
                    ""id"": ""47c5a425-d689-49c0-97ca-3552c50db536"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon1"",
                    ""type"": ""Button"",
                    ""id"": ""6af24dd3-e78e-4754-8814-4b499e6dac42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon2"",
                    ""type"": ""Value"",
                    ""id"": ""f70b1f94-c832-4507-b187-a3e65def488a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""453154de-9fd4-4ef0-b140-08f90816b240"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""86c0c896-d16f-4c8f-98ad-bbb11820e358"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d53986e5-1ef2-4d8d-81ec-6bd9eff98692"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a15cceed-a668-4a87-94b2-b652b5534ba4"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/stick/left"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9ba75f77-8b29-4fa7-bc66-d523d2b8a13c"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fde6c1fc-625d-463d-b0aa-4f69a6e20bdc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c04a835d-785d-42a5-bd43-a788ee4b2331"",
                    ""path"": ""<HID::Logitech Extreme 3D pro>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""66af5f63-7b55-4994-b848-3c402d184a22"",
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
                    ""id"": ""944df314-a7c4-405e-9961-474a5d628fc9"",
                    ""path"": ""<HID::Logitech Extreme 3D pro>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7fb22747-c60c-473a-b45f-ac3f781ce130"",
                    ""path"": ""<HID::Logitech Extreme 3D pro>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c0796035-d8dc-455d-9c82-80c01f7f91d6"",
                    ""path"": ""<HID::Logitech Extreme 3D pro>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18e2e05e-7bf4-4ff0-bdd7-76c2587c2a92"",
                    ""path"": ""<HID::Logitech Extreme 3D pro>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d7d713ac-fae9-4831-adde-cdc0ec801380"",
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
                    ""id"": ""77041b1c-d7ae-43c4-80e5-3a2c221df5e7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""96f6b0a0-e604-419d-a052-c444011a111f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0df81156-bf25-4bae-ab9f-bde0d61852a3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a6fa8cad-d86e-4008-af53-cfcb87467740"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""47ab46b8-78c3-4a9b-89cf-63dd0d6dfc1c"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/button8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""628a2f28-cad7-4f8b-bd99-e2c83b90598b"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60333411-4ce1-4c6d-a8f4-dc8a6b22c468"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/button2"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8ca355c-33a1-401e-939d-7a9dfb473dae"",
                    ""path"": ""<HID::Logitech Extreme 3D pro>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""id"": ""4627cd27-9d90-4e83-856d-ca11f6982310"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""9a0f6330-22e3-486d-ba94-3969ed1b3171"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a4dad89a-c44e-4176-8d9e-22fe2a56db98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""4ebc8c3a-60be-4890-a5ca-da739dc6d221"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Flashlight"",
                    ""type"": ""Button"",
                    ""id"": ""541dada8-542c-4cf6-909d-b31ee05810b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mine"",
                    ""type"": ""Button"",
                    ""id"": ""9eb52212-4bd9-4ce5-a9d0-a18d56d2b3a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon1"",
                    ""type"": ""Button"",
                    ""id"": ""46ad8662-2e80-4679-b88f-b8d597afe3bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon2"",
                    ""type"": ""Value"",
                    ""id"": ""eb1af4d7-53d4-4083-98ad-76d65c6bb1f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dd085fe0-eabc-4655-8d50-eaaad45faa0a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb6ee036-cbcc-40bb-98e1-d61889ff5667"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6ddb5f13-5f4d-4b55-acc4-7c7796ae29bc"",
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
                    ""id"": ""90aafaf1-4def-4ba7-8fe9-9fe962ab6409"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1c63a791-030b-475d-920b-60ab2de6cdec"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6848b7a2-d013-449b-a5a5-be3e6677889f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5c1a49bf-6d2b-49e8-89f9-83405c627c91"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c96e59ab-0597-4525-987b-04e3db6e8ce8"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad785b0e-c012-4e38-864c-9b896db644e2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""896f8edd-9854-4d0d-9ebf-3fc002e182ac"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92b5ffa5-0eec-4de1-9c05-765e3995995f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // JoystickController
        m_JoystickController = asset.FindActionMap("JoystickController", throwIfNotFound: true);
        m_JoystickController_Look = m_JoystickController.FindAction("Look", throwIfNotFound: true);
        m_JoystickController_Jump = m_JoystickController.FindAction("Jump", throwIfNotFound: true);
        m_JoystickController_Movement = m_JoystickController.FindAction("Movement", throwIfNotFound: true);
        m_JoystickController_Flashlight = m_JoystickController.FindAction("Flashlight", throwIfNotFound: true);
        m_JoystickController_Mine = m_JoystickController.FindAction("Mine", throwIfNotFound: true);
        m_JoystickController_Weapon1 = m_JoystickController.FindAction("Weapon1", throwIfNotFound: true);
        m_JoystickController_Weapon2 = m_JoystickController.FindAction("Weapon2", throwIfNotFound: true);
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Look = m_Keyboard.FindAction("Look", throwIfNotFound: true);
        m_Keyboard_Jump = m_Keyboard.FindAction("Jump", throwIfNotFound: true);
        m_Keyboard_Movement = m_Keyboard.FindAction("Movement", throwIfNotFound: true);
        m_Keyboard_Flashlight = m_Keyboard.FindAction("Flashlight", throwIfNotFound: true);
        m_Keyboard_Mine = m_Keyboard.FindAction("Mine", throwIfNotFound: true);
        m_Keyboard_Weapon1 = m_Keyboard.FindAction("Weapon1", throwIfNotFound: true);
        m_Keyboard_Weapon2 = m_Keyboard.FindAction("Weapon2", throwIfNotFound: true);
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

    // JoystickController
    private readonly InputActionMap m_JoystickController;
    private List<IJoystickControllerActions> m_JoystickControllerActionsCallbackInterfaces = new List<IJoystickControllerActions>();
    private readonly InputAction m_JoystickController_Look;
    private readonly InputAction m_JoystickController_Jump;
    private readonly InputAction m_JoystickController_Movement;
    private readonly InputAction m_JoystickController_Flashlight;
    private readonly InputAction m_JoystickController_Mine;
    private readonly InputAction m_JoystickController_Weapon1;
    private readonly InputAction m_JoystickController_Weapon2;
    public struct JoystickControllerActions
    {
        private @PLayer m_Wrapper;
        public JoystickControllerActions(@PLayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_JoystickController_Look;
        public InputAction @Jump => m_Wrapper.m_JoystickController_Jump;
        public InputAction @Movement => m_Wrapper.m_JoystickController_Movement;
        public InputAction @Flashlight => m_Wrapper.m_JoystickController_Flashlight;
        public InputAction @Mine => m_Wrapper.m_JoystickController_Mine;
        public InputAction @Weapon1 => m_Wrapper.m_JoystickController_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_JoystickController_Weapon2;
        public InputActionMap Get() { return m_Wrapper.m_JoystickController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JoystickControllerActions set) { return set.Get(); }
        public void AddCallbacks(IJoystickControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_JoystickControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_JoystickControllerActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Flashlight.started += instance.OnFlashlight;
            @Flashlight.performed += instance.OnFlashlight;
            @Flashlight.canceled += instance.OnFlashlight;
            @Mine.started += instance.OnMine;
            @Mine.performed += instance.OnMine;
            @Mine.canceled += instance.OnMine;
            @Weapon1.started += instance.OnWeapon1;
            @Weapon1.performed += instance.OnWeapon1;
            @Weapon1.canceled += instance.OnWeapon1;
            @Weapon2.started += instance.OnWeapon2;
            @Weapon2.performed += instance.OnWeapon2;
            @Weapon2.canceled += instance.OnWeapon2;
        }

        private void UnregisterCallbacks(IJoystickControllerActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Flashlight.started -= instance.OnFlashlight;
            @Flashlight.performed -= instance.OnFlashlight;
            @Flashlight.canceled -= instance.OnFlashlight;
            @Mine.started -= instance.OnMine;
            @Mine.performed -= instance.OnMine;
            @Mine.canceled -= instance.OnMine;
            @Weapon1.started -= instance.OnWeapon1;
            @Weapon1.performed -= instance.OnWeapon1;
            @Weapon1.canceled -= instance.OnWeapon1;
            @Weapon2.started -= instance.OnWeapon2;
            @Weapon2.performed -= instance.OnWeapon2;
            @Weapon2.canceled -= instance.OnWeapon2;
        }

        public void RemoveCallbacks(IJoystickControllerActions instance)
        {
            if (m_Wrapper.m_JoystickControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IJoystickControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_JoystickControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_JoystickControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public JoystickControllerActions @JoystickController => new JoystickControllerActions(this);

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private List<IKeyboardActions> m_KeyboardActionsCallbackInterfaces = new List<IKeyboardActions>();
    private readonly InputAction m_Keyboard_Look;
    private readonly InputAction m_Keyboard_Jump;
    private readonly InputAction m_Keyboard_Movement;
    private readonly InputAction m_Keyboard_Flashlight;
    private readonly InputAction m_Keyboard_Mine;
    private readonly InputAction m_Keyboard_Weapon1;
    private readonly InputAction m_Keyboard_Weapon2;
    public struct KeyboardActions
    {
        private @PLayer m_Wrapper;
        public KeyboardActions(@PLayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Keyboard_Look;
        public InputAction @Jump => m_Wrapper.m_Keyboard_Jump;
        public InputAction @Movement => m_Wrapper.m_Keyboard_Movement;
        public InputAction @Flashlight => m_Wrapper.m_Keyboard_Flashlight;
        public InputAction @Mine => m_Wrapper.m_Keyboard_Mine;
        public InputAction @Weapon1 => m_Wrapper.m_Keyboard_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_Keyboard_Weapon2;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void AddCallbacks(IKeyboardActions instance)
        {
            if (instance == null || m_Wrapper.m_KeyboardActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeyboardActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Flashlight.started += instance.OnFlashlight;
            @Flashlight.performed += instance.OnFlashlight;
            @Flashlight.canceled += instance.OnFlashlight;
            @Mine.started += instance.OnMine;
            @Mine.performed += instance.OnMine;
            @Mine.canceled += instance.OnMine;
            @Weapon1.started += instance.OnWeapon1;
            @Weapon1.performed += instance.OnWeapon1;
            @Weapon1.canceled += instance.OnWeapon1;
            @Weapon2.started += instance.OnWeapon2;
            @Weapon2.performed += instance.OnWeapon2;
            @Weapon2.canceled += instance.OnWeapon2;
        }

        private void UnregisterCallbacks(IKeyboardActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Flashlight.started -= instance.OnFlashlight;
            @Flashlight.performed -= instance.OnFlashlight;
            @Flashlight.canceled -= instance.OnFlashlight;
            @Mine.started -= instance.OnMine;
            @Mine.performed -= instance.OnMine;
            @Mine.canceled -= instance.OnMine;
            @Weapon1.started -= instance.OnWeapon1;
            @Weapon1.performed -= instance.OnWeapon1;
            @Weapon1.canceled -= instance.OnWeapon1;
            @Weapon2.started -= instance.OnWeapon2;
            @Weapon2.performed -= instance.OnWeapon2;
            @Weapon2.canceled -= instance.OnWeapon2;
        }

        public void RemoveCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyboardActions instance)
        {
            foreach (var item in m_Wrapper.m_KeyboardActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeyboardActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IJoystickControllerActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnFlashlight(InputAction.CallbackContext context);
        void OnMine(InputAction.CallbackContext context);
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnFlashlight(InputAction.CallbackContext context);
        void OnMine(InputAction.CallbackContext context);
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
    }
}
