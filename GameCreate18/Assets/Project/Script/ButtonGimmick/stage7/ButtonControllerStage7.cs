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

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovementStage7 player =
            other.GetComponent<PlayerMovementStage7>();

        if (player == null)
            return;

        OnPressed(player);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovementStage7 player =
            other.GetComponent<PlayerMovementStage7>();

        if (player == null)
            return;

        ResetPressState();
    }

    public void OnPressed(PlayerMovementStage7 player)
    {
        //Debug.Log("OnPressed Called");

        if (_isPlayerOnButton)
            return;

        if (_buttonActive)
            return;

        _isPlayerOnButton = true;
        _buttonActive = true;

        //Debug.Log($"Switch : {_buttonActive}");

        UpdateState();

        player.SetButtonActive(_buttonActive);

        if (stage7 != null)
        {
            stage7.OnSwitchPressed();
        }
        else
        {
            //Debug.LogWarning("Stage7 is not assigned");
        }

        //Debug.Log("Switch Pressed");
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