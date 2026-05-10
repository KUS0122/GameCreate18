using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;

    // パラメーター名をハッシュ化して処理を効率化します。 
    private int _isWalkingHash;
    private int _isJumpingHash;
    private int _JumpingHash;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();

        // Animatorパラメーターの文字列をあらかじめハッシュ化しておきます。 
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _JumpingHash = Animator.StringToHash("Jumping");
    }

    private void Update()
    {
        // 現在のAnimatorが保持しているパラメーターの値を取得します。 
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isJumping = _animator.GetBool(_isJumpingHash);
        bool Jumping = _animator.GetBool(_JumpingHash);
        // 【移動状態の同期】PlayerMovementのIsMove値とAnimatorの値が異なる場合のみ更新します。 
        if (_playerMovement.IsMove != isWalking)
        {
            _animator.SetBool(_isWalkingHash, _playerMovement.IsMove);
        }
        // 【ジャンプ状態の同期】先ほどプロパティとして修正したIsJumpを参照して更新します。 
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
