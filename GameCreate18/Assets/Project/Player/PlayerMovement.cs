using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    [Header("Jump")]
    [SerializeField] private float jumpStartDelay = 0.2f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Respawn")]
    [SerializeField] private float fallThreshold = -10f;
    [SerializeField] private Stage1 stage;

    private Vector3 _currentMovement = Vector3.zero;

    private Vector3 _respawnPosition;
    private Transform _currentRoom;

    public bool IsMove { get; private set; } = false;
    public bool IsJump { get; private set; } = false;
    public bool Jumping { get; private set; } = false;
    public bool IsButtonActive { get; private set; } = false;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();

        _respawnPosition = transform.position;
    }

    private void Update()
    {
        HandleMoveInput();
        HandleRotation();
        HandleJump();
        ApplyGravity();

        _characterController.Move(_currentMovement * Time.deltaTime);

        // 空中判定
        Jumping = !_characterController.isGrounded;

        // 落下チェック
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void HandleMoveInput()
    {
        IsMove = _playerInput.MovementInput != Vector2.zero;

        _currentMovement.x =
         _playerInput.MovementInput.x * moveSpeed;

        _currentMovement.z =
            _playerInput.MovementInput.y * moveSpeed;
    }


    private void HandleRotation()
    {
        Vector2 input = _playerInput.MovementInput;

        if (input == Vector2.zero)
            return;

        Vector3 targetDirection =
            new Vector3(input.x, 0, input.y);

        Quaternion targetRotation =
            Quaternion.LookRotation(targetDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded &&
            _currentMovement.y < 0)
        {
            _currentMovement.y = -2.0f;
        }

        _currentMovement.y += gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (_characterController.isGrounded)
        {
            if (!IsJump &&
                _playerInput.IsJumpPressed)
            {
                StartCoroutine(JumpCoroutine());
            }
        }
    }

    private IEnumerator JumpCoroutine()
    {

        // JumpStart
        IsJump = true;

        // しゃがみ待機
        yield return new WaitForSeconds(jumpStartDelay);

        // ジャンプ
        _currentMovement.y = jumpForce;

        // 地面を離れるまで待機
        while (_characterController.isGrounded)
        {
            yield return null;
        }

        // JumpLoopへ
        IsJump = false;

        // 着地待機
        while (!_characterController.isGrounded)
        {
            yield return null;
        }

        // 終了
        IsJump = false;
    }

    public void SetButtonActive(bool active)
    {
        IsButtonActive = active;
    }

    private void Respawn()
    {
        StopAllCoroutines();

        IsJump = false;
        IsJump = false;
        Jumping = false;

        _currentMovement = Vector3.zero;

        transform.position =
            _respawnPosition + Vector3.up * 0.5f;

        IsButtonActive = false;

        //if (_currentRoom != null)
        //{
        //    foreach (ButtonController button in
        //             _currentRoom.GetComponentsInChildren<ButtonController>(true))
        //    {
        //        button.ResetButton();
        //    }
        //}

        if (stage != null)
        {
            stage.ResetStage();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Button"))
            return;

        if (hit.gameObject.TryGetComponent(out ButtonController button))
        {
            if (!button.IsActive)
                return;

            button.OnPressed(this);
        }
    }
}