using UnityEngine;

public class stage9 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    [Header("ƒWƒƒƒ“ƒv—Í")]
    [SerializeField] private float normalJump = 5.0f;
    [SerializeField] private float highJump = 8.0f;
    [SerializeField] public float Duration = 10.0f;
    [SerializeField] private float Timer = 0.0f;

    private bool _ButtonState = false;
    private bool _isHighJumpActive = false;
    void Start()
    {
        if(player != null)
        {
            _ButtonState = player.IsButtonActive;
        }
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }
        if(player.IsButtonActive != _ButtonState )
        {
            if(player.IsButtonActive)
            {
                ToggleJump();
            }
            _ButtonState = player.IsButtonActive;
        }
        if(_isHighJumpActive)
        {
            Timer -= Time.deltaTime;

            if( Timer <= 0.0f )
            {
                Highjump();
            }
        }
    }
    private void ToggleJump()
    {
        player.jumpForce = highJump;
        Timer = Duration;
        _isHighJumpActive = true;
    }
    private void Highjump()
    {
        player.jumpForce = normalJump;
        Timer = 0.0f;
        _isHighJumpActive = false;
    }

    /*public override void ResetStage()
    {
        player.jumpForce = normalJump;
    }*/
}
