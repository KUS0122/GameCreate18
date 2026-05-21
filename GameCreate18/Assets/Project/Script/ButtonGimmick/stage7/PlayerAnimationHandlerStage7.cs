using UnityEngine;

[RequireComponent(typeof(PlayerMovementStage7))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandlerStage7 : MonoBehaviour
{
    private PlayerMovementStage7 _playerMovement;
    private Animator _animator;

    private int _isWalkingHash;
    private int _isJumpingHash;
    private int _jumpingHash;

    private void Awake()
    {
        _playerMovement =
            GetComponent<PlayerMovementStage7>();

        _animator =
            GetComponent<Animator>();

        _isWalkingHash =
            Animator.StringToHash("isWalking");

        _isJumpingHash =
            Animator.StringToHash("isJumping");

        _jumpingHash =
            Animator.StringToHash("Jumping");
    }

    private void Update()
    {
        bool isWalking =
            _animator.GetBool(_isWalkingHash);

        bool isJumping =
            _animator.GetBool(_isJumpingHash);

        bool jumping =
            _animator.GetBool(_jumpingHash);

        if (_playerMovement.IsMove != isWalking)
        {
            _animator.SetBool(
                _isWalkingHash,
                _playerMovement.IsMove
            );
        }

        if (_playerMovement.IsJump != isJumping)
        {
            _animator.SetBool(
                _isJumpingHash,
                _playerMovement.IsJump
            );
        }

        if (_playerMovement.Jumping != jumping)
        {
            _animator.SetBool(
                _jumpingHash,
                _playerMovement.Jumping
            );
        }
    }
}