using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class ButtonController : MonoBehaviour
{
    [Header("Switch State")]
    [SerializeField]
    private bool _buttonActive = false;

    // 一度押したらGroundを踏むまで再押下不可
    private bool _isPlayerOnButton = false;

    public bool ButtonActive => _buttonActive;

    private Animator _animator;

    private int _buttonActiveHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _buttonActiveHash =
            Animator.StringToHash("ButtonActive");

        UpdateState();
    }

    /// <summary>
    /// ボタンを押す
    /// </summary>
    public void OnPressed(PlayerMovement player)
    {
        // 連続押し禁止
        if (_isPlayerOnButton)
            return;

        _isPlayerOnButton = true;

        // ON / OFF切替
        _buttonActive = !_buttonActive;

        Debug.Log($"Switch : {_buttonActive}");

        UpdateState();

        player.SetButtonActive(_buttonActive);
    }

    /// <summary>
    /// Groundを踏んだら解除
    /// </summary>
    public void ResetPressState()
    {
        _isPlayerOnButton = false;
    }

    /// <summary>
    /// リスポーン時OFFへ戻す
    /// </summary>
    public void ResetButton()
    {
        _buttonActive = false;

        _isPlayerOnButton = false;

        UpdateState();
    }

    /// <summary>
    /// Animator同期
    /// </summary>
    private void UpdateState()
    {
        _animator.SetBool(
            _buttonActiveHash,
            _buttonActive
        );
    }
}