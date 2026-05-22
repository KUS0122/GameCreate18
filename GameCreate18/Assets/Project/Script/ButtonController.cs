using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class ButtonController : MonoBehaviour
{
    [Header("Switch State")]
    [SerializeField]
    private bool _buttonActive = false;

    // àÍìxâüÇµÇΩÇÁGroundÇì•ÇﬁÇ‹Ç≈çƒâüâ∫ïsâ¬
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
        // òAë±âüÇµã÷é~
        if (_isPlayerOnButton)
            return;

        _isPlayerOnButton = true;

        // ON / OFFêÿë÷
        _buttonActive = !_buttonActive;

        //Debug.Log($"Switch : {_buttonActive}");

        UpdateState();

        player.SetButtonActive(_buttonActive);
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