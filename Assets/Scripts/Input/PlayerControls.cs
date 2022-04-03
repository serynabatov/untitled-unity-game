// GENERATED AUTOMATICALLY FROM 'Assets/Input System/PlayerControls.inputactions'

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
            ""id"": ""9966684e-1718-407b-bb8b-4465138caf71"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""fa0a6506-a3ef-48c5-8e17-d1a2296c8da1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
<<<<<<< HEAD
                    ""name"": ""Attack"",
                    ""type"": ""Value"",
=======
                    ""name"": ""MoveAxis"",
                    ""type"": ""Button"",
                    ""id"": ""e117df34-6671-4f6f-a873-a7e34f591602"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                    ""id"": ""afad137e-2881-426f-9825-3a4df8391109"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
<<<<<<< HEAD
=======
                    ""name"": ""Parry"",
                    ""type"": ""Button"",
                    ""id"": ""e0fd3c24-f989-475d-8242-740cf07ee6f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""634da9fc-0f17-45d7-b270-512b27826239"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenMenu"",
                    ""type"": ""Button"",
                    ""id"": ""fe417eb8-6ffb-43d9-96fd-e62a4552b79f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""19ab56cd-6785-4881-a1be-dbc3100c1456"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SubmitDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""a60a69c3-d1ab-43ef-86bc-7de6dc3c76c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill"",
                    ""type"": ""Button"",
                    ""id"": ""33fd3b2c-89c3-4d0a-a851-58b6b1454f4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
<<<<<<< HEAD
                    ""name"": ""Parry"",
                    ""type"": ""Button"",
                    ""id"": ""e0fd3c24-f989-475d-8242-740cf07ee6f2"",
=======
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a29fa0f9-6ff4-432d-acc0-6bda932b9761"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpAttack"",
                    ""type"": ""Button"",
                    ""id"": ""90da3e47-785a-49da-be2d-1af9650547cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackAirSlam"",
                    ""type"": ""Button"",
                    ""id"": ""ea49c410-5c7a-4a34-9e87-f4084a1ae70f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""5bef2ac8-9025-4fd8-8cd0-e738cde19d20"",
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""7304ec22-de15-478a-ab39-8eb35caa9b42"",
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
                    ""id"": ""517f9a0e-19a3-422b-92b8-5d244fae592c"",
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
                    ""id"": ""45a8a7bd-53d5-4bd6-aad4-b79d12bd600e"",
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
                    ""id"": ""8ac48d72-44b1-45bb-ab5b-09a0a807af71"",
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
                    ""id"": ""fadd63ed-5e7d-418c-bf6e-45272bbdd37c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a31fd099-1fe5-4f6f-bf2b-3f3554224e5b"",
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
                    ""id"": ""50c91db4-8cb2-45f7-8c00-c98815ce98e0"",
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
                    ""id"": ""837b6d85-5088-4317-9aa8-ad54b9d051a5"",
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
                    ""id"": ""6a961bf2-533f-4cb4-b156-8190e1211db5"",
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
                    ""id"": ""5be4f23a-a498-4ef0-ba2d-23ce443fa356"",
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
<<<<<<< HEAD
                    ""id"": ""2566ed9e-cd3b-4c35-bd05-ee6803360239"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
=======
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                    ""id"": ""3bf0d5d9-3dad-4cae-8933-f986b15d3e50"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cad9417-d207-4e70-8611-2cdf02544037"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04a6db62-695f-4070-b1bf-16e8e3c1dda0"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7de89269-0f87-4082-9293-556020eb7463"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SubmitDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cb94e85-4fe7-405e-b815-8c74dd9b1171"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
<<<<<<< HEAD
=======
                    ""id"": ""2129d23c-ba18-4995-a50e-b85e8a12605c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                    ""id"": ""9de7f9b0-2018-4256-ad36-39600310082c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
<<<<<<< HEAD
=======
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""a58769f6-3f1c-45b9-b345-12b2886ec5ad"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpAttack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""ef7f1b81-b069-417a-a57c-ccacbb36ea72"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""ad35c377-13ef-4d6f-9139-5c4273a70226"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""046b6eaa-16d6-4876-9701-0d2557b99e54"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackAirSlam"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""ea2d7d58-ab08-49ab-a58f-6cef6c107de9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackAirSlam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""9063b0a0-0e06-4e63-af64-9215f75c9fee"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackAirSlam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""24c9e3f5-6f84-44ce-ab81-bf31fd318fc2"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e3fb8c20-8d41-4bc6-80e4-85f748bea212"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9e0de602-f2e7-4f68-bd31-693b11ec98ab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""55560864-3e18-4c3f-bcd2-ee652775a27d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2566ed9e-cd3b-4c35-bd05-ee6803360239"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
<<<<<<< HEAD
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
=======
        m_Player_MoveAxis = m_Player.FindAction("MoveAxis", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Parry = m_Player.FindAction("Parry", throwIfNotFound: true);
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_OpenMenu = m_Player.FindAction("OpenMenu", throwIfNotFound: true);
        m_Player_OpenInventory = m_Player.FindAction("OpenInventory", throwIfNotFound: true);
        m_Player_SubmitDialogue = m_Player.FindAction("SubmitDialogue", throwIfNotFound: true);
        m_Player_Skill = m_Player.FindAction("Skill", throwIfNotFound: true);
<<<<<<< HEAD
        m_Player_Parry = m_Player.FindAction("Parry", throwIfNotFound: true);
=======
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_UpAttack = m_Player.FindAction("UpAttack", throwIfNotFound: true);
        m_Player_AttackAirSlam = m_Player.FindAction("AttackAirSlam", throwIfNotFound: true);
        m_Player_Dodge = m_Player.FindAction("Dodge", throwIfNotFound: true);
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
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
<<<<<<< HEAD
    private readonly InputAction m_Player_Attack;
=======
    private readonly InputAction m_Player_MoveAxis;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Parry;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_OpenMenu;
    private readonly InputAction m_Player_OpenInventory;
    private readonly InputAction m_Player_SubmitDialogue;
    private readonly InputAction m_Player_Skill;
<<<<<<< HEAD
    private readonly InputAction m_Player_Parry;
=======
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_UpAttack;
    private readonly InputAction m_Player_AttackAirSlam;
    private readonly InputAction m_Player_Dodge;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
<<<<<<< HEAD
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
=======
        public InputAction @MoveAxis => m_Wrapper.m_Player_MoveAxis;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Parry => m_Wrapper.m_Player_Parry;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @OpenMenu => m_Wrapper.m_Player_OpenMenu;
        public InputAction @OpenInventory => m_Wrapper.m_Player_OpenInventory;
        public InputAction @SubmitDialogue => m_Wrapper.m_Player_SubmitDialogue;
        public InputAction @Skill => m_Wrapper.m_Player_Skill;
<<<<<<< HEAD
        public InputAction @Parry => m_Wrapper.m_Player_Parry;
=======
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @UpAttack => m_Wrapper.m_Player_UpAttack;
        public InputAction @AttackAirSlam => m_Wrapper.m_Player_AttackAirSlam;
        public InputAction @Dodge => m_Wrapper.m_Player_Dodge;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
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
<<<<<<< HEAD
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
=======
                @MoveAxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveAxis;
                @MoveAxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveAxis;
                @MoveAxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveAxis;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Parry.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnParry;
                @Parry.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnParry;
                @Parry.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnParry;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @OpenMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenInventory;
                @SubmitDialogue.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubmitDialogue;
                @SubmitDialogue.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubmitDialogue;
                @SubmitDialogue.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubmitDialogue;
                @Skill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill;
                @Skill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill;
                @Skill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill;
<<<<<<< HEAD
                @Parry.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnParry;
                @Parry.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnParry;
                @Parry.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnParry;
=======
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @UpAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpAttack;
                @UpAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpAttack;
                @UpAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpAttack;
                @AttackAirSlam.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackAirSlam;
                @AttackAirSlam.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackAirSlam;
                @AttackAirSlam.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackAirSlam;
                @Dodge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
<<<<<<< HEAD
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
=======
                @MoveAxis.started += instance.OnMoveAxis;
                @MoveAxis.performed += instance.OnMoveAxis;
                @MoveAxis.canceled += instance.OnMoveAxis;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Parry.started += instance.OnParry;
                @Parry.performed += instance.OnParry;
                @Parry.canceled += instance.OnParry;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @OpenMenu.started += instance.OnOpenMenu;
                @OpenMenu.performed += instance.OnOpenMenu;
                @OpenMenu.canceled += instance.OnOpenMenu;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
                @SubmitDialogue.started += instance.OnSubmitDialogue;
                @SubmitDialogue.performed += instance.OnSubmitDialogue;
                @SubmitDialogue.canceled += instance.OnSubmitDialogue;
                @Skill.started += instance.OnSkill;
                @Skill.performed += instance.OnSkill;
                @Skill.canceled += instance.OnSkill;
<<<<<<< HEAD
                @Parry.started += instance.OnParry;
                @Parry.performed += instance.OnParry;
                @Parry.canceled += instance.OnParry;
=======
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @UpAttack.started += instance.OnUpAttack;
                @UpAttack.performed += instance.OnUpAttack;
                @UpAttack.canceled += instance.OnUpAttack;
                @AttackAirSlam.started += instance.OnAttackAirSlam;
                @AttackAirSlam.performed += instance.OnAttackAirSlam;
                @AttackAirSlam.canceled += instance.OnAttackAirSlam;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
<<<<<<< HEAD
        void OnAttack(InputAction.CallbackContext context);
=======
        void OnMoveAxis(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnParry(InputAction.CallbackContext context);
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
        void OnInteract(InputAction.CallbackContext context);
        void OnOpenMenu(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnSubmitDialogue(InputAction.CallbackContext context);
        void OnSkill(InputAction.CallbackContext context);
<<<<<<< HEAD
        void OnParry(InputAction.CallbackContext context);
=======
        void OnJump(InputAction.CallbackContext context);
        void OnUpAttack(InputAction.CallbackContext context);
        void OnAttackAirSlam(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
>>>>>>> 343d89c41a1992554ffbe5270ff3abc06046d65e
    }
}
