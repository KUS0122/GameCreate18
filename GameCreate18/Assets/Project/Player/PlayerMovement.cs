using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;

    [Header("Camera")]
    [SerializeField]
    private Transform cameraTransform;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    [Header("Jump")]
    [SerializeField] private float jumpStartDelay = 0.2f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Respawn")]
    [SerializeField] private float fallThreshold = -10f;
    //[SerializeField] private Stage1 stage;

    private Vector3 _currentMovement = Vector3.zero;

    private Vector3 _respawnPosition;

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
        Vector2 input =
            _playerInput.MovementInput;

        IsMove = input != Vector2.zero;

        // カメラ前方向
        Vector3 forward =
            cameraTransform.forward;

        forward.y = 0f;
        forward.Normalize();

        // カメラ右方向
        Vector3 right =
            cameraTransform.right;

        right.y = 0f;
        right.Normalize();

        // 移動方向
        Vector3 moveDirection =
            forward * input.y +
            right * input.x;

        _currentMovement.x =
            moveDirection.x * moveSpeed;

        _currentMovement.z =
            moveDirection.z * moveSpeed;
    }

    private void HandleRotation()
    {
        Vector2 input =
            _playerInput.MovementInput;

        if (input == Vector2.zero)
            return;

        // カメラ前方向
        Vector3 forward =
            cameraTransform.forward;

        forward.y = 0f;
        forward.Normalize();

        // カメラ右方向
        Vector3 right =
            cameraTransform.right;

        right.y = 0f;
        right.Normalize();

        // 入力方向
        Vector3 moveDirection =
            forward * input.y +
            right * input.x;

        Quaternion targetRotation =
            Quaternion.LookRotation(moveDirection);

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
        Jumping = false;

        _currentMovement = Vector3.zero;

        transform.position =
            _respawnPosition + Vector3.up * 0.5f;

        IsButtonActive = false;

        // 全ボタンリセット
        ButtonController[] buttons =
            Object.FindObjectsByType<ButtonController>(
                FindObjectsSortMode.None);

        foreach (ButtonController button in buttons)
        {
            button.ResetButton();
        }

        // 全ステージギミックリセット
        StageBase[] stages =
            Object.FindObjectsByType<StageBase>(
                FindObjectsSortMode.None);

        foreach (StageBase stage in stages)
        {
            stage.ResetStage();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Groundを踏んだら再押下可能
        if (hit.gameObject.CompareTag("Ground"))
        {
            ButtonController[] buttons =
                Object.FindObjectsByType<ButtonController>(FindObjectsSortMode.None);

            foreach (ButtonController button in buttons)
            {
                button.ResetPressState();
            }
        }

        // Buttonを押す
        ButtonController buttonController =
            hit.gameObject.GetComponentInParent<ButtonController>();

        if (buttonController != null)
        {
            buttonController.OnPressed(this);
        }
    }

    public void UpdateRespawnPoint(
    Vector3 newPosition)
    {
        _respawnPosition = newPosition;
    }
}