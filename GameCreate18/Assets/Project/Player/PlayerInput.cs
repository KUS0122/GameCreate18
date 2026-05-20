using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{    private PlayerInputActions _inputActions;

    public Vector2 MovementInput { get; private set; }
    public bool IsJumpPressed { get; private set; } //ジャンプ入力の状態 


    private void Awake()
    {
        _inputActions = new PlayerInputActions();

        _inputActions.CharacterControls.Move.started += OnMovementInput;
        _inputActions.CharacterControls.Move.performed += OnMovementInput;
        _inputActions.CharacterControls.Move.canceled += OnMovementInput;

        _inputActions.CharacterControls.Jump.started += OnJumpInput;
        _inputActions.CharacterControls.Jump.canceled += OnJumpInput;
    }

    private void OnEnable()
    {
        _inputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _inputActions.CharacterControls.Disable();
    }

    //入力値を処理するメソッド
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
    }

}
