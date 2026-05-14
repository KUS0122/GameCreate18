using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private int _isWalkingHash;
    private int _isJumpingHash;
    private int _JumpingHash;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
 
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _JumpingHash = Animator.StringToHash("Jumping");
    }

    private void Update()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isJumping = _animator.GetBool(_isJumpingHash);
        bool Jumping = _animator.GetBool(_JumpingHash); 
        if (_playerMovement.IsMove != isWalking)
        {
            _animator.SetBool(_isWalkingHash, _playerMovement.IsMove);
        }
        if (_playerMovement.IsJump != isJumping)
        {
            _animator.SetBool(_isJumpingHash, _playerMovement.IsJump);
        }
        if (_playerMovement.Jumping != Jumping)
        {
            _animator.SetBool(_JumpingHash, _playerMovement.Jumping);
        }
    }
}
