using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class ButtonControllerStage7 : MonoBehaviour
{
    [Header("Switch State")]
    [SerializeField]
    private bool _buttonActive = false;

    [Header("Stage7")]
    [SerializeField]
    private Stage7 stage7;

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

    public void OnPressed(PlayerMovement player)
    {
        if (_isPlayerOnButton)
            return;

        _isPlayerOnButton = true;

        _buttonActive = !_buttonActive;

        Debug.Log($"Switch : {_buttonActive}");

        UpdateState();

        player.SetButtonActive(_buttonActive);

        if (_buttonActive && stage7 != null)
        {
            stage7.OnSwitchPressed();
        }
    }

    public void ResetPressState()
    {
        _isPlayerOnButton = false;
    }

    public void ResetButton()
    {
        _buttonActive = false;
        _isPlayerOnButton = false;

        UpdateState();
    }

    private void UpdateState()
    {
        _animator.SetBool(
            _buttonActiveHash,
            _buttonActive
        );
    }
}