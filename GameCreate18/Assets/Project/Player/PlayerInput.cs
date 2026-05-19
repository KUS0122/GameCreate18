using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _inputActions;

    public Vector2 MovementInput { get; private set; }
    public bool IsJumpPressed { get; private set; }

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
        if (_inputActions == null)
            _inputActions = new PlayerInputActions();

        _inputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        if (_inputActions != null)
            _inputActions.CharacterControls.Disable();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
    }
}