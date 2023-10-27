//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Resources/InputManager/ControllerInput.inputactions
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

public partial class @ControllerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControllerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControllerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""cc3eca78-b394-4bf0-9366-a828bf09d5d3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""caac699e-fd85-4b6a-9f83-733d2cd9915e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SkillA"",
                    ""type"": ""Button"",
                    ""id"": ""c6529f72-a31d-4392-ae7b-a7385643b4de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillB"",
                    ""type"": ""Button"",
                    ""id"": ""e5aeabc9-0826-4797-9da2-14cb3b081f10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f51e01de-9331-41be-aa04-7d39d06f9fd6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d52aaeee-8410-4760-b899-21fab75d7ed0"",
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
                    ""id"": ""5c38ad0c-88d8-49e4-a09b-ae89f5cdb124"",
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
                    ""id"": ""c463582e-af70-407c-9240-aab753bf52ed"",
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
                    ""id"": ""b9ceed1d-292d-4e7e-a9b6-1c4f13968108"",
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
                    ""id"": ""eb666429-0905-4e1a-aa53-978a7a74213d"",
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
                    ""id"": ""4fe151b1-ad0c-46c0-8b4a-691be14be3be"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b48292e-ed9a-4e6a-bb2c-9d6528961fd4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Rotate"",
            ""id"": ""4b23967c-62b6-4b8b-918a-5271815cdf31"",
            ""actions"": [
                {
                    ""name"": ""CamR"",
                    ""type"": ""Button"",
                    ""id"": ""fbc355fe-139b-464e-9c6f-07917e11342c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CamL"",
                    ""type"": ""Button"",
                    ""id"": ""61cc7791-447a-4610-98cf-1eb29519c2de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ea4ff1a-bfd6-4429-853d-d394c22bb424"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbda6c31-8b70-404d-aa73-33ba689ee5ef"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""StageSelect"",
            ""id"": ""df853591-890d-4736-9e37-6d8d5d1df41b"",
            ""actions"": [
                {
                    ""name"": ""CursorMove"",
                    ""type"": ""Value"",
                    ""id"": ""923c1cc0-3a94-47a3-a083-723303e99fc3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Decision"",
                    ""type"": ""Button"",
                    ""id"": ""825b1fbe-b69d-43b1-8d8a-604c8eea4645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""dba4b5e2-2600-4937-aa47-96eb1918f1bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tutorial"",
                    ""type"": ""Button"",
                    ""id"": ""d5505478-71b8-4eb6-8a30-2ee89e632173"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f2e2730a-14e4-4c03-90eb-eefccf8b3c2a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1a7e50dc-7d96-4382-84ef-80e9b6e5335c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a8518418-1c98-4abe-a27a-1b5bef2f13e9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fe829c08-c446-40a9-95e3-18e363854258"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bf91c12a-bc09-4267-9efe-776d7f777032"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""01903508-d465-4341-bf30-1a0dcae9c426"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1271550a-2c7f-4492-86bd-8e8592024807"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""64048719-7d0d-4499-82f8-803862e4b2fb"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""97447c3b-f203-4b22-a3e2-9474753bc4b3"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5c63c049-2075-4229-b024-d954eb885432"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e827e89e-9a05-4db7-883a-4f40a8cb1a19"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decision"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef569d8f-e7dc-496e-b462-469b27c7d64b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""755f8ffb-758f-4d85-9836-01314fff5e54"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tutorial"",
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
        m_Player_SkillA = m_Player.FindAction("SkillA", throwIfNotFound: true);
        m_Player_SkillB = m_Player.FindAction("SkillB", throwIfNotFound: true);
        // Rotate
        m_Rotate = asset.FindActionMap("Rotate", throwIfNotFound: true);
        m_Rotate_CamR = m_Rotate.FindAction("CamR", throwIfNotFound: true);
        m_Rotate_CamL = m_Rotate.FindAction("CamL", throwIfNotFound: true);
        // StageSelect
        m_StageSelect = asset.FindActionMap("StageSelect", throwIfNotFound: true);
        m_StageSelect_CursorMove = m_StageSelect.FindAction("CursorMove", throwIfNotFound: true);
        m_StageSelect_Decision = m_StageSelect.FindAction("Decision", throwIfNotFound: true);
        m_StageSelect_Back = m_StageSelect.FindAction("Back", throwIfNotFound: true);
        m_StageSelect_Tutorial = m_StageSelect.FindAction("Tutorial", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_SkillA;
    private readonly InputAction m_Player_SkillB;
    public struct PlayerActions
    {
        private @ControllerInput m_Wrapper;
        public PlayerActions(@ControllerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @SkillA => m_Wrapper.m_Player_SkillA;
        public InputAction @SkillB => m_Wrapper.m_Player_SkillB;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @SkillA.started += instance.OnSkillA;
            @SkillA.performed += instance.OnSkillA;
            @SkillA.canceled += instance.OnSkillA;
            @SkillB.started += instance.OnSkillB;
            @SkillB.performed += instance.OnSkillB;
            @SkillB.canceled += instance.OnSkillB;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @SkillA.started -= instance.OnSkillA;
            @SkillA.performed -= instance.OnSkillA;
            @SkillA.canceled -= instance.OnSkillA;
            @SkillB.started -= instance.OnSkillB;
            @SkillB.performed -= instance.OnSkillB;
            @SkillB.canceled -= instance.OnSkillB;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Rotate
    private readonly InputActionMap m_Rotate;
    private List<IRotateActions> m_RotateActionsCallbackInterfaces = new List<IRotateActions>();
    private readonly InputAction m_Rotate_CamR;
    private readonly InputAction m_Rotate_CamL;
    public struct RotateActions
    {
        private @ControllerInput m_Wrapper;
        public RotateActions(@ControllerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @CamR => m_Wrapper.m_Rotate_CamR;
        public InputAction @CamL => m_Wrapper.m_Rotate_CamL;
        public InputActionMap Get() { return m_Wrapper.m_Rotate; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RotateActions set) { return set.Get(); }
        public void AddCallbacks(IRotateActions instance)
        {
            if (instance == null || m_Wrapper.m_RotateActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RotateActionsCallbackInterfaces.Add(instance);
            @CamR.started += instance.OnCamR;
            @CamR.performed += instance.OnCamR;
            @CamR.canceled += instance.OnCamR;
            @CamL.started += instance.OnCamL;
            @CamL.performed += instance.OnCamL;
            @CamL.canceled += instance.OnCamL;
        }

        private void UnregisterCallbacks(IRotateActions instance)
        {
            @CamR.started -= instance.OnCamR;
            @CamR.performed -= instance.OnCamR;
            @CamR.canceled -= instance.OnCamR;
            @CamL.started -= instance.OnCamL;
            @CamL.performed -= instance.OnCamL;
            @CamL.canceled -= instance.OnCamL;
        }

        public void RemoveCallbacks(IRotateActions instance)
        {
            if (m_Wrapper.m_RotateActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRotateActions instance)
        {
            foreach (var item in m_Wrapper.m_RotateActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RotateActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RotateActions @Rotate => new RotateActions(this);

    // StageSelect
    private readonly InputActionMap m_StageSelect;
    private List<IStageSelectActions> m_StageSelectActionsCallbackInterfaces = new List<IStageSelectActions>();
    private readonly InputAction m_StageSelect_CursorMove;
    private readonly InputAction m_StageSelect_Decision;
    private readonly InputAction m_StageSelect_Back;
    private readonly InputAction m_StageSelect_Tutorial;
    public struct StageSelectActions
    {
        private @ControllerInput m_Wrapper;
        public StageSelectActions(@ControllerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @CursorMove => m_Wrapper.m_StageSelect_CursorMove;
        public InputAction @Decision => m_Wrapper.m_StageSelect_Decision;
        public InputAction @Back => m_Wrapper.m_StageSelect_Back;
        public InputAction @Tutorial => m_Wrapper.m_StageSelect_Tutorial;
        public InputActionMap Get() { return m_Wrapper.m_StageSelect; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StageSelectActions set) { return set.Get(); }
        public void AddCallbacks(IStageSelectActions instance)
        {
            if (instance == null || m_Wrapper.m_StageSelectActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_StageSelectActionsCallbackInterfaces.Add(instance);
            @CursorMove.started += instance.OnCursorMove;
            @CursorMove.performed += instance.OnCursorMove;
            @CursorMove.canceled += instance.OnCursorMove;
            @Decision.started += instance.OnDecision;
            @Decision.performed += instance.OnDecision;
            @Decision.canceled += instance.OnDecision;
            @Back.started += instance.OnBack;
            @Back.performed += instance.OnBack;
            @Back.canceled += instance.OnBack;
            @Tutorial.started += instance.OnTutorial;
            @Tutorial.performed += instance.OnTutorial;
            @Tutorial.canceled += instance.OnTutorial;
        }

        private void UnregisterCallbacks(IStageSelectActions instance)
        {
            @CursorMove.started -= instance.OnCursorMove;
            @CursorMove.performed -= instance.OnCursorMove;
            @CursorMove.canceled -= instance.OnCursorMove;
            @Decision.started -= instance.OnDecision;
            @Decision.performed -= instance.OnDecision;
            @Decision.canceled -= instance.OnDecision;
            @Back.started -= instance.OnBack;
            @Back.performed -= instance.OnBack;
            @Back.canceled -= instance.OnBack;
            @Tutorial.started -= instance.OnTutorial;
            @Tutorial.performed -= instance.OnTutorial;
            @Tutorial.canceled -= instance.OnTutorial;
        }

        public void RemoveCallbacks(IStageSelectActions instance)
        {
            if (m_Wrapper.m_StageSelectActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IStageSelectActions instance)
        {
            foreach (var item in m_Wrapper.m_StageSelectActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_StageSelectActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public StageSelectActions @StageSelect => new StageSelectActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSkillA(InputAction.CallbackContext context);
        void OnSkillB(InputAction.CallbackContext context);
    }
    public interface IRotateActions
    {
        void OnCamR(InputAction.CallbackContext context);
        void OnCamL(InputAction.CallbackContext context);
    }
    public interface IStageSelectActions
    {
        void OnCursorMove(InputAction.CallbackContext context);
        void OnDecision(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnTutorial(InputAction.CallbackContext context);
    }
}
