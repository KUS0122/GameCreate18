using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private Vector3 _moveDirection;
    private bool _isGrounded;

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
    [SerializeField] private float fallThreshold = -3f;
    //[SerializeField] private Stage1 stage;

    private Vector3 _currentMovement = Vector3.zero;

    private Vector3 _respawnPosition;

    public bool IsMove { get; private set; } = false;
    public bool IsJump { get; private set; } = false;
    public bool Jumping { get; private set; } = false;
    public bool IsButtonActive { get; private set; } = false;

    public int score = 0;
    public static string gameState = "playing";

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();

        _respawnPosition = transform.position;
        gameState = "playing";
    }

    private void Update()
    {
        if (gameState != "playing")
        {
            return;
        }

        HandleMoveInput();
        HandleRotation();

        // 地面判定
        _isGrounded = _characterController.isGrounded;

        HandleJump();
        ApplyGravity();

        Vector3 move = new Vector3(
            _currentMovement.x,
            _currentMovement.y,
            _currentMovement.z
        );

        CollisionFlags flags =
            _characterController.Move(move * Time.deltaTime);

        _isGrounded =
            (flags & CollisionFlags.Below) != 0;

        Jumping = !_isGrounded;

        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
        Debug.Log(transform.position.y);
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

        // カメラ基準移動方向
        _moveDirection =
            forward * input.y +
            right * input.x;

        _moveDirection.Normalize();

        // 移動
        _currentMovement.x =
            _moveDirection.x * moveSpeed;

        _currentMovement.z =
            _moveDirection.z * moveSpeed;
    }

    private void HandleRotation()
    {
        // 入力なし
        if (_moveDirection == Vector3.zero)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(
                _moveDirection
            );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void ApplyGravity()
    {
        if (_isGrounded &&
            _currentMovement.y < 0)
        {
            _currentMovement.y = -2.0f;
        }

        _currentMovement.y += gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (_isGrounded &&
            !IsJump &&
            _playerInput.IsJumpPressed)
        {
            IsJump = true;
            _currentMovement.y = jumpForce;
        }

        if (_isGrounded &&
            _currentMovement.y <= 0f)
        {
            IsJump = false;
        }
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

// HEAD
    //プレイヤーステータス
    private void OnTriggerEnter(Collider collision)
    {
      if(collision.gameObject.tag == "Clear")
        {
            Clear();
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score += 1;
            Destroy(collision.gameObject);
        }
      else if(collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Next")
        {
            Next();
        }
    }
    public void Clear()
    {
        gameState = "gameclear";
        GameStop();
    }public void Next()
    {
        gameState = "next";
        GameStop();

        if(GameManager.instance != null)
        {
            GameManager.instance.NextStage();
        }
    }
    public void Goal()
    {
        gameState = "gameGoal";
        GameStop();
    }
    void GameStop()
    {
        IsJump = false;
        Jumping = false;
        _currentMovement = Vector3.zero;

    }
    public void ResetButtonState()
    {
        IsButtonActive = false;

        // 全ButtonをOFFへ戻す
        ButtonController[] buttons =
            Object.FindObjectsByType<ButtonController>(
                FindObjectsSortMode.None);

        foreach (ButtonController button in buttons)
        {
            button.ResetButton();
        }
    }
}