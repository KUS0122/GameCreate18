using UnityEngine;

// 1. コンポーネントの追加漏れを防ぐ 
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 _currentMovement = Vector3.zero;

    private void Start()
    {
        // 使用するコンポーネントをキャッシュ 
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // 2. 入力方向の取得と移動距離の計算 
        _currentMovement.x = _playerInput.MovementInput.x * moveSpeed;
        _currentMovement.z = _playerInput.MovementInput.y * moveSpeed;

        // 3. キャラクターを移動させる 
        _characterController.Move(_currentMovement * Time.deltaTime);

        // 4. 重力の適用
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded)
        {
            _currentMovement.y = -1.0f;
        }
        else
        {
            _currentMovement.y += gravity * Time.deltaTime; // 重力による移動も適用 
        }
    }
}
